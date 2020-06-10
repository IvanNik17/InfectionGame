using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rateGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonRate(int rating)
    {
        KeepDataBetweenLevels.keepGameRating = rating;

        gameObject.SetActive(false);

        

        KeepDataBetweenLevels.nextLevel();

        GlobalEvents.current.nextLevelEvent();

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }


}
