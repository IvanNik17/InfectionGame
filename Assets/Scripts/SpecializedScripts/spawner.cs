using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script connect to each of the spawner objects. Keeps track of which class it is, current number of students, maximum number of students, days it has been closed and is it currently open or closed
/// Methods implemented:
/// - countClosedDays - is called on each day end and counts how many days the school has been closed
/// - changeSchoolAppearance - called when the school is clicked to change its appearance
/// - changeClass - called when the school is clicked to change if it is open or closed
/// </summary>

public class spawner : MonoBehaviour
{

    public int whichClass;
    public int currNumStudents;
    public int maxNumStudents;

    public int daysClosed = 0;

    public int maxDaysClosed = 0;

    public int spawnerNumber;

    public bool isOpen = true;

    public List<GameObject> kidsInThisClass;

    public GameObject schoolObj;

    // Start is called before the first frame update
    void Start()
    {

        maxDaysClosed = globalScoreKeeper.current.maxDaysSchoolClosed;



        kidsInThisClass = new List<GameObject>();

        GlobalEvents.current.onToggleClasses += changeClass;

        GlobalEvents.current.onToggleClasses += changeSchoolAppearance;

        GlobalEvents.current.onDayPassed += countClosedDays;

    }




    void countClosedDays()
    {
        if (!isOpen)
        {
            daysClosed++;
        }
        else
        {
            if (daysClosed>0)
            {
                daysClosed--;
            }
            
        }
    }


    void changeSchoolAppearance(int thisClass, bool openClose)
    {

       
        if (spawnerNumber == thisClass)
        {
            
            foreach (Transform schoolChildren in schoolObj.transform)
            {
                if (schoolChildren.name == "closed")
                {
                    Debug.Log(schoolChildren.gameObject.activeSelf);
                    schoolChildren.gameObject.SetActive(!openClose);
                }
            }
        }
    }


    void changeClass(int thisClass, bool openClose)
    {

        if (spawnerNumber == thisClass)
        {
            isOpen = openClose;
        }
        
    }
}
