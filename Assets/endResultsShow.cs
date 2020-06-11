using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endResultsShow : MonoBehaviour
{

    public GameObject endScreen;
    public GameObject endTextFieldHeadline;

    public GameObject rateScreen;

    public GameObject buttonRestart;
    public GameObject buttonNext;

    public GameObject textHospitalized;
    public GameObject textMissed;
    public GameObject textSchoolHeadline;
    public GameObject[] schoolsSubText;

    public GameObject graphScreen;
    public GameObject timeSlider;

    public AudioSource mainSource;
    public AudioSource endSource;

    public AudioClip[] goodBadAudio;

    bool isPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int checkEnding = gameEndInitializer.current.whatEnd;



        

        if (checkEnding >= 0 )
        {
            //mainSource.Stop();

            mainSource.volume = 0.01f;

            endScreen.SetActive(true);

            AudioClip endClip = goodBadAudio[1];

            string endText = "";
            if (checkEnding == 0)
            {
                endText = "You managed to survive the epidemic!";
                endClip = goodBadAudio[0];
                
            }
            else if (checkEnding == 1)
            {

                endText = "The hospital capacity was exceeded!";
            }
            else if (checkEnding == 2)
            {

                endText = "Too many sick people in society!";
            }
            else if (checkEnding == 3)
            {

                endText = "Children got depressed, because classes were closed for too long!";
            }
            endTextFieldHeadline.GetComponent<Text>().text = endText;

            

            if (!endSource.isPlaying && !isPlayed)
            {
                endSource.clip = endClip;
                endSource.Play();
                isPlayed = true;
            }
            

            textHospitalized.GetComponent<Text>().text = "There were <b>" + globalScoreKeeper.current.countHospitalized.ToString() + " </b> people hospitalized";

            textMissed.GetComponent<Text>().text = "You missed <b>" + globalScoreKeeper.current.numberSickSociety.ToString() + " </b> infected people";

            bool didSchoolsClose = false;
            for (int i = 0; i < globalScoreKeeper.current.daysEachSchoolClosed.Length; i++)
            {

                schoolsSubText[i].GetComponent<Text>().text = "<b>" + globalScoreKeeper.current.daysEachSchoolClosed[i].ToString() + "</b>\n" + "days";
                if (globalScoreKeeper.current.daysEachSchoolClosed[i]>0)
                {
                    didSchoolsClose = true;
                }
            }

            if (didSchoolsClose)
            {
                textSchoolHeadline.GetComponent<Text>().text = "You needed to close some grades:";
                
            }
            else
            {
                textSchoolHeadline.GetComponent<Text>().text = "You did not close any grades";
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
        //globalTimer.current.maxDays += 20;
        //globalTimer.current.spawnTime -= 0.2f;
        //globalTimer.current.slideTime -= 0.2f;

        //globalScoreKeeper.current.currentLevel += 1;
        //globalScoreKeeper.current.percentHealthyToSick += 0.05f;
        //globalScoreKeeper.current.maxHospitalCapacity += 5;
        //globalScoreKeeper.current.maxSickSociety += 5;
        //globalScoreKeeper.current.maxNumStudentsInSchools += 5;

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
