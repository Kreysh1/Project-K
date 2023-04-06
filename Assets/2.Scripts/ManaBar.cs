using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    // public Gradient gradient;
    // public Image fill;

    public void SetMana(int _health){
        slider.value = _health;

        // fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxMana(int _health){
        slider.value = _health;
        slider.maxValue = _health;

        // fill.color = gradient.Evaluate(1f);
    }
}
