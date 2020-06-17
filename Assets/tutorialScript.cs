using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        if (whichTutorial == 5)
        {
            buttonNext.GetComponentInChildren<Text>().text = "End Tutorial";
        }
        else
        {
            buttonNext.GetComponentInChildren<Text>().text = "Next";
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
