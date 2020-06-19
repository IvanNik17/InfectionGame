using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script keeps track if any of the level end conditions are reached. If they are the variable whatEnd is changed and the event levelEndingEvent are called.
/// Possible game endings:
/// - the last day of the level is reached - level is won - whatEnd == 0
/// - the number of sick in the hospital exceeds maximum capacity - level is lost - whatEnd == 1
/// - the number of sick in society exceeds the maximum possible - level is lost - whatEnd == 2
/// - the depression in any closed school exceeds the maximum possible - level is lost - whatEnd == 3
/// </summary>

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
