using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        textInfected.GetComponent<Text>().text = "At day " + dateToShow + " there were " + globalScoreKeeper.current.infectedEachDay[Mathf.Clamp(dateToShow - 1,0,99999)] + " infected";

        textHospitalized.GetComponent<Text>().text = "At day " + dateToShow + " there were " + globalScoreKeeper.current.inHospitalEachDay[Mathf.Clamp(dateToShow - 1, 0, 99999)] + " hospitalized";

        textHappiness.GetComponent<Text>().text = "Mental health for the classes at day " + dateToShow;
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
