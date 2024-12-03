using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class EnemyV2 : Walkable
{
    // Healthbar object
    public EnemyHealthbar Healthbar;
    public float CurrentHealth;
    public float MaxHealth;
    public float Damage;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);  
    }

    public void TakeHit(float damage)
    {
        CurrentHealth -= damage;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
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
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
        }
    }

}
