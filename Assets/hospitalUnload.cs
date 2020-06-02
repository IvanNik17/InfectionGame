using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hospitalUnload : MonoBehaviour
{

    public GameObject bubbleFull;

    private void Start()
    {
        GlobalEvents.current.onDayPassed += getHealthy;  
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ambulance")
        {

            if (other.GetComponent<ambulanceTrigger>().sickInAmbulance == globalScoreKeeper.current.maxAmbulanceCapacity)
            {
                bubbleFull.GetComponent<Animator>().SetTrigger("isEmpty");
                bubbleFull.GetComponent<Animator>().ResetTrigger("isFull");
                
            }



            //Debug.Log("HERE");
            globalScoreKeeper.current.numberHealthyHospital += other.GetComponent<ambulanceTrigger>().healthyInAmbulance;

            globalScoreKeeper.current.numberSickHospital += other.GetComponent<ambulanceTrigger>().sickInAmbulance;

            globalScoreKeeper.current.countHospitalized += other.GetComponent<ambulanceTrigger>().sickInAmbulance;

            other.GetComponent<ambulanceTrigger>().sickInAmbulance = 0;
            other.GetComponent<ambulanceTrigger>().healthyInAmbulance = 0;

            foreach (Transform child in other.transform)
            {
                if (child.name != "Ambulance")
                {
                    this.GetComponent<AudioSource>().Play();

                    GameObject.Destroy(child.gameObject);
                }

            }



            
        }

        

    }


    void getHealthy()
    {
        if (globalScoreKeeper.current.numberSickHospital >0)
        {
            //Debug.Log("BLALBA" + Mathf.Clamp((int)(KeepDataBetweenLevels.keepCurrentLevel / 2), 1, 100000));

            int currNumInHospital = globalScoreKeeper.current.numberSickHospital - Mathf.Clamp((int)(KeepDataBetweenLevels.keepCurrentLevel / 2), 1, 100000);
            globalScoreKeeper.current.numberSickHospital = Mathf.Clamp(currNumInHospital, 0,100000);
        }
    }
}
