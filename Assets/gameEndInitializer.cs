using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEndInitializer : MonoBehaviour
{

    public static gameEndInitializer current;

    public int whatEnd = -2;

    GameObject[] allSpawners;

    int prevWhatEnd;

    private void Awake()
    {
        current = this;

        prevWhatEnd = whatEnd;
    }


    // Start is called before the first frame update
    void Start()
    {
        allSpawners = GameObject.FindGameObjectsWithTag("spawners");
    }

    // Update is called once per frame
    void Update()
    {

        if (globalTimer.current.daysPassed >= globalTimer.current.maxDays)
        {
            whatEnd = 0;
        }

        if (globalScoreKeeper.current.numberSickHospital > globalScoreKeeper.current.maxHospitalCapacity)
        {
            whatEnd = 1;
        }

        if (globalScoreKeeper.current.numberSickSociety > globalScoreKeeper.current.maxSickSociety)
        {
            whatEnd = 2;
        }

        bool isSchoolOverflow = false;
        for (int i = 0; i < allSpawners.Length; i++)
        {
            if (allSpawners[i].GetComponent<spawner>().daysClosed > globalScoreKeeper.current.maxDaysSchoolClosed)
            {
                isSchoolOverflow = true;
            }
        }

        if (isSchoolOverflow)
        {
            whatEnd = 3;
        }

        //Debug.Log(whatEnd + " | " + prevWhatEnd);
        if (whatEnd != prevWhatEnd && whatEnd>=-1 && prevWhatEnd >= -1)
        {
            GlobalEvents.current.levelEndingEvent(whatEnd);
        }


        prevWhatEnd = whatEnd;
    }
}
