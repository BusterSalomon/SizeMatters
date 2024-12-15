using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngineInternal;

public class Enemy : Walkable
{
    // Healthbar object
    public EnemyHealthbar Healthbar;
    public float CurrentHealth;
    public float MaxHealth;
    public float CollisionDamage;
    public float SelfDamageOnCharacterCollision;
    public Animator ExplosionAnimator;
    private bool enemyDeadDestroyOnDeadAnimationComplete = false;

    [SerializeField] bool canDrop = false;
    [SerializeField] int DropChance;

    public GameObject[] itemDrop;

    protected override void Start()
    {
        base.Start();
        CurrentHealth = MaxHealth;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);
        if(DropChance > 100) DropChance = 100;
    }

    private bool explosionAnimStarted = false;
    protected override void Update()
    {
        base.Update();
        
        if (enemyDeadDestroyOnDeadAnimationComplete)
        {
            bool isExploding = ExplosionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Explosion");
            if (isExploding)
            {
                explosionAnimStarted = true;
            }
            if (!isExploding && explosionAnimStarted)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDeadAnimationStarted ()
    {}


    public virtual void TakeHit(float damage)
    {
        CurrentHealth -= damage;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
        {
            if (ExplosionAnimator)
            {
                ExplosionAnimator.SetTrigger("fired");
                FindObjectOfType<AudioManager>().Play("explosion");
                OnDeadAnimationStarted();
                enemyDeadDestroyOnDeadAnimationComplete = true;
            } else
            {
                if(canDrop){ItemDrop();}
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Method for damaging character
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="characterHealth"></param>
    public void DamageCharacter(float damage, Health characterHealth)
    {
        characterHealth.TakeDamage(damage);
    }

    /// <summary>
    /// Flip the enemy
    /// </summary>
    protected virtual void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Character"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(CollisionDamage);
            TakeHit(SelfDamageOnCharacterCollision);
        }
    }

    public void ItemDrop(){

        int rand = Random.Range(1, 100); 
        int drop = itemDrop.Count();

        if( rand < DropChance ){
            if(itemDrop != null){

                Debug.Log("NumOfItems: " + drop );
                Instantiate(itemDrop[Random.Range(0,drop)], transform.position + new Vector3(0,1,0), Quaternion.identity);

            }
        }
    }

}
