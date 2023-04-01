using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
   public NavMeshAgent agent;
   public Transform player;
   public GameObject Projectile;
   public LayerMask whatIsGround, whatIsPlayer;

   //Patroling
   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;

   //Attacking
   public float timeBetweenAttacks;
   bool alreadyAttacked;

   //States
   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;

   //Stats
   [Range(0,100)]
   public float health;

   private void Awake()
   {
       player = GameObject.Find("Player").transform;
       agent = GetComponent<NavMeshAgent>();
   }

   private void Update()
   {
      playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
      playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

      if(!playerInSightRange && !playerInAttackRange){Patroling();}
      if(playerInSightRange && !playerInAttackRange){ChasePlayer();}
      if(playerInSightRange && playerInAttackRange){AttackPlayer();}
   }

   private void Patroling(){
      if(!walkPointSet) { SearchWalkPoint(); }
      if(walkPointSet){
         agent.SetDestination(walkPoint);
      }

      Vector3 distanceToWalkPoint = transform.position - walkPoint;

      if(distanceToWalkPoint.magnitude < 1f){
         walkPointSet = false;
      }
   }

   private void SearchWalkPoint(){
      float randomZ = Random.Range(-walkPointRange, walkPointRange);
      float randomX = Random.Range(-walkPointRange, walkPointRange);

      walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

      if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
         walkPointSet = true;
      }
   }
   private void ChasePlayer(){
      agent.SetDestination(player.position);
   }

   private void AttackPlayer(){
      agent.SetDestination(transform.position);
      transform.LookAt(player);

      if(!alreadyAttacked){

         Rigidbody rb = Instantiate(Projectile, transform.position,Quaternion.identity).GetComponent<Rigidbody>();
         rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
         rb.AddForce(transform.up * 8f, ForceMode.Impulse);

         alreadyAttacked = true;
         Invoke(nameof(ResetAttack),timeBetweenAttacks);
      }
   }

   private void ResetAttack(){
      alreadyAttacked = false;

   }

   public void TakeDamge(int damage){
      health -= damage;

      if(health <= 0){
         Invoke(nameof(DestroyEnemy), 0.5f);
      }
   }

   private void DestroyEnemy(){
      Destroy(gameObject);
   }

   private void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, attackRange);
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, sightRange);
   }
}
