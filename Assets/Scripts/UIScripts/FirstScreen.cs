using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Analytics;

public class FirstScreen : MonoBehaviour
{

#pragma warning disable 0649


    [SerializeField]
    private GameObject howToObj;

    [SerializeField]
    private GameObject settingsObj;

    [SerializeField]
    private GameObject creditsObj;

    [SerializeField]
    private GameObject volumeTxt;

    [SerializeField]
    private GameObject volumeSlider;

    [SerializeField]
    private AudioSource startScreenAudio;

    [SerializeField]
    private GameObject loadButton;

    [SerializeField]
    private GameObject highScoreObj;

    [SerializeField]
    private GameObject tutorialToggle;

#pragma warning restore 0649



    int currMenu; //0 - main, 1 - how to, 2 - settings, 3 - credits; // current menu items

    private static float volumeLevel = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        currMenu = 0;

        volumeSlider.GetComponent<Slider>().value = volumeLevel;


        //Debug.Log(AnalyticsSessionInfo.sessionId);
        

        if (PlayerPrefs.GetInt("currentLevel") > 1)
        {
            loadButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            loadButton.GetComponent<Button>().interactable = false;
        }

        int highScore = PlayerPrefs.GetInt("highScoreLevel");
        if (highScore != 0)
        {
            highScoreObj.SetActive(true);
            highScoreObj.GetComponentInChildren<Text>().text = "Højeste Score:" + "\n" + "Niveau " + PlayerPrefs.GetInt("highScoreLevel").ToString();
        }
        else
        {
            highScoreObj.SetActive(false);
        }


        if (KeepDataBetweenLevels.checkForTutorial() == 0)
        {
            tutorialToggle.GetComponent<Toggle>().interactable = false;
            tutorialToggle.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            tutorialToggle.GetComponent<Toggle>().interactable = true;
        }

        KeepDataBetweenLevels.playTutorial = tutorialToggle.GetComponent<Toggle>().isOn;

    }

    // Update is called once per frame
    void Update()
    {
        startScreenAudio.volume = volumeLevel;
        
        
    }


    public void doTutorial(bool toggleTut)
    {
        KeepDataBetweenLevels.playTutorial = toggleTut;

    }


    public void goToGameNew()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        KeepDataBetweenLevels.resetStaticVals();

        SceneManager.LoadScene(nextSceneIndex);


        KeepDataBetweenLevels.keepPlaySessionID = KeepDataBetweenLevels.CreateMD5(KeepDataBetweenLevels.generateUniqueID());

        PlayerPrefs.SetString("currPlaySessionID", KeepDataBetweenLevels.keepPlaySessionID);

    }


    public void goToGameLoad()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        KeepDataBetweenLevels.keepPlaySessionID = PlayerPrefs.GetString("currPlaySessionID");

        KeepDataBetweenLevels.loadData();

        

        SceneManager.LoadScene(nextSceneIndex);
    }


    public void howToShow()
    {
        howToObj.SetActive(true);

        currMenu = 1;
    }

    public void settingsShow()
    {
        settingsObj.SetActive(true);
        currMenu = 2;
    }

    public void creditsShow()
    {
        creditsObj.SetActive(true);
        currMenu = 3;
    }


    public void quitApp()
    {
        Application.Quit();
    }

    public void goBack()
    {
        if (currMenu == 1)
        {
            howToObj.SetActive(false);
        }
        else if(currMenu == 2)
        {
            settingsObj.SetActive(false);
        }
        else if (currMenu == 3)
        {
            creditsObj.SetActive(false);
        }
    }

    public void changeVolume(float currVolume)
    {

        volumeLevel = currVolume;
        volumeTxt.GetComponent<Text>().text = ((int)(currVolume * 100)).ToString();
    }


}
