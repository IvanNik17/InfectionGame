using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Mental bar script that sets the max and current values of the mental health/happiness for each school bar.
/// These are set in the UI_interaction Visualization script
/// In addition it changes the face and plays animations when the values get closer to maximum.
/// </summary>

public class mentalBar : MonoBehaviour
{


    public Image barfill;
    public Gradient gradient;

    float maxValue;

    public Sprite[] arrayOfSprites; // 0 - good, 1 - medium, 2- bad

    public Image mentalImage;


    public void setMaxValueMental(float setMaxValue)
    {
        maxValue = setMaxValue;
        barfill.color = gradient.Evaluate(1f);
    }

    public void setCurrValueMental(float currValue)
    {
        barfill.fillAmount = currValue / maxValue;

        barfill.color = gradient.Evaluate(currValue / maxValue);


        mentalImage.GetComponent<Animator>().SetFloat("attentionDepression", (currValue / maxValue));

        if ((currValue / maxValue) <= 0.25)
        {
            mentalImage.sprite = arrayOfSprites[0];
        }
        else if ((currValue / maxValue) > 0.25 && (currValue / maxValue) <= 0.60)
        {
            mentalImage.sprite = arrayOfSprites[1];
        }
        else if ((currValue / maxValue) > 0.60)
        {
            mentalImage.sprite = arrayOfSprites[2];
        }

    }
}
