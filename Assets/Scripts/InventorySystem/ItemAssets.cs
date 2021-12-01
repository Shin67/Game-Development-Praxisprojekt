using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance {get; private set;}
    //Sprites für die items aus der enum in klasse item
    public Sprite swordSprite;
    public Sprite HealPotionSprite;

    public Transform pfItemWorld;



    private void Awake()
    {
        Instance=this;
    }
}
