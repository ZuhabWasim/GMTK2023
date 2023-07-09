using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisdainDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider _display; // Health bar in the UI

    [SerializeField] private float _lerpMagnificationVar = 1f;
    [SerializeField] private float _lerpDuration = 1f;
    [SerializeField] private Disdain _disdain;

    [SerializeField] private Color _disdainColor;
    [SerializeField] private Color _affectionColor;

    [SerializeField] private float speedOfChange = 1f;
    [SerializeField] private float exponentialModifier = 1f;

    public Color targetColor;
    public Image FillImage;

    private float _curHealthPercent;


    // Start is called before the first frame update
    void Start()
    {
        if (_disdain){
            _disdain.HealthChanged += OnHealthChange;
            _disdain.Death += OnDeath;

            // Record the max disdain
            _display.maxValue = _disdain.maxHealth;
        }
        _curHealthPercent = _disdain.health / _disdain.maxHealth;

    }

    void Update(){
        //Display is being changed over time, so we want the color to change over time as well
        _curHealthPercent = _display.value / _disdain.maxHealth;
        // Lerp the color of the fill image
        targetColor = Color.Lerp(_affectionColor, _disdainColor, _curHealthPercent);
        targetColor.a = 1f;
        FillImage.color = targetColor;
    }


    void OnHealthChange(float oldHealth, float newHealth){
        StartCoroutine(LerpDisplay(oldHealth, newHealth, _lerpDuration));
    }

    private IEnumerator LerpDisplay(float start, float end, float duration){
        float startTime = Time.time;
        float endTime = startTime + duration;


        while (Time.time < endTime){
            // Ref: https://discussions.unity.com/t/vector3-lerp-slow-first-and-become-faster/128930
            float t = (Time.time - startTime) / duration;
            float stepAmount = Mathf.Pow (t * speedOfChange, exponentialModifier);
            _display.value = Mathf.Lerp(start, end, Mathf.MoveTowards(0f, 1f, stepAmount));

            yield return null;
        }
        _display.value = end;
    }

    void OnDeath(){

    }
}
