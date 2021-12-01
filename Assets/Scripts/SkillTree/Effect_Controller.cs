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

    public void setElementarpfeil(){
        player.elementarPfeil=true;
    }
    public void setElementarRegen(){
        player.elementarRegen=true;
    }
    public void setScharfschuss(){
        player.scharfschuss=true;
    }
    public void setElementarHieb(){
        player.elementarhieb=true;
    }
    public void setElementarWirbel(){
        player.elementarwirbel=true;
    }
    public void setRage(){
        player.rage=true;
    }
    public void setElementarBall(){
        player.elementarball=true;
    }
    public void setElementarflaeche(){
        player.elementarflaeche=true;
    }
    public void setSturmkette(){
        player.sturmkette=true;
    }
}
