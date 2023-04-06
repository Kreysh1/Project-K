using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    private int maxMana = 100;
    public int currentMana;

    public ManaBar manaBar;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)){
            ConsumeMana(10);
        }
    }

    private void Start() {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    void ConsumeMana(int _mana){
        currentMana -= _mana;
        manaBar.SetMana(currentMana);
    }
}
