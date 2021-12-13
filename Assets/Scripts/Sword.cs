using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int attackDmg;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject player = GameObject.FindWithTag("Player");
            var dmg = attackDmg + player.GetComponent<Player>().atk;
            collision.gameObject.GetComponent<Enemy>().TakeDmg(dmg);
        }
    }
}
