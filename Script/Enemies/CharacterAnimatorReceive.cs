using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorReceive : MonoBehaviour
{
    public CharacterCombat combat;
    public void AttackHitEvent(){
        combat.AttackHit_AnimationEvent();
    }
}
