using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseScreen : MonoBehaviour
{

    public GameObject pauseScreenObj;

    public GameObject pauseButton;
    // Start is called before the first frame update

    private void Update()
    {
        if (gameEndInitializer.current.whatEnd != -1)
        {
            pauseButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            pauseButton.GetComponent<Button>().interactable = true;
        }
    }


    public void resumeGame()
    {

        gameEndInitializer.current.whatEnd = -1;

        pauseScreenObj.SetActive(false);
    }


    public void restartFromPause()
    {
        gameEndInitializer.current.whatEnd = -1;

        pauseScreenObj.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void quitGame()
    {

        gameEndInitializer.current.whatEnd = -1;

        pauseScreenObj.SetActive(false);

        KeepDataBetweenLevels.saveData();

        int prevSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        SceneManager.LoadScene(prevSceneIndex);
    }

    public void pauseGame()
    {
        gameEndInitializer.current.whatEnd = -3;

        pauseScreenObj.SetActive(true);
    }


}
