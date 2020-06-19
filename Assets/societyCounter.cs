using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger script for when sick objects reach the society. The script counts how many infected have been missed and visualizes the number of missed infected, as a percentage by coloring houses in red
/// The current infected in society and maximum possible are used to calculate a normalized value and divide it by the number of visual houses.
/// The script also shows appropriate animations when a sick object is missed.
/// </summary>

public class societyCounter : MonoBehaviour
{


    // Start is called before the first frame update

    public List<GameObject> housesInSociety;

    List<bool> houseInfected;

    Vector3 societyArea;
    Vector3 societyCenter;

    public GameObject sadbubble;

    int infectShowCounter = 1;


    void Start()
    {
        societyArea = this.transform.localScale;
        societyCenter = this.transform.position;

        housesInSociety = new List<GameObject>();

        houseInfected = new List<bool>();

        foreach (Transform childTransf in transform.parent.transform)
        {
            if (childTransf.name != "counterZoneSociety")
            {
                housesInSociety.Add(childTransf.gameObject);
                houseInfected.Add(false);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kids")
        {
            if (other.GetComponent<kids>().condition == 0)
            {
                globalScoreKeeper.current.numberHealthySociety++;


            }
            else if (other.GetComponent<kids>().condition == 1)
            {
                globalScoreKeeper.current.numberSickSociety++;

                Vector3 sadbubblePos = societyCenter + new Vector3(Random.Range(-societyArea.x / 2, societyArea.x / 2), 0, 0);
                sadbubble.transform.position = sadbubblePos;

                sadbubble.GetComponentInChildren<Animator>().SetTrigger("showBubble");


                GetComponent<AudioSource>().Play();

                //float percentageSick = ((float)globalScoreKeeper.current.numberSickSociety / (float)globalScoreKeeper.current.maxSickSociety) * 100f;

                float oneHouseChunk = (float)globalScoreKeeper.current.maxSickSociety / 10f;

                //Debug.Log((float)globalScoreKeeper.current.numberSickSociety  + " | " + oneHouseChunk * (float)infectShowCounter);
                //Debug.Log((float)globalScoreKeeper.current.numberSickSociety + " | " + (float)globalScoreKeeper.current.maxSickSociety);
               // if (percentageSick % 10 == 0)
                if (oneHouseChunk * (float)infectShowCounter <= (float)globalScoreKeeper.current.numberSickSociety)
                {
                    infectShowCounter++;
                    for (int i = 0; i < housesInSociety.Count; i++)
                    {

                        if (!houseInfected[i])
                        {
                            foreach (Transform subHouse in housesInSociety[i].transform)
                            {
                                if (subHouse.name == "HomeSusceptible")
                                {
                                    subHouse.gameObject.SetActive(false);

                                }

                                if (subHouse.name == "HomeInfected")
                                {
                                    subHouse.gameObject.SetActive(true);

                                }


                            }
                            houseInfected[i] = true;
                            break;
                        }

                    }
                }


            }

            Destroy(other.gameObject);
        }
    }
}