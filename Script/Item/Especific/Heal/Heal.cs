using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Item",menuName = "Inventory/Item/Heal")]
public class Heal : Item
{
     private CharacterStats myStats;

     [Range(5,50)]
     public int lifeHeal;
    public override void Use()
    {
        base.Use();
        myStats = PlayerManager.instance.playerStats;

        if(HealAvailable()){
            myStats.LifeHeal(lifeHeal);
            RemoveFromInventory();
        }
    }

    private bool HealAvailable(){ 
        if(myStats.currentHealth >= myStats.maxHealth){
            return false;
        }
        else if(myStats.currentHealth <= myStats.maxHealth){
            return true;
        }
        else{
            return false;
        }
    }
}
