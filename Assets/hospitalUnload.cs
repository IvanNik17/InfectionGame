using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hospitalUnload : MonoBehaviour
{

    public GameObject bubbleFull;

    public GameObject bubbleFull_hospital;

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

            GameObject[] allSickInAmbulance = new GameObject[other.GetComponent<ambulanceTrigger>().sickInAmbulance];

            other.GetComponent<ambulanceTrigger>().sickInAmbulance = 0;
            other.GetComponent<ambulanceTrigger>().healthyInAmbulance = 0;

            Transform[] childsTransforms = other.transform.GetComponentsInChildren<Transform>();

            foreach (Transform child in childsTransforms)
            {
                if (child.tag == "kids")
                {
                    this.GetComponent<AudioSource>().Play();

                    //GameObject.Destroy(child.gameObject);
                   // child.transform.parent = null;
                    child.transform.GetComponent<SkinnedMeshRenderer>().enabled = true;


                    StartCoroutine(child.GetComponent<kids>().MoveToPosition(other.transform.position, this.transform.position, 1));
                    child.transform.SetParent(null);
                }

            }

            //Debug.Log(((float)globalScoreKeeper.current.numberSickHospital / (float)globalScoreKeeper.current.maxHospitalCapacity));

            


        }



    }


    private void Update()
    {
        if (((float)globalScoreKeeper.current.numberSickHospital / (float)globalScoreKeeper.current.maxHospitalCapacity) > 0.7f)
        {
            bubbleFull_hospital.GetComponent<Animator>().SetTrigger("isFull");
        }
        else
        {
            bubbleFull_hospital.GetComponent<Animator>().SetTrigger("isEmpty");
            bubbleFull_hospital.GetComponent<Animator>().ResetTrigger("isFull");
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
