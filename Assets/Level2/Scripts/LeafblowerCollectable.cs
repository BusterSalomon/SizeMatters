using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafblowerCollectable : Collectable
{
    public float initialForce = 100f;
    public float AudioFadeTime = 1f;
    private Transform blowNozzle;
    private bool blow = false;
    private Animator anim;
    private AudioManager audioManager;

    private void Start()
    {
        blowNozzle = transform.Find("BlowNozzle");
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if (IsCollected && KeyWasPressed(KeyCode.N))
        {
            blow = true;
            anim.SetBool("IsBlowing", true);
            audioManager.FadeIn("leafblower_middle", AudioFadeTime);
        } 
        if (IsCollected && KeyWasReleased(KeyCode.N))
        {
            blow = false;
            anim.SetBool("IsBlowing", false);
            audioManager.FadeOut("leafblower_middle", AudioFadeTime);
        }
    }

    public void HandleObjectInBlowZone(Collider2D other)
    {
        if (blow)
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
        }
    }

}
