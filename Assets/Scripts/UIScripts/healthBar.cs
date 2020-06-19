using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Health bar script that sets the max value of the bar and in game time changes the bar color and fill depending on the values
/// Uses a gradient of a color to change the color to closer to full it gets.
/// These are set in the UI_interaction Visualization script
/// </summary>

public class healthBar : MonoBehaviour
{

    public Image barfill;
    public Gradient gradient;

    float maxValue;

    //public Sprite[] arrayOfSprites; // 0 - good, 1 - medium, 2- bad

    

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
