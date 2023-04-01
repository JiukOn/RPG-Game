using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    public Image icon;
    public Button RemoveButton;

    public void AddItem (Item newItem){
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        RemoveButton.interactable = true;

        //Debug.Log("Novo item adicionado:" + item.name);
    }

    public void ClearSlot(){
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        RemoveButton.interactable = false;
        
         //Debug.Log("Slot limpo");        
    }

    public void onRemoveButton(){
        Inventory.instance.Remove(item);
    }

    public void UseItem(){
        if(item != null){
            item.Use();
        }
    }
}
