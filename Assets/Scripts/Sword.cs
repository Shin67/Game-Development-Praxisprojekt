using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("BOOOOM");
        if (collision.gameObject.tag == "Enemy")
        {
            
            collision.gameObject.GetComponent<Enemy>().TakeDmg(10);
        }
    }
}
