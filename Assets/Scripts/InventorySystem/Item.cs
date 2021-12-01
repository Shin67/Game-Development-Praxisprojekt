using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
   public enum Itemtype
   {
       Sword,
       HealPotion
   }

   public Itemtype itemtype;
   public int amount;


   public Sprite GetSprite()
   {
       switch (itemtype)
       {           
           default:
           case Itemtype.Sword:         return ItemAssets.Instance.swordSprite;
           case Itemtype.HealPotion:    return ItemAssets.Instance.HealPotionSprite;
       }

   }

   public bool IsStackable() //hier für alle aus dem enum festlegen
   {
       switch (itemtype)
       {           
           default:
           case Itemtype.Sword:         return false;
           case Itemtype.HealPotion:    return true;
       }
   }


}
