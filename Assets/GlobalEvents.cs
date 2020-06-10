using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
