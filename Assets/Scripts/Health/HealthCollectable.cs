using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float extraHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Character")){
            collision.collider.GetComponent<Health>().AddHealth(extraHealth);
            Debug.Log("DMGPlatform");
            gameObject.SetActive(false);
        }
    }
}
