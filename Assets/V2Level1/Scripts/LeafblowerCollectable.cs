using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafblowerCollectable : Collectable
{
    public float initialForce = 100f;
    public Transform blowNozzle;
    private bool blow = false;

    private void Start()
    {
        blowNozzle = transform.Find("BlowNozzle");

        if (blowNozzle == null)
        {
            Debug.LogError("BlowNozzle child object not found.");
        }
        else
        {
            Debug.Log("BlowNozzle Transform found: " + blowNozzle.position);
        }
    }
    private void Update()
    {
        if (IsCollected && Input.GetKey(KeyCode.N))
        {
            bool blowBtnPressed = Input.GetKey(KeyCode.N);
            blow = true;
        } else
        {
            blow = false;
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
