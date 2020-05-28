using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kids : MonoBehaviour
{

    public int condition = -1; // 0= healthy, 1= sick

    public int currentPlace = -1; // 0 - spawner, 1 - falling, 2- cough, 3-ground

    public Material[] possibleConditions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeMaterial(int condition)
    {
        switch (condition)
        {
            case 0:
                transform.GetComponentInChildren<Renderer>().material = possibleConditions[0];
                break;
            case 1:
                transform.GetComponentInChildren<Renderer>().material = possibleConditions[1];
                break;
            default:
                break;
        }

    }
}
