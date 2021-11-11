﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    //Kampf shit
    public Transform attackpoint;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;
    public int Schaden=40;
    public float attackRate = 2;
    float nextAttackTime = 0f;


    void Update()
    {   
        //Movement        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //Kampf shit
        if(Time.time >= nextAttackTime)
        {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            nextAttackTime= Time.time + 1f / attackRate;
        }
        }
        
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        //Animation
        animator.SetTrigger("Attack");
        //Attack enemies
        Collider2D [] hitEnemies= Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemyLayers);

        //dmg to enemies
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Hit"+enemy.name);
            enemy.GetComponent<fightable>().TakeDMG(Schaden);
        }
    } 

    
    void OnDrawGizmosSelected()
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }   


}
