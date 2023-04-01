using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    InventorySlot[] slots;

    public Transform itemsParent;
    public GameObject ThisUI;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChagedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            ThisUI.SetActive(!ThisUI.activeSelf);
        }
    }

    void UpdateUI ()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count){
				slots[i].AddItem(inventory.items[i]);
            }
			else{
				slots[i].ClearSlot();
			}
		}
	}
}
