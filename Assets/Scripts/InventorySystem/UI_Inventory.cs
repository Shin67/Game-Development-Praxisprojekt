using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private int rowCap =4;          //wie viele items sollen in einer reihe angezeigt werden?
    private bool isDrop=false;

    public Player player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void setInventory(Inventory newInventory)
    {
        this.inventory=newInventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue ;     
            Destroy(child.gameObject);            
        }

        int x = 0;
        int y = 0;
        float cellSize = 30f;

        foreach(Item item in inventory.getItemList())
        {
            
            RectTransform itemSlotRectTransform =  Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x*cellSize, y*cellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();//sprite aus den assets holen
            /*TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();    //mengenangabe refreshen
            if(item.amount>1)
            {
                uiText.SetText(item.amount.ToString());

            }else{
                uiText.SetText("");

            }*/

            Button myButton = itemSlotRectTransform.Find("button").GetComponent<Button>();
             
            myButton.onClick.AddListener(() => dropOrUse(item));      
            x++;
            if(x>rowCap)    //wenn reihe voll dann die darunter befüllen
            {
                x=0;
                y++;
            }
        }
    }

    public void setisDrop()
    {
        isDrop = !isDrop;
        Debug.Log(isDrop);
    }

    private void dropOrUse(Item item)
    {
        if(isDrop)
        {
        Debug.Log("dropitem");
        Item duplicate = new Item {itemtype=item.itemtype, amount=item.amount};
        inventory.RemoveItem(item);
        ItemWorld.DropItem(player.position, duplicate);
        }else{
        Debug.Log("use");
        inventory.UseItem(item);
        }
        
    }

}
