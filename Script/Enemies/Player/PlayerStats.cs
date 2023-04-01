using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles the players stats and adds/removes modifiers when equipping items. */

public class PlayerStats : CharacterStats {

	// Use this for initialization
	void Start () {
		EquipamentManager.instance.OnEquipamentChanged += OnEquipamentChanged;
	}
	
	// Called when an item gets equipped/unequipped
	void OnEquipamentChanged (Equipament newItem, Equipament oldItem)
	{
		// Add new modifiers
		if (newItem != null)
		{
			armor.AddModifier(newItem.armorMod);
			damage.AddModifier(newItem.damageMod);
		}

		// Remove old modifiers
		if (oldItem != null)
		{
			armor.RemoveModifier(oldItem.armorMod);
			damage.RemoveModifier(oldItem.damageMod);
		}
		
	}

	public override void Die()
	{
		base.Die();
		PlayerManager.instance.KillPlayer();
	}
}