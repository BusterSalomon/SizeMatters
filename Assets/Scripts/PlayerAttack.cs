using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{   
    public TMP_Text Ammo_Text;

    [SerializeField] private uint maxAmmo;
    [SerializeField] private uint currentAmmo;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    private Animator anim;
    private MovementDEPRECATED playerMovement; 
    private float cooldownTimer = Mathf.Infinity;

    private void Awake(){

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<MovementDEPRECATED>();

        maxAmmo = 30;
        currentAmmo = 10;
        UpdateAmmoText();
    }

    private void Update() {

        if(Input.GetKey(KeyCode.N) && cooldownTimer > attackCooldown /*&& playerMovement.canAttack()*/){
            Attack();
            Debug.Log("Attack called " + currentAmmo);
        }

        if(Input.GetKey(KeyCode.R)){
            Reload();
            Debug.Log("Reload");
        }

        cooldownTimer += Time.deltaTime;
    }

        private void Attack(){

        if( (maxAmmo != 0 ) && (currentAmmo != 0) ){

            anim.SetTrigger("attack");
            cooldownTimer = 0;

            projectiles[FindProjectile()].transform.position = firePoint.position;
            projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            //projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(-1);
        
            // Debug.Log(Mathf.Sign(transform.localScale.x));

            currentAmmo--;
            UpdateAmmoText();
            }
        }

    private void Reload(){
        maxAmmo -= (10 - currentAmmo);
        currentAmmo = 10;
        UpdateAmmoText();
    }

    private int FindProjectile(){
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public void AddAmmo(uint _ammo){
        maxAmmo += _ammo;
        UpdateAmmoText();
    }
    private void UpdateAmmoText(){
        Ammo_Text.text = "Ammo: " + maxAmmo.ToString() + " | " + currentAmmo.ToString() ;
    }

}