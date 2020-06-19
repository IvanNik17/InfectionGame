using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script connected to the pause screen menu - it implements methods for the different buttons present at the screen.
/// - resumeGame - unpauses timers and goes back to the game
/// - restartFromPause - restarts current level and calls the restart event
/// - quitGame - quits to the start screen scene
/// - pauseGame - pauses timers and shows the pause menu
/// </summary>

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

        GlobalEvents.current.restartLevelEvent();

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void quitGame()
    {

        gameEndInitializer.current.whatEnd = -1;

        pauseScreenObj.SetActive(false);

        KeepDataBetweenLevels.saveData();

        int prevSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        GlobalEvents.current.exitGameEvent();

        SceneManager.LoadScene(prevSceneIndex);
    }

    public void pauseGame()
    {
        gameEndInitializer.current.whatEnd = -3;

        pauseScreenObj.SetActive(true);
    }


}
