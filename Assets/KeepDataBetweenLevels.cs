using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDataBetweenLevels : MonoBehaviour
{

    //40 , 0.5 ,0.5 , 1, 0.5 , 30, 40, 20

    public static int keepMaxDays = 20;
    public static float keepSpawnTime = 0.7f;
    public static float keepSlideTime = 0.7f;

    public static int keepCurrentLevel = 1;
    public static float keepPercentHealthyToSick = 0.4f;
    public static int keepMaxHospitalCapacity = 20;
    public static int keepMaxSickSociety = 30;
    public static int keepMaxNumStudentsInSchools = 15;


    public static int baseMaxDays;
    public static float baseSpawnTime;
    public static float baseSlideTime ;

    public static int baseCurrentLevel;
    public static float basePercentHealthyToSick;
    public static int baseMaxHospitalCapacity;
    public static int baseMaxSickSociety;
    public static int baseMaxNumStudentsInSchools;

    private void Awake()
    {
        if (keepCurrentLevel == 1)
        {
            baseMaxDays = keepMaxDays;
            baseSpawnTime = keepSpawnTime;
            baseSlideTime = keepSlideTime;

            baseCurrentLevel = keepCurrentLevel;
            basePercentHealthyToSick = keepPercentHealthyToSick;
            baseMaxHospitalCapacity = keepMaxHospitalCapacity;
            baseMaxSickSociety = keepMaxSickSociety;
            baseMaxNumStudentsInSchools = keepMaxNumStudentsInSchools;
        }
        

    }


    public static void nextLevel()
    {
        keepMaxDays += 10;
        keepSpawnTime -= 0.1f;
        keepSlideTime -= 0.1f;

        keepCurrentLevel += 1;
        keepPercentHealthyToSick += 0.05f;
        keepMaxHospitalCapacity += 5;
        keepMaxSickSociety += 5;
        keepMaxNumStudentsInSchools += 5;
    }

    public static void resetStaticVals()
    {
        keepMaxDays = baseMaxDays;
        keepSpawnTime = baseSpawnTime;
        keepSlideTime = baseSlideTime;

        keepCurrentLevel = baseCurrentLevel;
        keepPercentHealthyToSick = basePercentHealthyToSick;
        keepMaxHospitalCapacity = baseMaxHospitalCapacity;
        keepMaxSickSociety = baseMaxSickSociety;
        keepMaxNumStudentsInSchools = baseMaxNumStudentsInSchools;
    }

    public static void saveData()
    {

        PlayerPrefs.SetInt("maxDays", keepMaxDays);
        PlayerPrefs.SetFloat("spawnTime", keepSpawnTime);
        PlayerPrefs.SetFloat("slideTime", keepSlideTime);

        PlayerPrefs.SetInt("currentLevel", keepCurrentLevel);
        PlayerPrefs.SetFloat("percentHealthyToSick", keepPercentHealthyToSick);
        PlayerPrefs.SetInt("maxHospitalCapacity", keepMaxHospitalCapacity);
        PlayerPrefs.SetInt("maxSickSociety", keepMaxSickSociety);
        PlayerPrefs.SetInt("maxNumStudentsInSchools", keepMaxNumStudentsInSchools);
    }


    public static void loadData()
    {

        keepMaxDays = PlayerPrefs.GetInt("maxDays");
        keepSpawnTime = PlayerPrefs.GetFloat("spawnTime");
        keepSlideTime = PlayerPrefs.GetFloat("slideTime");

        keepCurrentLevel =  PlayerPrefs.GetInt("currentLevel");
        keepPercentHealthyToSick = PlayerPrefs.GetFloat("percentHealthyToSick");
        keepMaxHospitalCapacity = PlayerPrefs.GetInt("maxHospitalCapacity");
        keepMaxSickSociety = PlayerPrefs.GetInt("maxSickSociety");
        keepMaxNumStudentsInSchools = PlayerPrefs.GetInt("maxNumStudentsInSchools");
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
