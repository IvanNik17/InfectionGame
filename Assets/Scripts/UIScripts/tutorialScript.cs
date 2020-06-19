using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tutorial script used to control the tutorial animations and to decide if the tutorial should be played or not
/// The tutorial is played if a new machine/ip plays the game for the first time. After that the tutorial can be replyed if the user choses the tutorial checkbox in the start screen
/// The script contains two methods - next tutorial and previous tutorial used by the two buttons for going between animations.
/// After all 5 animations are played the tutorial can be finished by pressing the next button.
/// </summary>

public class tutorialScript : MonoBehaviour
{

    public int whichTutorial = -1;

    public GameObject buttonNext;

    public GameObject startScreen;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(KeepDataBetweenLevels.checkForTutorial());

        Debug.Log(KeepDataBetweenLevels.playTutorial);

        if (KeepDataBetweenLevels.keepCurrentLevel == 1)
        {
            if (KeepDataBetweenLevels.checkForTutorial() == 0)
            {
                startScreen.SetActive(false);
                this.gameObject.SetActive(true);
            }
            else if (KeepDataBetweenLevels.checkForTutorial() == 1 && KeepDataBetweenLevels.playTutorial)
            {
                startScreen.SetActive(false);
                this.gameObject.SetActive(true);
            }
            else if (KeepDataBetweenLevels.checkForTutorial() == 1 && !KeepDataBetweenLevels.playTutorial)
            {
                startScreen.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            startScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (whichTutorial == 5)
        {
            buttonNext.GetComponentInChildren<Text>().text = "Afslut Tutorial";
        }
        else
        {
            buttonNext.GetComponentInChildren<Text>().text = "Næste";
        }

        
    }

    
    public void nextTutorial()
    {

        whichTutorial = Mathf.Clamp(whichTutorial + 1, 1, 6);

        if (whichTutorial<6)
        {
            this.GetComponent<Animator>().SetInteger("changeStates", whichTutorial);
        }
        else
        {
            startScreen.SetActive(true);
            this.gameObject.SetActive(false);

            PlayerPrefs.SetInt("playedTutorial", 1);
        }

        

        
    }

    public void prevTutorial()
    {
        whichTutorial = Mathf.Clamp(whichTutorial - 1, 1, 6);

        this.GetComponent<Animator>().SetInteger("changeStates", whichTutorial);
    }


}
