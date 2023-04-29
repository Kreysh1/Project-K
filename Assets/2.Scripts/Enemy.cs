using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("Damage")]
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [Range(1,3)]
    [SerializeField] private float criticalMutiplier;
    [Range(0,100)]
    [SerializeField] private int criticalChance;

    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target2Follow;


    private int finalDamage;
    private bool isCritical;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
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
            Player player = other.GetComponent<Player>();
            if(player != null){
                CalculateDamage(minDamage, maxDamage);
                player.TakeDamage(finalDamage, isCritical);
            }
        }
    }

    void ChaseTarget(){
        navMeshAgent.SetDestination(target2Follow.position);
    }

    private void CalculateDamage(int _minDamage, int _maxDamage){
        // Get actual damage value.
        finalDamage = Random.Range(_minDamage, _maxDamage);

        // Check if damage is critical.
        isCritical = Random.Range(0,100) < criticalChance ? true : false;

        // Multiply damage and criticalMutiplier.
        if(isCritical){
            finalDamage += (int)Mathf.Round(finalDamage * criticalMutiplier);
        }
    }
}
