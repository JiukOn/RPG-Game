using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemieController : MonoBehaviour {

	[Range(1,10)]
	public float lookRadius = 10f;
	[Range(1,10)]
	public float walkPointRange = 3f; 
	public Vector3 walkPoint;
    bool walkPointSet;
	public LayerMask whatIsGround;

	Transform target;
	NavMeshAgent agent;
	CharacterCombat combat;

	CharacterStats targetStats;

	public float DistanceSetter = 1.5f;
	float randomX, randomZ;
	//int timeLoop = 6;

	void Start () {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		combat = GetComponent<CharacterCombat>();
		
		targetStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
		
	}
	
void Update () {
    float distance = Vector3.Distance(target.position, transform.position);
	Vector3 direction = (target.position - transform.position).normalized;

    if (distance <= lookRadius)
		{
			agent.SetDestination(target.position);
			NullValues();

			if (distance <= agent.stoppingDistance)
				{
					if (targetStats != null)
					{
						if (Vector3.Dot(transform.forward, direction) > 0.5f)
						{
							combat.Attack(targetStats);
						}
					}

					FaceTarget();
				}
		} else if (distance >= agent.stoppingDistance){
			Patroling();
		}
	}
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}


	private void Patroling(){
	  if(combat.InCombat == false){
		if(!walkPointSet) { SearchWalkPoint(); }
		if(walkPointSet){
			agent.SetDestination(walkPoint);
		}

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		if(distanceToWalkPoint.magnitude < DistanceSetter){
			walkPointSet = false;
		}
	  }
   }

   private void SearchWalkPoint(){
     randomZ = Random.Range(-walkPointRange, walkPointRange);
     randomX = Random.Range(-walkPointRange, walkPointRange);

      walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

      if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
         walkPointSet = true;
      }
   }

   void NullValues(){
			randomX = 0;
			randomZ = 0;
			walkPoint = new Vector3(0,0,0);
   }

}