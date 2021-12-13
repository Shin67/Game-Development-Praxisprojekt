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
<<<<<<< HEAD
=======
        {
            Destroy(gameObject);
        }
    }

    public void setPlayerFound(bool playerFound)
    {
        this.playerFound = playerFound;
    }

}


/*
 
  if (direction.x > 0) // moves right
>>>>>>> f22815d9598172cd0ed56f51924d988794be7489
        {
            Destroy(gameObject);
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<Player>().addExp(exp);
        }
    }
}