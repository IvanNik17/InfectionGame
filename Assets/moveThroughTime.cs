using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class moveThroughTime : MonoBehaviour
{

    public GameObject timeSlider;

    public GameObject managerObj;


    public void setMaxTime(int maxTime)
    {
        timeSlider.GetComponent<Slider>().maxValue = maxTime;

    }


    public void changeDay(float currDay)
    {
        managerObj.GetComponent<spawnPeople>().currDay = (int)currDay;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
