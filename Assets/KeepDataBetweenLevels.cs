using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

/// <summary>
/// Class of static variables and access to PlayerPref for saving data between levels and between game starts.
/// The static variables have a current and a base copy - the base copy is used for when a new game is started and current values need to be cleared to initial states
/// The base values are set at level 1
/// Methods implemented in the class:
/// - checkForTutorial() - check if the user has went through the tutorial on this machine/this IP
/// - nextLevel() - updates the level variables when a new level is reached. Currently the update is with hardcoded values, clamped to minimum and maximum possible values
/// - resetStaticVals() - resets the static variables to the their base values when a new game is started
/// - saveData() - saves data to where the user has gotten, so the level can be continued next time the game is started
/// - loadData() - loads data if the load button is clicked in the start screen in the start scene
/// - CreateMD5() - creates a MD5 hash for the saved data
/// - generateUniqueID() - generates a random ID from current date, current game time, a random number, etc.
/// </summary>


public class KeepDataBetweenLevels : MonoBehaviour
{

    //40 , 0.5 ,0.5 , 1, 0.5 , 30, 40, 20

    public static int keepMaxDays = 25; // 20
    public static float keepSpawnTime = 0.7f;
    public static float keepSlideTime = 0.7f;

    public static int keepCurrentLevel = 1;
    public static float keepPercentHealthyToSick = 0.5f; // 0.5f
    public static int keepMaxHospitalCapacity = 20;
    public static int keepMaxSickSociety = 30;
    public static int keepMaxPossibleSchoolClose = keepMaxDays/2;


    public static int keepMaxAmbulanceCapacity = 5;


    public static int baseMaxDays;
    public static float baseSpawnTime;
    public static float baseSlideTime ;

    public static int baseCurrentLevel;
    public static float basePercentHealthyToSick;
    public static int baseMaxHospitalCapacity;
    public static int baseMaxSickSociety;
    public static int baseMaxPossibleSchoolClose;

    public static int baseMaxAmbulanceCapacity;

    public static int keepGameRating = 0;

    public static bool playTutorial;

    
    public static string keepPlaySessionID = " ";


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
            baseMaxPossibleSchoolClose = keepMaxPossibleSchoolClose;

            baseMaxAmbulanceCapacity = keepMaxAmbulanceCapacity;
        }


        if (!PlayerPrefs.HasKey("currDeviceID"))
        {
            string deviceID_MD5 = CreateMD5(generateUniqueID());

            Debug.Log("Device ID: " + deviceID_MD5);
            PlayerPrefs.SetString("currDeviceID", deviceID_MD5);
        }
        

    }

    public static int checkForTutorial()
    {
        return PlayerPrefs.GetInt("playedTutorial");
    }


    public static void nextLevel()
    {
        
        keepSpawnTime = Mathf.Clamp(keepSpawnTime- 0.1f,0.2f, 99999);
        keepSlideTime = Mathf.Clamp(keepSlideTime - 0.1f, 0.2f, 99999);

        keepCurrentLevel += 1;
        keepPercentHealthyToSick = Mathf.Clamp(keepPercentHealthyToSick + 0.05f, 0.1f, 0.8f);
        keepMaxHospitalCapacity = Mathf.Clamp(keepMaxHospitalCapacity + 5, 20, 55);
        keepMaxSickSociety = Mathf.Clamp(keepMaxSickSociety + 2, 30, 46);
        keepMaxPossibleSchoolClose = Mathf.RoundToInt(keepMaxDays / 3);


        keepMaxAmbulanceCapacity = Mathf.Clamp(keepMaxAmbulanceCapacity + 1, 4, 10);


        Debug.Log("spawnTime " + keepSpawnTime + " HealthyToSick " + keepPercentHealthyToSick + " HospitalCapacity " + keepMaxHospitalCapacity + " SickSociety " + keepMaxSickSociety);
        
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
        keepMaxPossibleSchoolClose = baseMaxPossibleSchoolClose;

        keepMaxAmbulanceCapacity = baseMaxAmbulanceCapacity;

        keepGameRating = 0;
        
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
        PlayerPrefs.SetInt("maxPossibleSchoolClose", keepMaxPossibleSchoolClose);


        PlayerPrefs.SetInt("maxAmbulanceCapacity", keepMaxAmbulanceCapacity);


        if (PlayerPrefs.GetInt("highScoreLevel") < keepCurrentLevel)
        {
            PlayerPrefs.SetInt("highScoreLevel", keepCurrentLevel);
        }
        

        
        
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

        keepMaxPossibleSchoolClose = PlayerPrefs.GetInt("maxPossibleSchoolClose");

        keepMaxAmbulanceCapacity = PlayerPrefs.GetInt("maxAmbulanceCapacity");

       
    }


    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    public static string generateUniqueID()
    {
        var random = new System.Random();
        DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
        double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

        string uniqueID = Application.systemLanguage                            //Language
                + "-" + Application.platform                                           //Device    
                + "-" + String.Format("{0:X}", Convert.ToInt32(timestamp))                //Time
                + "-" + String.Format("{0:X}", Convert.ToInt32(Time.time * 1000000))        //Time in game
                + "-" + String.Format("{0:X}", random.Next(1000000000));                //random number

        //Debug.Log("Generated Unique ID: " + uniqueID);

        return uniqueID;
    }


}
