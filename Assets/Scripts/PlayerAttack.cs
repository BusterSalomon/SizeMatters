using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{   
    public TMP_Text Ammo_Text;

    [SerializeField] protected uint maxAmmo;
    [SerializeField] protected uint currentAmmo;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    private Animator anim;
    private MovementDEPRECATED playerMovement; 
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] protected Collectable gunCollectable;

<<<<<<< Updated upstream
    protected virtual void Awake(){
=======
    private AudioManager am;

    private void Awake(){
>>>>>>> Stashed changes

        am = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<MovementDEPRECATED>();
        //gunCollectable = GetComponent<GunCollectable>(); 

        // maxAmmo = 30;
        // currentAmmo = 10;
        UpdateAmmoText();
    }

    protected virtual void Update() {

        if(Input.GetKey(KeyCode.N) && cooldownTimer > attackCooldown /*&& playerMovement.canAttack()*/){
            Attack();
            FindObjectOfType<AudioManager>().Play("shooting");
            Debug.Log("Attack called " + currentAmmo);
        }

        if(Input.GetKey(KeyCode.R)){
            Reload();
<<<<<<< Updated upstream
            FindObjectOfType<AudioManager>().Play("reload");
=======
            am.Play("reloadGun");
>>>>>>> Stashed changes
            Debug.Log("Reload");
        }

        cooldownTimer += Time.deltaTime;
    }

    protected virtual void Attack(){

    if( (maxAmmo != 0 ) && (currentAmmo != 0) && canAttack()){

<<<<<<< Updated upstream
        anim.SetTrigger("attack");
        cooldownTimer = 0;
=======
            am.Play("shootGun");
            anim.SetTrigger("attack");
            cooldownTimer = 0;
>>>>>>> Stashed changes

        projectiles[FindProjectile()].transform.position = firePoint.position;
        projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        //projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(-1);
    
        // Debug.Log(Mathf.Sign(transform.localScale.x));

        currentAmmo--;
        UpdateAmmoText();
        }
    }

    protected virtual void Reload(){
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
    protected virtual void UpdateAmmoText(){
        Ammo_Text.text = "Ammo: " + maxAmmo.ToString() + " | " + currentAmmo.ToString() ;
    }

    public virtual bool canAttack(){
        return gunCollectable != null && gunCollectable.IsCollected;
    }

}