using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Global code for initializing all the used events in the project. List of events:
/// - onSpawnKids - triggered by globalTimer, when spawner timer hits the specified value
/// - onSlideKids - same as onSpawnKids
/// - onDayPassed - triggered by globalTimer, when the day timer hits the specified end value
/// - onToggleClasses - triggered, when user opens or closes one of the school/class objects that spawns kids - triggered by schoolSelector script
/// - onShowStatisticsEvent - triggered, when user opens the statistics menu, after the game level has ended - triggered by button initialized in endResultsShow script
/// - onlevelEndingEvent - triggered, when one of the conditions for a level end is reached - these are set in globalEndInitializer script
/// - onNextLevelEvent - triggered, when user clicks the button for next level start - triggered by button initialized in endResultsShow script
/// - onRestartLevelEvent - triggered, when user clicks the restart button - initialized in the endResultsShow and pauseScreen scripts
/// - onExitGameEvent - triggered, when user exits to the start scene - initialized in the endResultsShow and pauseScreen scripts
/// - onStartGameEvent - triggered, when a level is started - initialized in the startScreen script
/// </summary>

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents current;

    void Awake()
    {
        current = this;
    }



    public event Action onSpawnKids;
    public void spawnKids()
    {
        if (onSpawnKids != null)
        {
            onSpawnKids();
        }
    }

    public event Action onSlideKids;
    public void slideKids()
    {
        if (onSlideKids != null)
        {
            onSlideKids();
        }
    }

    public event Action onDayPassed;
    public void dayPassed()
    {
        if (onDayPassed != null)
        {
            onDayPassed();
        }
    }

    public event Action<int, bool> onToggleClasses;
    public void toggleClasses(int whichClass, bool isClosed)
    {
        if (onToggleClasses != null)
        {
            onToggleClasses(whichClass, isClosed);
        }
    }


    public event Action onShowStatisticsEvent;
    public void showStatisticsEvent()
    {
        if (onShowStatisticsEvent != null)
        {
            onShowStatisticsEvent();
        }
    }

    public event Action<int> onlevelEndingEvent;
    public void levelEndingEvent(int whichEnding)
    {
        if (onlevelEndingEvent != null)
        {
            onlevelEndingEvent(whichEnding);
        }
    }

    public event Action onNextLevelEvent;
    public void nextLevelEvent()
    {
        if (onNextLevelEvent != null)
        {
            onNextLevelEvent();
        }
    }

    public event Action onRestartLevelEvent;
    public void restartLevelEvent()
    {
        if (onRestartLevelEvent != null)
        {
            onRestartLevelEvent();
        }
    }

    public event Action onExitGameEvent;
    public void exitGameEvent()
    {
        if (onExitGameEvent != null)
        {
            onExitGameEvent();
        }
    }

    public event Action onStartGameEvent;
    public void startGameEvent()
    {
        if (onStartGameEvent != null)
        {
            onStartGameEvent();
        }
    }


}
