using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Example_NPC : MonoBehaviour
{
    private bool redet=false;

    public TMP_Text text;    

    public void talk()
    {
        if (redet==false)
        {
            Debug.Log("Hallo");
            text.text = "Hallo";
            redet = true;
        }
        else
        {
            text.text = "";
            redet = false;
        }   
    }    



}
