using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI counter;

    // Start is called before the first frame update
    void Start()
    {
        // image = GetComponentInChildren<Image>();
        // counter = GetComponentInChildren<TextMeshProUGUI>();
        SetFill(1f, 1f);
        SetVisible(false);
    }

    public void SetVisible(bool set)
    {
        gameObject.SetActive(set);
    }

    public void SetFill(float total, float remaining)
    {
        float percent = remaining / total;
        image.fillAmount = percent;
        counter.text = Mathf.CeilToInt(remaining).ToString();
        // if (percent < 0.25f)
        // {
        //     image.color = Color.red;
        // }
        // else if (percent < 0.5f)
        // {
        //     image.color = Color.yellow;
        // }
        // else
        // {
        //     image.color = Color.green;
        // }
    }
}
