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



    private void Awake()
    {
        current = this;

        currentLevel = KeepDataBetweenLevels.keepCurrentLevel;
        percentHealthyToSick = KeepDataBetweenLevels.keepPercentHealthyToSick;
        maxHospitalCapacity = KeepDataBetweenLevels.keepMaxHospitalCapacity;
        maxSickSociety = KeepDataBetweenLevels.keepMaxSickSociety;

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



        //allSpawners = GameObject.FindGameObjectsWithTag("spawners");

        daysEachSchoolClosed = new int[allSpawners.Length];

        

        //for (int i = 0; i < daysEachSchoolClosed.GetLength(1); i++)
        //{
        //    daysEachSchoolClosed[0, i] = allSpawners[i].GetComponent<spawner>().spawnerNumber;

            
        //}

        GlobalEvents.current.onDayPassed += countClosedDays;
    }

    // Update is called once per frame
    void Update()
    {
        
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
