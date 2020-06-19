using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schoolSelector : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip open;
    public AudioClip close;

    public int whichClass;
    public bool isSelected = true;

    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    void OnMouseDown()
    {

        if (gameEndInitializer.current.whatEnd == -1)
        {
            isSelected = !isSelected;

            if (isSelected)
            {
                GetComponent<AudioSource>().clip = open;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().clip = close;
                GetComponent<AudioSource>().Play();
            }
            GlobalEvents.current.toggleClasses(whichClass, isSelected);
        }

        
    }
}
