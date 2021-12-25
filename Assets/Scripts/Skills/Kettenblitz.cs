using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettenblitz : MonoBehaviour
{

    private Player caster;
    private void Awake(){
        caster = Player.getInstance();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
