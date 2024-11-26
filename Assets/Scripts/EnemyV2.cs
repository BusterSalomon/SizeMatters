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
    private bool enemyEnabled = false;


    public void TakeHit(float damage)
    {
        CurrentHealth -= damage;
        Healthbar.SetHealth(CurrentHealth, MaxHealth);

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

    public void DisableEnemy()
    {
        enemyEnabled = false;
    }

    public void EnableEnemy()
    {
        enemyEnabled = true;
    }
}
