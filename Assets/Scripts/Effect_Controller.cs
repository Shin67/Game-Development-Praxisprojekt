using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Controller : MonoBehaviour
{
    [SerializeField] Player player;

    //Effekte des Skilltrees
    public void increaseDEX(){
        player.dex+=10;
    }
    public void increaseSTR(){
        player.str+=10;
    }
    public void increaseINT(){
        player.inte+=10;
    }
}
