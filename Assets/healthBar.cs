using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Image barfill;
    public Gradient gradient;

    float maxValue;

   // public Sprite[] arrayOfSprites; // 0 - good, 1 - medium, 2- bad

   

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

       
        //if ( (currValue / maxValue) <= 0.20)
        //{
        //    mentalHealthImage.GetComponent<Image>().sprite = arrayOfSprites[0];
        //}
        //else if ((currValue / maxValue) > 0.20 && (currValue / maxValue) <= 0.70)
        //{
        //    mentalHealthImage.GetComponent<Image>().sprite = arrayOfSprites[1];
        //}
        //else if ((currValue / maxValue) > 0.70)
        //{
        //    mentalHealthImage.GetComponent<Image>().sprite = arrayOfSprites[2];
        //}

    }
}
