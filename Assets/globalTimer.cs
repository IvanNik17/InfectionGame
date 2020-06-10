using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalTimer : MonoBehaviour
{

    public static globalTimer current;

    public float timerSpawn = 0f;
    public float timerSlide = 0f;

    public float spawnTime = 3f;


    public float slideTime = 3f;

    public int daysPassed = 0;
    public float timerDay = 0f;
    public float dayPassTime = 10f;

    public int maxDays = 40;

    public int hourPassed = 0;
    public float timerHour = 0;

    public float levelTime = 0;


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
