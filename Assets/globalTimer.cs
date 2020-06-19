using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global code for keeping track of game time and timers for game ending, spawners and sliders. Calls events when timers hit specific values.
/// Uses FixedUpdate, so timers are independent of the framerate.
/// </summary>

public class globalTimer : MonoBehaviour
{

    public static globalTimer current;

    public int daysPassed = 0;
    public int maxDays = 25;
    public int hourPassed = 0;
    public float levelTime = 0;

    private float timerSpawn = 0f;
    private float timerSlide = 0f;
    private float spawnTime = 0.5f;
    private float slideTime = 0.5f;
    private float timerDay = 0f;
    private float dayPassTime = 2f;
    private float timerHour = 0;




    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxDays = KeepDataBetweenLevels.keepMaxDays;
        spawnTime = KeepDataBetweenLevels.keepSpawnTime;
        slideTime = KeepDataBetweenLevels.keepSlideTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        levelTime += Time.fixedDeltaTime;

        if (gameEndInitializer.current.whatEnd == -1)
        {
            timerSpawn += Time.fixedDeltaTime;
            timerSlide += Time.fixedDeltaTime;
            timerDay += Time.fixedDeltaTime;

            timerHour += Time.fixedDeltaTime;


            if (timerSpawn >= spawnTime)
            {
                GlobalEvents.current.spawnKids();

                timerSpawn = 0f;
            }


            if (timerSlide >= slideTime)
            {

                GlobalEvents.current.slideKids();
                timerSlide = 0f;
            }

            if (timerDay >= dayPassTime)
            {
                GlobalEvents.current.dayPassed();

                timerDay = 0f;

                daysPassed++;

                hourPassed = 0;
            }

            if (timerHour >= dayPassTime / 24f)
            {
                timerHour = 0;
                hourPassed++;
            }
        }

        


    }
}
