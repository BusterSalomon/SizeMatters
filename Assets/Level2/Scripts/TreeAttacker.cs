using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAttacker : EnemyBase
{
    public override List<string> GetTargetTags()
    {
        return new List<string> { "Tree" };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Attack(collision.gameObject);
        }
    }

    private void Attack(GameObject g)
    {
        g.GetComponent<Health>().TakeDamage(1);
        Destroy(gameObject);
    }
}
