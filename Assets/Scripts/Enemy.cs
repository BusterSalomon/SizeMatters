using System.Collections;
using System.Collections.Generic;
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

    protected override void Start()
    {
        base.Start();
        CurrentHealth = MaxHealth;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);
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
    protected void Flip()
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

}
