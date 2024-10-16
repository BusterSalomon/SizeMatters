using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    private Animator anim;
    private Movement playerMovement; 
    private float cooldownTimer = Mathf.Infinity;

    private void Awake(){

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Movement>();

    }

    private void Update() {

        if(Input.GetKey(KeyCode.N) && cooldownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
            Debug.Log("Attack called");
        }

        cooldownTimer += Time.deltaTime;
    }

        private void Attack(){

        anim.SetTrigger("attack");
        cooldownTimer = 0;

        projectiles[FindProjectile()].transform.position = firePoint.position;
        projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        //projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(-1);
    
       // Debug.Log(Mathf.Sign(transform.localScale.x));
    }

    private int FindProjectile(){
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}