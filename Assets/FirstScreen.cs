using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Analytics;

public class FirstScreen : MonoBehaviour
{

    public GameObject howToObj;
    public GameObject settingsObj;
    public GameObject creditsObj;

    public GameObject volumeTxt;

    public GameObject volumeSlider;

    public AudioSource startScreenAudio;

    public GameObject loadButton;

    int currMenu; //0 - main, 1 - how to, 2 - settings, 3 - credits;

    public static float volumeLevel = 0.7f;

    public GameObject highScoreObj;

    // Start is called before the first frame update
    void Start()
    {
        currMenu = 0;

        volumeSlider.GetComponent<Slider>().value = volumeLevel;


        Debug.Log(AnalyticsSessionInfo.sessionId);
        

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
            highScoreObj.GetComponentInChildren<Text>().text = "Highscore:" + "\n" + "Level " + PlayerPrefs.GetInt("highScoreLevel").ToString();
        }
        else
        {
            highScoreObj.SetActive(false);
        }
        



    }

    // Update is called once per frame
    void Update()
    {
        startScreenAudio.volume = volumeLevel;
    }


    public void goToGameNew()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        KeepDataBetweenLevels.resetStaticVals();

        SceneManager.LoadScene(nextSceneIndex);
    }


    public void goToGameLoad()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

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
