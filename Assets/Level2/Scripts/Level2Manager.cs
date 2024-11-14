using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : LevelManager
{

    private Transform jarLeftTransform;
    private Transform jarRightTransform;
    private Transform virusTransform;
    private Health characterHealth;

    public void Start()
    {
        GameObject jar = GameObject.Find("Jar");
        jarLeftTransform = jar.transform.Find("Left");
        jarRightTransform = jar.transform.Find("Right");
        virusTransform = GameObject.Find("Virus").transform;
        characterHealth = GameObject.FindGameObjectWithTag("Character").GetComponent<Health>();
    }
    public override bool DidWin()
    {
        float x = virusTransform.position.x;
        float y = virusTransform.position.y;
        float jlx = jarLeftTransform.position.x;
        float jrx = jarRightTransform.position.x;
        float jy = jarLeftTransform.position.y + jarLeftTransform.localScale.y/2;
        return (x > jlx && x < jrx && y < jy);
    }

    public override bool DidLose()
    {
        return characterHealth.currentHealth <= 0;
    }
}
