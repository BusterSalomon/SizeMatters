using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    [SerializeField] private uint extraAmmo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Character")){
            collision.collider.GetComponent<PlayerAttack>().AddAmmo(extraAmmo);
            Debug.Log("Ammo Collected");
            gameObject.SetActive(false);
        }
    }
}
