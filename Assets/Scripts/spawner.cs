using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public int whichClass;
    public int currNumStudents;
    public int maxNumStudents;

    public int spawnerNumber;

    public bool isOpen = true;

    public List<GameObject> kidsInThisClass;

    public GameObject schoolObj;

    //public List<int> placeOfKids; // 0 - spawner, 1 - falling, 2- cough, 3-ground

    // Start is called before the first frame update
    void Start()
    {

        

        maxNumStudents = globalScoreKeeper.current.maxNumStudentsInSchools;

        kidsInThisClass = new List<GameObject>();

        GlobalEvents.current.onToggleClasses += changeClass;

        GlobalEvents.current.onToggleClasses += changeSchoolAppearance;

        
        //foreach (Transform childTrans in transform)
        //{
        //    Debug.Log(childTrans.name);
        //    if (childTrans.name == "schoolRot")
        //    {
        //        schoolObj = childTrans.gameObject;
        //    }
        //}
        //Debug.Log("-------------");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void changeSchoolAppearance(int thisClass, bool openClose)
    {
        if (spawnerNumber == thisClass)
        {
            foreach (Transform schoolChildren in schoolObj.transform)
            {
                if (schoolChildren.name == "closed")
                {
                    
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
