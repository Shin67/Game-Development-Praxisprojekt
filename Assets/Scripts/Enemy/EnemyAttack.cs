using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.healthPoints -= GetComponent<Enemy>().attack;
            if (GetComponent<Transform>().position.x > collision.gameObject.GetComponent<Transform>().position.x)
            {
                collision.gameObject.GetComponent<Transform>().Translate(-2,0,0);
            } else if (GetComponent<Transform>().position.y > collision.gameObject.GetComponent<Transform>().position.y)
            {
                collision.gameObject.GetComponent<Transform>().Translate(0, -2, 0);
            } else if (GetComponent<Transform>().position.x < collision.gameObject.GetComponent<Transform>().position.x)
            {
                collision.gameObject.GetComponent<Transform>().Translate(2, 0, 0);
            }
            else
            {
                collision.gameObject.GetComponent<Transform>().Translate(0, 2, 0);
            }
        }
    }
}
