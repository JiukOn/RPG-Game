using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieStats : CharacterStats
{
    public override void Die(){
        base.Die();

        Destroy(gameObject);
    }
}
