using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global code for keeping the score for the current level only. On awake it takes the between scene static values from the KeepDataBetweenLevels script.
/// The script contains two methods:
/// - gatherStatisticalData - gather summed up data for the end screen and user data gathering
/// - countClosedDays - aggregates the amount of time each school has been closed per level
/// </summary>

public class globalScoreKeeper : MonoBehaviour
{
    public static globalScoreKeeper current;

    private int currentLevel = 1;

    public int numberHealthySociety = 0;
    public int numberSickSociety = 0;

    public int numberHealthyHospital = 0;
    public int numberSickHospital = 0;


    public int maxSickSociety = 50;
    public int maxHospitalCapacity = 30;
    public int maxAmbulanceCapacity = 5;



    public GameObject[] allSpawners;

    public int[] daysEachSchoolClosed;


    public float percentHealthyToSick = 0.5f;

    public int maxDaysSchoolClosed = 0;


    public int countHospitalized;


    public List<float> infectedEachDay;

    public List<float> inHospitalEachDay;


    public List<List<float>> happinessClassesEachDay;



    private void Awake()
    {
        current = this;

        currentLevel = KeepDataBetweenLevels.keepCurrentLevel;
        percentHealthyToSick = KeepDataBetweenLevels.keepPercentHealthyToSick;
        maxHospitalCapacity = KeepDataBetweenLevels.keepMaxHospitalCapacity;
        maxSickSociety = KeepDataBetweenLevels.keepMaxSickSociety;

        maxAmbulanceCapacity = KeepDataBetweenLevels.keepMaxAmbulanceCapacity;

        maxDaysSchoolClosed = KeepDataBetweenLevels.keepMaxPossibleSchoolClose;

        for (int i = 0; i < allSpawners.Length; i++)
        {
            allSpawners[i].GetComponent<spawner>().maxDaysClosed = maxDaysSchoolClosed;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        infectedEachDay = new List<float>();
        inHospitalEachDay = new List<float>();

        happinessClassesEachDay = new List<List<float>>();

        for (int i = 0; i < allSpawners.Length; i++)
        {
            happinessClassesEachDay.Add(new List<float>());
        }

        daysEachSchoolClosed = new int[allSpawners.Length];

        GlobalEvents.current.onDayPassed += countClosedDays;

        GlobalEvents.current.onDayPassed += gatherStatisticalData;
    }


    void gatherStatisticalData()
    {
        infectedEachDay.Add(numberSickSociety);

        inHospitalEachDay.Add(numberSickHospital);


        for (int i = 0; i < allSpawners.Length; i++)
        {

            List<float> currClassList = happinessClassesEachDay[i];

            currClassList.Add(allSpawners[i].GetComponent<spawner>().daysClosed);

            happinessClassesEachDay[i] = currClassList;
        }

    }


    void countClosedDays()
    {
        for (int i = 0; i < allSpawners.Length; i++)
        {

            if (!allSpawners[i].GetComponent<spawner>().isOpen)
            {
                daysEachSchoolClosed[i] += 1;
            }
        }
    }

}
