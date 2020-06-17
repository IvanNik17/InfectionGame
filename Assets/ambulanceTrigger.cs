using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambulanceTrigger : MonoBehaviour
{

    public int healthyInAmbulance = 0;
    public int sickInAmbulance = 0;

    public GameObject bubble;

    public GameObject fullBubble;

    public GameObject layspaceObj;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kids" && (healthyInAmbulance +sickInAmbulance)<globalScoreKeeper.current.maxAmbulanceCapacity )
        {

            
            
            //if (other.GetComponent<kids>().condition == 0)
            //{
            //    //globalScoreKeeper.current.numberHealthyHospital++;

            //    healthyInAmbulance++;

            //}
            if (other.GetComponent<kids>().condition == 1)
            {
                //globalScoreKeeper.current.numberSickHospital++;

                this.GetComponent<AudioSource>().Play();

                sickInAmbulance++;


                if ((healthyInAmbulance + sickInAmbulance) < globalScoreKeeper.current.maxAmbulanceCapacity)
                {
                    bubble.GetComponent<Animator>().SetTrigger("showBubble");
                }

                

                //other.transform.position = transform.position;
                other.transform.position = layspaceObj.transform.position + new Vector3(Random.Range(-layspaceObj.transform.lossyScale.x / 2, layspaceObj.transform.lossyScale.x / 2), 0, 0);
                other.GetComponent<Animator>().enabled = false;
                other.transform.rotation = Quaternion.Euler(-180f, Random.Range(0f, 180f), 0f);

                other.transform.GetComponent<SphereCollider>().enabled = false;
                other.transform.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.parent = transform;

                //other.transform.GetComponent<SkinnedMeshRenderer>().enabled = false;

                
            }

            

            //Destroy(other.gameObject);
        }

        if ((healthyInAmbulance + sickInAmbulance) == globalScoreKeeper.current.maxAmbulanceCapacity)
        {
            fullBubble.GetComponent<Animator>().SetTrigger("isFull");
            

        }
    }
}
