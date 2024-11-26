using UnityEngine;

public class DamagePlatforms : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Character")){
            collision.collider.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("DMGPlatform");
        }
    }


}
