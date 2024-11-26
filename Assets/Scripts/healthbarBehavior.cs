using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public float HealthbarHeight = 0.75f;

    public void SetHealth(float health, float maxHealth)
    {
        Slider.gameObject.SetActive(health <= maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }

    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + new Vector3(0, HealthbarHeight, 0));
    }
}
