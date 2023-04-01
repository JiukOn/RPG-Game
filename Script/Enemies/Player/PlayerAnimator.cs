using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimator : CharacterAnimator {

	public WeaponAnim[] weaponAnim;
	Dictionary<Equipament, AnimationClip[]> weaponAnimDict;
	protected override void Start(){
		EquipamentManager.instance.OnEquipamentChanged += OnEquipamentChanged;
		weaponAnimDict = new Dictionary<Equipament, AnimationClip[]>();

		foreach(WeaponAnim a in weaponAnim){
			weaponAnimDict.Add(a.weapon, a.clips);
		}

		combat.OnAttack += OnAttack;
		return;
	}
void OnEquipamentChanged(Equipament newItem, Equipament oldItem){
	if(newItem != null && newItem.equipSlot == EquipamentSlot.Weapon){
		animator.SetLayerWeight(1,1);
		if(weaponAnimDict.ContainsKey(newItem)){
			currentAnimSet = weaponAnimDict[newItem];
		}
	}
	else if(newItem == null && oldItem != null && oldItem.equipSlot == EquipamentSlot.Weapon){
		animator.SetLayerWeight(1,0);
		currentAnimSet = defaultAnimSet;
	}

	if(newItem != null && newItem.equipSlot == EquipamentSlot.Shield){
		animator.SetLayerWeight(1,1);
	}
	else if(newItem == null && oldItem != null && oldItem.equipSlot == EquipamentSlot.Shield){
		animator.SetLayerWeight(1,0);
	}
}

[System.Serializable]
public struct WeaponAnim{
	public Equipament weapon;
	public AnimationClip[] clips;
}
}