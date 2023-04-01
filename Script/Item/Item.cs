using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Base")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    public bool isDefaultItem = false;

    public virtual void Use(){
        //Debug.Log("Usando:" + name);
    }
    
    public void RemoveFromInventory(){
        Inventory.instance.Remove(this);
    }
}
