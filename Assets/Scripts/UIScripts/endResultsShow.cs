using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for visualizing the end screen and controlling data visualized on it.
/// It implements a number of methods:
/// - Restart - used by the restart button to restart current level
/// - NextLevel - used by the next level button - it reload the scene and updates the start values in KeepDataBetweenLevels
/// - showStatistics - used to call the statistics screen and load all necessary data for it
/// - saveExit - used by the exit and save button - loads the start screen scene and saves the level data, so a user can load the same level again
/// </summary>

public class endResultsShow : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private GameObject endTextFieldHeadline;
    [SerializeField]
    private GameObject rateScreen;
    [SerializeField]
    private GameObject buttonRestart;
    [SerializeField]
    private GameObject buttonNext;
    [SerializeField]
    private GameObject textHospitalized;
    [SerializeField]
    private GameObject textMissed;
    [SerializeField]
    private GameObject textSchoolHeadline;
    [SerializeField]
    private GameObject[] schoolsSubText;
    [SerializeField]
    private GameObject graphScreen;
    [SerializeField]
    private GameObject timeSlider;
    [SerializeField]
    private AudioSource mainSource;
    [SerializeField]
    private AudioSource endSource;
    [SerializeField]
    private AudioClip[] goodBadAudio;
#pragma warning restore 0649

    private bool isPlayed = false;


    // Update is called once per frame
    void Update()
    {
        int checkEnding = gameEndInitializer.current.whatEnd;


        if (checkEnding >= 0 )
        {
            

            mainSource.volume = 0.01f;

            endScreen.SetActive(true);

            AudioClip endClip = goodBadAudio[1];

            string endText = "";
            if (checkEnding == 0)
            {
                endText = "Du overlevede epidemien!";
                endClip = goodBadAudio[0];
                
            }
            else if (checkEnding == 1)
            {

                endText = "Hospitalet er fyldt!";
            }
            else if (checkEnding == 2)
            {

                endText = "For mange syge mennesker!";
            }
            else if (checkEnding == 3)
            {

                endText = "Befolkningen blev ulykkelig!";
            }
            endTextFieldHeadline.GetComponent<Text>().text = endText;

            

            if (!endSource.isPlaying && !isPlayed)
            {
                endSource.clip = endClip;
                endSource.Play();
                isPlayed = true;
            }

            
            textHospitalized.GetComponent<Text>().text = "Du har indlagt <b>" + globalScoreKeeper.current.countHospitalized.ToString() + " </b> mennesker";

            textMissed.GetComponent<Text>().text = "Du gik glip af <b>" + globalScoreKeeper.current.numberSickSociety.ToString() + " </b> syge mennesker";

            bool didSchoolsClose = false;
            for (int i = 0; i < globalScoreKeeper.current.daysEachSchoolClosed.Length; i++)
            {

                schoolsSubText[i].GetComponent<Text>().text = "<b>" + globalScoreKeeper.current.daysEachSchoolClosed[i].ToString() + "</b>\n" + "dage";
                if (globalScoreKeeper.current.daysEachSchoolClosed[i]>0)
                {
                    didSchoolsClose = true;
                }
            }

            if (didSchoolsClose)
            {
                textSchoolHeadline.GetComponent<Text>().text = "Du havde brug for at lukke :";
                
            }
            else
            {
                textSchoolHeadline.GetComponent<Text>().text = "Du lukkede ingen bygninger";
            }


            if (checkEnding == 0)
            {
                buttonNext.SetActive(true);
                buttonRestart.SetActive(false);
            }
            else
            {
                buttonNext.SetActive(false);
                buttonRestart.SetActive(true);
            }


        }
    }


    public void Restart()
    {

        GlobalEvents.current.restartLevelEvent();

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void NextLevel()
    {


        if (KeepDataBetweenLevels.keepGameRating == 0 && KeepDataBetweenLevels.keepCurrentLevel>2)
        {
            rateScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            KeepDataBetweenLevels.nextLevel();

            GlobalEvents.current.nextLevelEvent();

            SceneManager.LoadScene(SceneManager.GetActiveScene().path);


            
        }


        
    }


    public void showStatistics()
    {
        graphScreen.SetActive(true);

        timeSlider.GetComponent<Slider>().maxValue = globalTimer.current.daysPassed;

        timeSlider.GetComponent<Slider>().value = globalTimer.current.daysPassed;

        GlobalEvents.current.showStatisticsEvent();
    }

    public void hideStatistics()
    {
        graphScreen.SetActive(false);

    }



    public void saveExit()
    {

        if (gameEndInitializer.current.whatEnd == 0)
        {
            KeepDataBetweenLevels.nextLevel();
        }
        

        KeepDataBetweenLevels.saveData();

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;


        GlobalEvents.current.exitGameEvent();

        SceneManager.LoadScene(nextSceneIndex);

        
    }
}
