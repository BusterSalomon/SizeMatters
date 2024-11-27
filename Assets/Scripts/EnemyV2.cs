using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV2 : MonoBehaviour
{
    // Healthbar object
    public EnemyHealthbar Healthbar;
    public float CurrentHealth;
    public float MaxHealth;
    public float Damage;
    public float MovementSpeed;
    protected bool enemyEnabled = true;
    protected bool isGrounded = false;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);
        Debug.Log("Base start called");
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

    public void DisableEnemy()
    {
        enemyEnabled = false;
    }

    public void EnableEnemy()
    {
        enemyEnabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
