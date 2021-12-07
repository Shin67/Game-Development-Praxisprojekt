using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int currentHealth;
    public Vector2 direction;
    public int directionUnits;
    public int speed;
    public int playerDistance;
    public int maxHealth;
    public HealthBar healthBar;
    
    bool goingBack;
    bool playerFound;
    int directionCounter;
    Rigidbody2D rigidbody;
    GameObject player;
    float savedX;
    float savedY;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        goingBack = false;
        playerFound = false;
        directionCounter = 0;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerFound)
        {
            player = GameObject.FindWithTag("Player");
            Debug.Log("Gegner: " + transform.position);
            Debug.Log("Player: " + player.transform.position);
            Vector2 playerCoord = new Vector2((player.transform.position - transform.position).normalized.x, (player.transform.position - transform.position).normalized.y);
            rigidbody.velocity = playerCoord * speed;
            if (Vector3.Distance(player.transform.position, rigidbody.transform.position) >= playerDistance)
            {
                playerFound = false;
                goingBack = true;
            }
        } else if (goingBack)
        {
            Vector2 goingBackDirection = (new Vector2(savedX, savedY).normalized - new Vector2(rigidbody.position.normalized.x, rigidbody.position.normalized.y).normalized) * speed;
          
            rigidbody.velocity = goingBackDirection;
            Debug.Log(goingBackDirection.x);
            if (goingBackDirection.x < 0.1 && goingBackDirection.y < 0.1)
            {
                Debug.Log(goingBackDirection);
                goingBack = false;
            }
        } else
        {
            if (directionCounter <= directionUnits && !goingBack && !playerFound)
            {
                Debug.Log("Player found: " + playerFound);
                Debug.Log("Going back: " + goingBack);
                rigidbody.velocity = direction;
                directionCounter ++;
            } else
            {
                directionCounter = 0;
                direction.x = -direction.x;
                direction.y = -direction.y;
            }
            savedX = rigidbody.position.x;
            savedY = rigidbody.position.y;
        }
        
    }

    public void getDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetSlider(damage);
    }

    public void setPlayerFound(bool playerFound)
    {
        this.playerFound = playerFound;
    }

}