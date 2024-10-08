using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthBar : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;

    float health, maxHealth = 100;

    private void Update()
    {
        healthText.text = "Health" + health + "%";
        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        
    }
}
