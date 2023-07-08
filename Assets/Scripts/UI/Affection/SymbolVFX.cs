using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolVFX : MonoBehaviour
{

    private float _scaleXStart;
    private float _scaleYStart;
    [SerializeField] private float _scaleXEnd;
    [SerializeField] private float _scaleYEnd;
    [SerializeField] private float _scaleTime;
    [SerializeField] private float _delayTime;

    [SerializeField] private float _rotationZMax;
    [SerializeField] private float _rotationZMin;
    [SerializeField] private float _rotationTime;

    [SerializeField] private bool _isRotatingRightStart;



    IEnumerator ScaleUp()
    {
        yield return new WaitForSeconds(_delayTime);
        float t = 0;
        while (t < _scaleTime)
        {
            float scaleX = Mathf.Lerp(_scaleXStart, _scaleXEnd, t / _scaleTime);
            float scaleY = Mathf.Lerp(_scaleYStart, _scaleYEnd, t / _scaleTime);
            transform.localScale = new Vector3(scaleX, scaleY, 1);

            float rotationZ = Mathf.Lerp(_rotationZMin, _rotationZMax, t / _rotationTime);
            transform.rotation = Quaternion.Euler(0, 0, rotationZ * (_isRotatingRightStart ? 1 : -1));
            t += Time.deltaTime;

            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(_delayTime);
        float t = 0;
        while (t < _scaleTime)
        {
            float scaleX = Mathf.Lerp(_scaleXEnd, _scaleXStart, t / _scaleTime);
            float scaleY = Mathf.Lerp(_scaleYEnd, _scaleYStart, t / _scaleTime);
            transform.localScale = new Vector3(scaleX, scaleY, 1);

            float rotationZ = Mathf.Lerp(_rotationZMax, _rotationZMin, t / _rotationTime);
            transform.rotation = Quaternion.Euler(0, 0, rotationZ * (_isRotatingRightStart ? 1 : -1));
            
            t += Time.deltaTime;

            yield return null;
        }
        StartCoroutine(ScaleUp());
    }

    // Start is called before the first frame update
    void Awake()
    {
        _scaleXStart = transform.localScale.x;
        _scaleYStart = transform.localScale.y;
    }

    void Start()
    {
        StartCoroutine(ScaleUp());
    }
}
