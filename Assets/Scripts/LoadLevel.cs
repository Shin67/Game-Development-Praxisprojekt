using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    [SerializeField] string lvToLoad; //Name von dem nächsten level  

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if(collisionObject.tag =="Player")  //nur wenn en spieler und kein mob hereinläuft funzt es
        {
                loadScene();
        }
    }

    public void loadScene() //extra funktion, falls mal animation und co dazukommen
    {
        Application.LoadLevel(lvToLoad);
    }
}
