using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;



public class captureAndSendInfo : MonoBehaviour
{
    // Start is called before the first frame update

    public static captureAndSendInfo current;


    Dictionary<string, List<string>> logs;

    string deviceId;
    string playId;

    private void Awake()
    {
        current = this;
        
        logs = new Dictionary<string, List<string>>() //create a new dictionary
		{
            {"GameVersion", new List<string>()},
            {"PlayID", new List<string>()},
            {"DeviceID", new List<string>()},
            {"Email", new List<string>()},
            {"Timestamp", new List<string>()},
            {"EventType", new List<string>()},
            {"GameTime", new List<string>()},
            {"GameState", new List<string>()},
            {"CurrentLevel", new List<string>()},
            {"MaxNumInfectedInSociety", new List<string>()},
            {"MaxNumHospitalized", new List<string>()},
            {"DaysClosed1_3", new List<string>()},
            {"DaysClosed4_6", new List<string>()},
            {"DaysClosed7_9", new List<string>()},
            {"DaysClosed10_12", new List<string>()},
            {"CurrentDay", new List<string>()},
            {"CurrentInfectedInSociety", new List<string>()},
            {"CurrentHospitalized", new List<string>()},
            {"CurrentDepression1_3", new List<string>()},
            {"CurrentDepression4_6", new List<string>()},
            {"CurrentDepression7_9", new List<string>()},
            {"CurrentDepression10_12", new List<string>()},
            {"BackendSpawnTime", new List<string>()},
            {"BackendHealthyToSick", new List<string>()},
            {"GameRating", new List<string>()}
        };

        deviceId = PlayerPrefs.GetString("currDeviceID");
        playId = KeepDataBetweenLevels.keepPlaySessionID;

        Debug.Log(deviceId);
        Debug.Log(playId);

        GlobalEvents.current.onDayPassed += eachDayData;

        GlobalEvents.current.onlevelEndingEvent += levelEndData;

        GlobalEvents.current.onShowStatisticsEvent += showStatisticsData;

        GlobalEvents.current.onNextLevelEvent += nextLevelData;

        GlobalEvents.current.onRestartLevelEvent += restartLevelData;

        GlobalEvents.current.onExitGameEvent += exitGameData;

        GlobalEvents.current.onStartGameEvent += startGameData;


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


    void showLastSave()
    {

        string showLastLog = "";

        showLastLog += " | " + logs["GameVersion"][logs["GameVersion"].Count-1].ToString();
        showLastLog += " | " + logs["PlayID"][logs["PlayID"].Count-1].ToString();
        showLastLog += " | " + logs["DeviceID"][logs["DeviceID"].Count-1].ToString();
        showLastLog += " | " + logs["Email"][logs["Email"].Count-1].ToString();
        showLastLog += " | " + logs["Timestamp"][logs["Timestamp"].Count-1].ToString();
        showLastLog += " | " + logs["EventType"][logs["EventType"].Count-1].ToString();
        showLastLog += " | " + logs["GameTime"][logs["GameTime"].Count-1].ToString();
        showLastLog += " | " + logs["GameState"][logs["GameState"].Count-1].ToString();
        showLastLog += " | " + logs["CurrentLevel"][logs["CurrentLevel"].Count-1].ToString();
        showLastLog += " | " + logs["MaxNumInfectedInSociety"][logs["MaxNumInfectedInSociety"].Count-1].ToString();
        showLastLog += " | " + logs["MaxNumHospitalized"][logs["MaxNumHospitalized"].Count - 1].ToString();
        showLastLog += " | " + logs["DaysClosed1_3"][logs["DaysClosed1_3"].Count - 1].ToString();
        showLastLog += " | " + logs["DaysClosed4_6"][logs["DaysClosed4_6"].Count - 1].ToString();
        showLastLog += " | " + logs["DaysClosed7_9"][logs["DaysClosed7_9"].Count - 1].ToString();
        showLastLog += " | " + logs["DaysClosed10_12"][logs["DaysClosed10_12"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentDay"][logs["CurrentDay"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentInfectedInSociety"][logs["CurrentInfectedInSociety"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentHospitalized"][logs["CurrentHospitalized"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentDepression1_3"][logs["CurrentDepression1_3"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentDepression4_6"][logs["CurrentDepression4_6"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentDepression7_9"][logs["CurrentDepression7_9"].Count - 1].ToString();
        showLastLog += " | " + logs["CurrentDepression10_12"][logs["CurrentDepression10_12"].Count - 1].ToString();
        showLastLog += " | " + logs["BackendSpawnTime"][logs["BackendSpawnTime"].Count - 1].ToString();
        showLastLog += " | " + logs["BackendHealthyToSick"][logs["BackendHealthyToSick"].Count - 1].ToString();
        showLastLog += " | " + logs["GameRating"][logs["GameRating"].Count - 1].ToString();


        Debug.Log(showLastLog);

    }


    void sendToServer()
    {
        //this.GetComponent<ConnectToMySQL>().AddToUploadQueue(logs);
        //this.GetComponent<ConnectToMySQL>().UploadNow();
    }


    void eachDayData()
    {

        string currEventType = "dayPassed";
        
        string currGameState = "levelPlaying";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();

    }


    void showStatisticsData()
    {
        string currEventType = "statisticsScreen";

        string currGameState = "showStatistics";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();
    }

    void nextLevelData()
    {
        string currEventType = "nextLevel";

        string currGameState = "levelLoadNext";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();

        sendToServer();
    }

    void restartLevelData()
    {
        string currEventType = "restartLevel";

        string currGameState = "levelReload";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();

        sendToServer();
    }


    void exitGameData()
    {
        string currEventType = "exitGame";

        string currGameState = "goingToMainMenu";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();

        sendToServer();
    }


    void startGameData()
    {
        string currEventType = "startGame";

        string currGameState = "levelStart";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();
    }


    void levelEndData(int whatEnding)
    {

        string currEventType = "";

        if (whatEnding == 0)
        {
            currEventType = "levelWin";
        }
        else if (whatEnding == 1)
        {
            currEventType = "levelLoseHospital";
        }
        else if (whatEnding == 2)
        {
            currEventType = "levelLoseSociety";
        }
        else if (whatEnding == 3)
        {
            currEventType = "levelLoseSchool";
        }



        string currGameState = "levelEnd";

        gatherAndSaveData(currEventType, currGameState);

        showLastSave();

    }



    void gatherAndSaveData(string currEventType, string currGameState)
    {
        string gameVersion = "beta2";
        string email = "anonymus";

        float gameTime = globalTimer.current.levelTime;



        int currLevel = KeepDataBetweenLevels.keepCurrentLevel;
        int maxInfSociety = KeepDataBetweenLevels.keepMaxSickSociety;
        int maxHospitalized = KeepDataBetweenLevels.keepMaxHospitalCapacity;


        int[] daysClosedPerClass = globalScoreKeeper.current.daysEachSchoolClosed;

        int currDay = globalTimer.current.daysPassed;

        int currInfSociety = globalScoreKeeper.current.numberSickSociety;
        int currHospitalized = globalScoreKeeper.current.numberSickHospital;

        int[] currDepressionAll = new int[globalScoreKeeper.current.allSpawners.Length];

        for (int i = 0; i < globalScoreKeeper.current.allSpawners.Length; i++)
        {
            currDepressionAll[i] = globalScoreKeeper.current.allSpawners[i].GetComponent<spawner>().daysClosed;
        }

        float spawnTime = KeepDataBetweenLevels.keepSpawnTime;
        float healthyToSick = KeepDataBetweenLevels.keepPercentHealthyToSick;



        int gameRating = KeepDataBetweenLevels.keepGameRating;


        addNewLog(gameVersion, email, currEventType, gameTime, currGameState, currLevel, maxInfSociety, maxHospitalized,
            daysClosedPerClass, currDay, currInfSociety, currHospitalized, currDepressionAll, spawnTime, healthyToSick, gameRating);

    }






    void addNewLog(string gameVersion,
                         string email,
                         string currEventType,
                         float gameTime,
                         string currGameState,
                         int currLevel,
                         int maxInfSociety,
                         int maxHospitalized,
                         int[] daysClosedPerClass,
                         //int daysClosed_4_6,
                         //int daysClosed_7_9,
                         //int daysClosed_10_12,
                         int currDay,
                         int currInfSociety,
                         int currHospitalized,
                         int[] currDepressionPerClass,
                         //int currDepression_4_6,
                         //int currDepression_7_9,
                         //int currDepression_10_12,
                         float spawnTime,
                         float healthyToSick,
                         int gameRating)
    {


        string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");




        logs["GameVersion"].Add(gameVersion);
        logs["PlayID"].Add(playId);
        logs["DeviceID"].Add(deviceId);
        logs["Email"].Add(email);
        logs["Timestamp"].Add(timeStamp);
        logs["EventType"].Add(currEventType);
        logs["GameTime"].Add(gameTime.ToString());
        logs["GameState"].Add(currGameState);
        logs["CurrentLevel"].Add(currLevel.ToString());
        logs["MaxNumInfectedInSociety"].Add(maxInfSociety.ToString());
        logs["MaxNumHospitalized"].Add(maxHospitalized.ToString());
        logs["DaysClosed1_3"].Add(daysClosedPerClass[0].ToString());
        logs["DaysClosed4_6"].Add(daysClosedPerClass[1].ToString());
        logs["DaysClosed7_9"].Add(daysClosedPerClass[2].ToString());
        logs["DaysClosed10_12"].Add(daysClosedPerClass[3].ToString());
        logs["CurrentDay"].Add(currDay.ToString());
        logs["CurrentInfectedInSociety"].Add(currInfSociety.ToString());
        logs["CurrentHospitalized"].Add(currHospitalized.ToString());
        logs["CurrentDepression1_3"].Add(currDepressionPerClass[0].ToString());
        logs["CurrentDepression4_6"].Add(currDepressionPerClass[1].ToString());
        logs["CurrentDepression7_9"].Add(currDepressionPerClass[2].ToString());
        logs["CurrentDepression10_12"].Add(currDepressionPerClass[3].ToString());
        logs["BackendSpawnTime"].Add(spawnTime.ToString().Replace(',', '.'));
        logs["BackendHealthyToSick"].Add(healthyToSick.ToString().Replace(',', '.'));
        logs["GameRating"].Add(gameRating.ToString());

        Debug.Log("added");
    }

}
