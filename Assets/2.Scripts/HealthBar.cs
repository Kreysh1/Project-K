using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    // public Gradient gradient;
    // public Image fill;

    public void SetHealth(int _health){
        slider.value = _health;

        // fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int _health){
        slider.value = _health;
        slider.maxValue = _health;

        // fill.color = gradient.Evaluate(1f);
    }
}
