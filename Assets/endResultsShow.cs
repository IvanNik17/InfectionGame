using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endResultsShow : MonoBehaviour
{

    public GameObject endScreen;
    public GameObject endTextFieldHeadline;

    public GameObject buttonRestart;
    public GameObject buttonNext;

    public GameObject textHospitalized;
    public GameObject textMissed;
    public GameObject textSchoolHeadline;
    public GameObject[] schoolsSubText;


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
            endScreen.SetActive(true);

            string endText = "";
            if (checkEnding == 0)
            {
                endText = "You managed to survive the epidemic!";
            }
            else if (checkEnding == 1)
            {

                endText = "The hospital capacity was exceeded!";
            }
            else if (checkEnding == 2)
            {

                endText = "You missed too many sick people!";
            }
            else if (checkEnding == 3)
            {

                endText = "Schools capacity was exceeded!";
            }
            endTextFieldHeadline.GetComponent<Text>().text = endText;


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


        KeepDataBetweenLevels.nextLevel();

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }


    


    public void saveExit()
    {

        if (gameEndInitializer.current.whatEnd == 0)
        {
            KeepDataBetweenLevels.nextLevel();
        }
        

        KeepDataBetweenLevels.saveData();

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        SceneManager.LoadScene(nextSceneIndex);

        
    }
}
