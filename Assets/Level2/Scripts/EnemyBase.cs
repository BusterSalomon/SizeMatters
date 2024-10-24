using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Serializable] private int health;
    [Serializable] private int move_speed;



    // ---- START ----
    void Start()
    {
        
    }

    // ---- UPDATE ----
    void Update()
    {
        
    }

    // ---- OWN METHODS ----
    public void TakeHit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Print("I am dead, destroy me!");
            //Destroy(gameObject);
        }
    }

}
