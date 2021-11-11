using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public float radius =3f;

    public bool isInRange;
    public KeyCode interactionKey;
    public UnityEvent interactAction;


    void OnDrawGizmosSelected()
    {
        Gizmos.color =Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactionKey))
            {
            Debug.Log("Key gedrueckt");

                interactAction.Invoke();
            }

        }
    }

    public void bspMethode()
    {
        Debug.Log("das ist ein Beispiel");   
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange= true;
            //Debug.Log("in range");

        }
    }

     private void OnTriggerExit2D(Collider2D collision)
     {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange= false;
            //Debug.Log("nicht mehr in range ");
        }
    }
}
