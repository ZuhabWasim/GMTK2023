using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisdainDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider _display; // Health bar in the UI
    [SerializeField] private Disdain _disdain;
    // Start is called before the first frame update
    void Start()
    {
        if (_disdain){
            _disdain.HealthChanged += OnHealthChange;
            _disdain.Death += OnDeath;

            // Record the max disdain
            _display.maxValue = _disdain.maxHealth;
        }
    }

    void OnHealthChange(float oldHealth, float newHealth){
        _display.value = newHealth;
    }

    void OnDeath(){
        // TODO
    }
}
