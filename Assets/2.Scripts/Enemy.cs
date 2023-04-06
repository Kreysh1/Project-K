using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage;

    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        damage = Random.Range(10, 30);
        currentHealth = maxHealth;
    }

    private void Update() {
        if(currentHealth <= 0){
            GameObject.Destroy(this.gameObject);
        }
        ChaseTarget();
    }

    void TakeDamage(int _damage){
        currentHealth -= _damage;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            print("Player Tag Triggered");
            if(healthSystem != null){
                healthSystem.TakeDamage(damage);
            }
        }
    }

    void ChaseTarget(){
        navMeshAgent.SetDestination(target.position);
        // transform.LookAt(target.position);
    }
}
