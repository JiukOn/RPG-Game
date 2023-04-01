using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;

	public override void Interact()
	{
		base.Interact();

		PickUp();
	}

	void PickUp(){
		//Debug.Log("Pickup " + item.name);
		bool pickup = Inventory.instance.Add(item);
		if(pickup){
			Destroy(gameObject);
		}
	}
}
