using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger script that counts up and changes object properties every time a kid object touches the school prefab
/// </summary>

public class schoolTriggerCounter : MonoBehaviour
{

    GameObject parentSchool;

    //public GameObject manager;

    private void Start()
    {
        parentSchool = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kids")
        {
            parentSchool.GetComponent<spawner>().kidsInThisClass.Add(other.gameObject);
            parentSchool.GetComponent<spawner>().currNumStudents++;

            
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.transform.position = this.transform.position;

        }
    }
}
