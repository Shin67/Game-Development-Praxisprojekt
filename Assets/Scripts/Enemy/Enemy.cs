using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public int maxHealth;
    public HealthBar healthBar;
    public int exp;
    public int attack;
    public GameObject[] droppableItems;

    GameObject itemDrop;
    int currentHealth;

    private void Start()
    {
        itemDrop = droppableItems[Random.Range(0, droppableItems.Length)];
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void TakeDmg(int damage)
    {
        currentHealth -= damage;
        healthBar.SetSlider(currentHealth);
        if (currentHealth <= 0)
        {
            var player = GameObject.FindWithTag("Player");
            player.GetComponent<Player>().addExp(exp);
            Destroy(gameObject);
        }
    }
}