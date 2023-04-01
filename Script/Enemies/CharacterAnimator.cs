using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {
	
	public AnimationClip repleacebleAnim;
	public AnimationClip[] defaultAnimSet;
	protected AnimationClip[] currentAnimSet;
	public Animator animator;

	[SerializeField]
	protected NavMeshAgent agent;
	[SerializeField]
	protected CharacterCombat combat;

	const float locAnim = .1f;
	public AnimatorOverrideController overrideControl;
	private int attackIndex;

	protected virtual void Start() {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		combat = GetComponent<CharacterCombat>();

		if(overrideControl == null){
		overrideControl = new AnimatorOverrideController(animator.runtimeAnimatorController);
		}
		animator.runtimeAnimatorController = overrideControl;

		currentAnimSet = defaultAnimSet;
	}

	protected virtual void Update () {	
		float speedPercent = agent.velocity.magnitude/agent.speed;
		animator.SetFloat ("speedPercent",speedPercent,locAnim,Time.deltaTime);

		animator.SetBool("inCombat", combat.InCombat);
	}

	protected virtual void OnAttack() {
		if(currentAnimSet != null){
		attackIndex = Random.Range(0, currentAnimSet.Length);
		overrideControl[repleacebleAnim.name] = currentAnimSet[attackIndex];
		 return;
		}
	}
}