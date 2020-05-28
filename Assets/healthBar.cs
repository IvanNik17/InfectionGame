using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Image barfill;
    public Gradient gradient;

    float maxValue; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMaxValue(float setMaxValue)
    {
        maxValue = setMaxValue;
        barfill.color = gradient.Evaluate(1f);
    }

    public void setCurrValue(float currValue)
    {
        barfill.fillAmount = currValue / maxValue;

        barfill.color = gradient.Evaluate(currValue / maxValue);
    }
}
