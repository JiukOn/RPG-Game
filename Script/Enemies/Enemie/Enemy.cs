using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

	PlayerManager playerManager;
	CharacterStats myStats;
	CharacterCombat playerCombat;

	void Start ()
	{
		playerManager = PlayerManager.instance;
		myStats = GetComponent<CharacterStats>();

		playerCombat = playerManager.player.GetComponent<CharacterCombat>();
	}

	public override void Interact()
	{
		base.Interact();
		if (playerCombat != null)
		{
			playerCombat.Attack(myStats);
		}
	}

}