using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Rating screen script. After the 3rd level it is called so a user can rate the game from 1 to 5 stars, after which the next level is loaded as normal. The script is called from endResultsShow
/// </summary>

public class rateGame : MonoBehaviour
{

    public void buttonRate(int rating)
    {
        KeepDataBetweenLevels.keepGameRating = rating;

        gameObject.SetActive(false);

        

        KeepDataBetweenLevels.nextLevel();

        GlobalEvents.current.nextLevelEvent();

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }


}
