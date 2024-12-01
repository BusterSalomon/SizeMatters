using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class EnemyV2 : MonoBehaviour
{
    // Healthbar object
    public EnemyHealthbar Healthbar;
    public float CurrentHealth;
    public float MaxHealth;
    public float Damage;
    public float MovementSpeed;
    protected bool enemyEnabled = true;
    [SerializeField] protected bool isGrounded = false;
    protected BoxCollider2D collider;
    public LayerMask groundLayer;

    protected float rayCastLen;
    public float rayCastDelta = 0.2f;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        if (Healthbar) Healthbar.SetHealth(CurrentHealth, MaxHealth);
        InitializeGroundCheck();   
    }

    protected virtual void Update()
    {
        isGrounded = DoGroundCheck();
    }

    protected void InitializeGroundCheck()
    {
        collider = GetComponent<BoxCollider2D>();
        rayCastLen = collider.size.y / 2 + rayCastDelta;
        Debug.Log($"raylen: {rayCastLen}");
    }
    protected bool DoGroundCheck ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayCastLen, groundLayer);
        if (hit)
        {
           return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, rayCastLen, 0));
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

    //protected virtual void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //protected virtual void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}
