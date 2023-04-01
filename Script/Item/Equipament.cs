using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipament",menuName = "Inventory/Equipable")]
public class Equipament : Item
{
    public EquipamentSlot equipSlot;
    public SkinnedMeshRenderer mesh;

    public int armorMod;
    public int damageMod;

    public override void Use()
    {
        base.Use();
        EquipamentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipamentSlot {Head, Chest, Legs, Weapon, Shield, Hands, Feet, Special}