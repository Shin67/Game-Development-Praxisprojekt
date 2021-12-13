using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int maxHealth;
    public HealthBar healthBar;
    public int exp;
    public int attack;

    int currentHealth;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void TakeDmg(int damage)
    {
        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}