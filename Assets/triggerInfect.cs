using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerInfect : MonoBehaviour
{

    // public float radiusInfection = 2f;

    int layerMaskPeople = 1 << 9;

    public Material[] possibleConditions;

    public GameObject managerObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "kids")
        {

            
            
            //collision.transform.GetComponent<Rigidbody>().isKinematic = true;




            //Collider[] hitColliders = Physics.OverlapSphere(collision.GetContact(0).point, managerObj.GetComponent<spawnPeople>().radBetweenPeople*2f, layerMaskPeople);


            //Debug.Log(hitColliders.Length);

            //for (int i = 0; i < hitColliders.Length; i++)
            //{

            //    changeMaterial(1, hitColliders[i].gameObject);
            //}

           // Destroy(collision.gameObject);
        }
    }


     void changeMaterial(int condition, GameObject objChangeMat)
    {
        switch (condition)
        {
            case 0:
                objChangeMat.transform.GetComponentInChildren<Renderer>().material = possibleConditions[0];
                break;
            case 1:
                objChangeMat.transform.GetComponentInChildren<Renderer>().material = possibleConditions[1];
                break;
            case 2:
                objChangeMat.transform.GetComponentInChildren<Renderer>().material = possibleConditions[2];
                break;
            default:
                break;
        }

    }



}
