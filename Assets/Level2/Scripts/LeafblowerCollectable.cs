using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafblowerCollectable : Collectable
{
    public float initialForce = 100f;
    public float AudioFadeTime = 1f;
    private Transform blowNozzle;
    private bool blowbtnpressed = false;
    private Animator anim;
    private Blowable blowable;

    private void Start()
    {
        blowNozzle = transform.Find("BlowNozzle");
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsCollected && KeyWasPressed(KeyCode.N))
        {
            blowbtnpressed = true;
            anim.SetBool("IsBlowing", true);
            AudioManager.instance.FadeIn("leafblower_middle", AudioFadeTime);
        } 
        if (IsCollected && KeyWasReleased(KeyCode.N))
        {
            blowbtnpressed = false;
            anim.SetBool("IsBlowing", false);
            AudioManager.instance.FadeOut("leafblower_middle", AudioFadeTime);
            if (blowable && blowable.IsGettingBlownAt) blowable.StopBlowing();
        }
    }

    public void HandleObjectInBlowZone(Collider2D other)
    {
        if (other.TryGetComponent<Blowable>(out Blowable blowableInZone)) {
            blowable = blowableInZone;

            if (blowbtnpressed)
            {
                Vector3 direction = (other.transform.position - blowNozzle.position).normalized;
                float distance = Vector3.Distance(blowNozzle.position, other.transform.position);

                // Adjust force based on distance, using inverse-square law or other fall-off
                float force = initialForce / Mathf.Pow(distance + 0.5f, 1.8f);

                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(direction * force);
                }

                if (!blowable.IsGettingBlownAt) blowable.StartBlowing((int)Mathf.Sign(direction.x));
            } 
        }
        
        
        //else if (!blowable)
        //{
        //    if (blowable.IsGettingBlownAt) blowable.StartBlowing();
        //}
    }

}
