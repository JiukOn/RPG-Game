using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region  Singleton
    public static Inventory instance;

    private void Awake(){
        if(instance != null){
            Debug.LogWarning("Mais de uma instancia de Inventário foram encontradas");
            return;
        }
    instance = this;
    }
    #endregion

    public delegate void OnItemChaged();
    public OnItemChaged onItemChagedCallback;
    public List<Item> items = new List<Item>();
    public int space = 20;

    public bool Add (Item item){
        if(!item.isDefaultItem){
            if(items.Count >= space){
                //Debug.Log("O inventário esta cheio.");
                return false;
            }
            items.Add(item);

            if(onItemChagedCallback != null){
                onItemChagedCallback.Invoke();
                //Debug.Log("Mudança feita! Item Adicionado");
            }
        }
        return true;
    }

    public void Remove(Item item){
        items.Remove(item);
         if(onItemChagedCallback != null){
                onItemChagedCallback.Invoke();
                //Debug.Log("Mudança feita! Item Removido");
         }
    }
}
