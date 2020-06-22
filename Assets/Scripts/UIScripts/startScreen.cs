using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script connected to the start screen menu - it shows the level and the days to survive. 
/// The StartGame method calls the appropriate event and sets the ending variable to an initial value
/// </summary>

public class startScreen : MonoBehaviour
{

    public GameObject startScreenObj;
    public GameObject headlineText;
    public GameObject underText;
    

    // Start is called before the first frame update
    void Start()
    {
        headlineText.GetComponent<Text>().text = "Niveau " + KeepDataBetweenLevels.keepCurrentLevel;

        if (KeepDataBetweenLevels.keepCurrentLevel > 1)
        {
            underText.GetComponent<Text>().text = "Sygdommen blev stærkere. Beskyt mennesker mod infektionen for " + KeepDataBetweenLevels.keepMaxDays + " dage.";
        }
        else
        {
            underText.GetComponent<Text>().text = "Beskyt mennesker mod infektionen for " + KeepDataBetweenLevels.keepMaxDays + " dage.";
        }
        
    }


    public void startGame()
    {
        gameEndInitializer.current.whatEnd = -1;

        GlobalEvents.current.startGameEvent();

        startScreenObj.SetActive(false);
    }

}
