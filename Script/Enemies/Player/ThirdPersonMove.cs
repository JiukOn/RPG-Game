using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform cam;
    public PlayerController controller;

    float turnSmoothTime = 0.1f;
    float turnSmoothVel;

    Vector3 movement;
    Vector3 moveDestination;
    Vector3 moveDir;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<PlayerController>();
    }
    void Update()
    {
        if(controller.focus != null){
            return;
        }
        
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");
        
        movement = new Vector3(horInput, 0f, verInput).normalized;
        
        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVel,turnSmoothTime);
        moveDir = Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;

        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
            MoveDestination();
            transform.rotation = Quaternion.Euler(0f,angle,0f);
        }

        
    }

    void MoveDestination(){
        moveDestination = transform.position + moveDir;
        agent.destination = moveDestination;
    }
}
