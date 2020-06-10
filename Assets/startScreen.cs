using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startScreen : MonoBehaviour
{

    public GameObject startScreenObj;
    public GameObject headlineText;
    public GameObject underText;
    

    // Start is called before the first frame update
    void Start()
    {
        headlineText.GetComponent<Text>().text = "Level " + KeepDataBetweenLevels.keepCurrentLevel;
        underText.GetComponent<Text>().text = "Protect society from the infection for " + KeepDataBetweenLevels.keepMaxDays + " days.";
    }


    public void startGame()
    {
        gameEndInitializer.current.whatEnd = -1;

        GlobalEvents.current.startGameEvent();

        startScreenObj.SetActive(false);
    }

}
