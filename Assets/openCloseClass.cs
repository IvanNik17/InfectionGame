using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCloseClass : MonoBehaviour
{

    public int currClass;


    public void changeClass(bool change)
    {
        GlobalEvents.current.toggleClasses(currClass,change);
    }
}
