using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
   public enum Itemtype
   {
       HealPotionNormal,
       HealPotionGroß,
       ManaTrank,
       ManaTrankGroß,
       MettBrot,
       Blätterwasser,
       LederRüstung,
       KettenRüstung,
       PlattenstahlRüstung,
       HolzSchild,
       EisenSchild,
       StahlSchild,
       BeginnerBogen,
       JägerBogen,
       Akolythenstab,
       Elementarstab,
       MeisterStab,
       Schwert,
       KampfAxt,
       StreitKolben

   }

   public Itemtype itemtype;
   public int amount;


   public Sprite GetSprite()
   {
       switch (itemtype)
       {           
           
           default:
           case Itemtype.HealPotionNormal:         return ItemAssets.Instance.HealPotionNormal;
           case Itemtype.HealPotionGroß:    return ItemAssets.Instance.HealPotionGroß;
           case Itemtype.ManaTrank:    return ItemAssets.Instance.ManaTrank;
           case Itemtype.ManaTrankGroß:    return ItemAssets.Instance.ManaTrankGroß;
           case Itemtype.MettBrot:    return ItemAssets.Instance.MettBrot;
           case Itemtype.Blätterwasser:         return ItemAssets.Instance.Blätterwasser;
           case Itemtype.KettenRüstung:    return ItemAssets.Instance.KettenRüstung;
           case Itemtype.PlattenstahlRüstung:    return ItemAssets.Instance.PlattenstahlRüstung;
           case Itemtype.HolzSchild:    return ItemAssets.Instance.HolzSchild;
           case Itemtype.EisenSchild:    return ItemAssets.Instance.EisenSchild;
           case Itemtype.StahlSchild:         return ItemAssets.Instance.StahlSchild;
           case Itemtype.BeginnerBogen:    return ItemAssets.Instance.BeginnerBogen;
           case Itemtype.JägerBogen:    return ItemAssets.Instance.JägerBogen;
           case Itemtype.Akolythenstab:    return ItemAssets.Instance.Akolythenstab;
           case Itemtype.Elementarstab:    return ItemAssets.Instance.Elementarstab;
           case Itemtype.MeisterStab:         return ItemAssets.Instance.MeisterStab;
           case Itemtype.Schwert:    return ItemAssets.Instance.Schwert;
           case Itemtype.KampfAxt:    return ItemAssets.Instance.KampfAxt;
           case Itemtype.StreitKolben:    return ItemAssets.Instance.StreitKolben;
       }

   }

   public bool IsStackable() //hier für alle aus dem enum festlegen
   {
       switch (itemtype)
       {           
           default:
           case Itemtype.HealPotionNormal:         return true;
           case Itemtype.HealPotionGroß:    return true;
           case Itemtype.ManaTrank:    return true;
           case Itemtype.ManaTrankGroß:    return true;
           case Itemtype.MettBrot:    return true;
           case Itemtype.Blätterwasser:         return true;
           case Itemtype.KettenRüstung:    return false;
           case Itemtype.PlattenstahlRüstung:    return false;
           case Itemtype.HolzSchild:    return false;
           case Itemtype.EisenSchild:    return false;
           case Itemtype.StahlSchild:         return false;
           case Itemtype.BeginnerBogen:    return false;
           case Itemtype.JägerBogen:    return false;
           case Itemtype.Akolythenstab:    return false;
           case Itemtype.Elementarstab:    return false;
           case Itemtype.MeisterStab:         return false;
           case Itemtype.Schwert:    return false;
           case Itemtype.KampfAxt:    return false;
           case Itemtype.StreitKolben:    return false;
           
           
       }
   }


}
