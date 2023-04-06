using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.J)){
            TakeDamage(10);
        }
    }

    private void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int _damage){
        currentHealth -= _damage;
        healthBar.SetHealth(currentHealth);
    }
}
