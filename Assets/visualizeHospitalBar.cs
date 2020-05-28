using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizeHospitalBar : MonoBehaviour
{

    public GameObject sickBar;
    //public GameObject spaceLeftBar;

    Vector3 initScale_sickBar;
    Vector3 initScale_spaceLeftBar;

    float maxScaleY = 1.40f;

    // Start is called before the first frame update
    void Start()
    {
        initScale_sickBar = sickBar.transform.localScale;
        //initScale_spaceLeftBar = spaceLeftBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        float currSick_normal = (float)globalScoreKeeper.current.numberSickHospital / (float)globalScoreKeeper.current.maxHospitalCapacity;
        //float currFreeSpace_normal = (float)(globalScoreKeeper.current.maxHospitalCapacity - globalScoreKeeper.current.numberSickHospital)/ (float)globalScoreKeeper.current.maxHospitalCapacity;

        float currSick_yScale = currSick_normal * maxScaleY;
        //float currFreeSpace_normal_yScale = currFreeSpace_normal * maxScaleY;


        Vector3 newSickBarScale = new Vector3(initScale_sickBar.x, currSick_yScale, initScale_sickBar.z);
        sickBar.transform.localScale = newSickBarScale;


       // Vector3 newSpaceBarScale = new Vector3(initScale_sickBar.x, currFreeSpace_normal_yScale, initScale_sickBar.z);
        //spaceLeftBar.transform.localScale = newSpaceBarScale;
    }
}
