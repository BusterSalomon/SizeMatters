using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool locked;
    [SerializeField] private Sprite openDoorSprite; // The sprite for the open door
    [SerializeField] private Sprite closeDoorSprite; // The sprite for the closed door
     private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        locked = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateDoorSprite();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        KeyScript keyScript = other.GetComponent<KeyScript>();
        if (other.gameObject.CompareTag("Key") && keyScript.isRealKey)
        {
            locked = false;
            UpdateDoorSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        KeyScript keyScript = other.GetComponent<KeyScript>();
        if (other.gameObject.CompareTag("Key") && keyScript.isRealKey)
        {
            locked = true;
            UpdateDoorSprite();
        }
    }
    
    private void UpdateDoorSprite()
    {
        if (locked)
        {
            spriteRenderer.sprite = closeDoorSprite;
        }
        else
        {
            spriteRenderer.sprite = openDoorSprite;
        }
    }
}
