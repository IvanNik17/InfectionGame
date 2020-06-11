using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalScoreKeeper : MonoBehaviour
{
    public static globalScoreKeeper current;

    public int currentLevel = 1;

    public int numberHealthySociety = 0;
    public int numberSickSociety = 0;

    public int numberHealthyHospital = 0;
    public int numberSickHospital = 0;


    public int maxSickSociety = 50;
    public int maxHospitalCapacity = 30;
    public int maxAmbulanceCapacity = 5;

    public int maxNumStudentsInSchools = 20;

    public GameObject[] allSpawners;

    public int[] daysEachSchoolClosed;


    public float percentHealthyToSick = 0.5f;

    public int maxDaysSchoolClosed = 0;

    

    public int countHospitalized;


    public List<float> infectedEachDay;

    public List<float> inHospitalEachDay;

    //public List<int> happinessEachDay_1_3;
    //public List<int> happinessEachDay_4_6;
    //public List<int> happinessEachDay_7_9;
    //public List<int> happinessEachDay_10_12;

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
            allSpawners[i].GetComponent<spawner>().maxNumStudents = maxNumStudentsInSchools;
            allSpawners[i].GetComponent<spawner>().maxDaysClosed = maxDaysSchoolClosed;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //currentLevel = KeepDataBetweenLevels.keepCurrentLevel;
        //percentHealthyToSick = KeepDataBetweenLevels.keepPercentHealthyToSick;
        //maxHospitalCapacity = KeepDataBetweenLevels.keepMaxHospitalCapacity;
        //maxSickSociety = KeepDataBetweenLevels.keepMaxSickSociety;

        //maxDaysSchoolClosed = KeepDataBetweenLevels.keepMaxPossibleSchoolClose;

        //maxNumStudentsInSchools = KeepDataBetweenLevels.keepMaxNumStudentsInSchools;

        infectedEachDay = new List<float>();
        inHospitalEachDay = new List<float>();

        happinessClassesEachDay = new List<List<float>>();

        for (int i = 0; i < allSpawners.Length; i++)
        {
            happinessClassesEachDay.Add(new List<float>());
        }

        //allSpawners = GameObject.FindGameObjectsWithTag("spawners");

        daysEachSchoolClosed = new int[allSpawners.Length];
        


        //for (int i = 0; i < daysEachSchoolClosed.GetLength(1); i++)
        //{
        //    daysEachSchoolClosed[0, i] = allSpawners[i].GetComponent<spawner>().spawnerNumber;


        //}

        GlobalEvents.current.onDayPassed += countClosedDays;


        GlobalEvents.current.onDayPassed += gatherStatisticalData;
    }

    // Update is called once per frame
    void Update()
    {

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
