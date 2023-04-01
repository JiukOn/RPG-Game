using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0.5f,5f)]
    public float MaxLifetime;
    private float lifetime;

    private bool isAlive;

    public Transform playerTransform;
    public ParticleSystem Particles;

    
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        isAlive = true;
    }

    void Update()
    {
        IsAlive();
        if(isAlive == false){
            DestroyProjectile();
        }
    }

    private void DestroyProjectile(){

        ParticleSystem.Instantiate(Particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void IsAlive(){
        lifetime += 1*Time.deltaTime;

        if(lifetime >= MaxLifetime){
            isAlive = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        isAlive = false;

        if(other.gameObject.CompareTag("Player")){
        }
    }
}
