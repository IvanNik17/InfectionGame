﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Helper class for changing the UI text boxes for the graph screen. The script is called when the graph event is called, as well as when the slider is changed
/// </summary>

public class changeTextForGraphs : MonoBehaviour
{

    public GameObject textInfected;
    public GameObject textHospitalized;
    public GameObject textHappiness;

    public int dateToShow = 0;

    public GameObject timeSlider;


    private void Awake()
    {
        GlobalEvents.current.onShowStatisticsEvent += changeText;

        dateToShow = globalTimer.current.daysPassed;

        timeSlider.GetComponentInChildren<Text>().text = "Day: " + dateToShow;
    }


    public void changeTextDate(float currDay)
    {
        dateToShow = (int)currDay;

        changeText();

        timeSlider.GetComponentInChildren<Text>().text = "Day: " + dateToShow;

    }


    void changeText()
    {
        textInfected.GetComponent<Text>().text = "At day <b>" + dateToShow + "</b> \n there were <b>" + globalScoreKeeper.current.infectedEachDay[Mathf.Clamp(dateToShow - 1,0,99999)] + "</b> infected";

        textHospitalized.GetComponent<Text>().text = "At day <b>" + dateToShow + "</b> \n there were <b>" + globalScoreKeeper.current.inHospitalEachDay[Mathf.Clamp(dateToShow - 1, 0, 99999)] + "</b> hospitalized";

        textHappiness.GetComponent<Text>().text = "Depression level for different grades at day <b>" + dateToShow + "</b>";
    }

}
