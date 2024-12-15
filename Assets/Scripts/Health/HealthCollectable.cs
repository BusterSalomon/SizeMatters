using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float extraHealth;
    private Rigidbody2D  itemRb;
    public float dropForce = 5;

    void Start(){
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Character")){
            collision.collider.GetComponent<Health>().AddHealth(extraHealth);
            Debug.Log("DMGPlatform");
            gameObject.SetActive(false);
        }
    }
}
