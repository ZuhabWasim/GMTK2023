using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisdainTest : MonoBehaviour
{
    [SerializeField] private float _changeAmount = 25f;
    [SerializeField] private Disdain _disdain;
    [SerializeField] private TextMeshProUGUI _textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        _textMesh.text = _changeAmount.ToString();
    }

    // Update is called once per frame
    public void OnClick(){
        if (_changeAmount >= 0f){
            _disdain.Heal(_changeAmount);
        } else {
            _disdain.BeDamaged(-_changeAmount);
        }
        _changeAmount = Random.Range(-10f, 10f);
    }
}
