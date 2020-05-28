using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnKids : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject kidPrefab;


    public GameObject kidPrefabSick;

    //public GameObject kidSpawner;
    //public GameObject kidSlider;

    public GameObject[] allSpawners;

    int whichSpawnerSpawn = 0;
    int whichSpawnerSlide = 0;

    //public List<GameObject> allKidObjects;

    //public int maxNumKids = 100;

    //public int kidCounter = 0;

   

    //int slideCounter = 0;
    
    void Start()
    {

        //allSpawners = GameObject.FindGameObjectsWithTag("spawners");

        //allKidObjects = new List<GameObject>();

        GlobalEvents.current.onSpawnKids += spawnKidNow;

        GlobalEvents.current.onSlideKids += slideKidNow;
    }

    int sickHealthyChance()
    {
        float chance = Random.Range(0f, 1f);

        int condition = -1;

        if (chance > globalScoreKeeper.current.percentHealthyToSick)
        {
            condition = 0; // healthy
        }
        else
        {
            condition = 1; // sick
        }

        return condition;

    }



    void spawnKidNow()
    {
        //Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

        


        whichSpawnerSpawn = Random.Range(0, allSpawners.Length);

        

        int currCondition = sickHealthyChance();

        

        //allSpawners[whichSpawnerSpawn].GetComponent<spawner>().currNumStudents++;

        GameObject currKid = null;

        Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

        foreach (Transform childTrans in allSpawners[whichSpawnerSpawn].transform)
        {
            if (childTrans.name == "kidSpawnerPoint")
            {

                if (currCondition == 0)
                {
                    currKid = Instantiate(kidPrefab, childTrans.position + randomOffset, Quaternion.Euler(-90 + Random.Range(-50f,50f),0 + Random.Range(-50f, 50f), -180 + Random.Range(-50f, 50f)));
                }
                else
                {
                    currKid = Instantiate(kidPrefabSick, childTrans.position + randomOffset, Quaternion.Euler(-90 + Random.Range(-50f, 50f), 0 + Random.Range(-50f, 50f), -180 + Random.Range(-50f, 50f)));
                }
                
                //allSpawners[whichSpawnerSpawn].GetComponent<spawner>().kidsInThisClass.Add(currKid);

                //allSpawners[whichSpawnerSpawn].GetComponent<spawner>().placeOfKids.Add(0);

                currKid.GetComponent<kids>().condition = currCondition;

                currKid.GetComponent<kids>().currentPlace = 0;
               // currKid.GetComponent<kids>().changeMaterial(currCondition);
            }
        }
        

    }

    void slideKidNow()
    {

        List<GameObject> openSpawners = new List<GameObject>();

        foreach (GameObject spawner in allSpawners)
        {
            //This part is so we lower the amount of infected when we close school. (I)
            // if (spawner.GetComponent<spawner>().isOpen)
            //{
            openSpawners.Add(spawner);
            //}
        }

        if (openSpawners.Count == 0)
        {
            return;
        }

        whichSpawnerSlide = Random.Range(0, openSpawners.Count);


        //This part is so we lower the amount of infected when we close school. (II)
        if (!openSpawners[whichSpawnerSlide].GetComponent<spawner>().isOpen)
        {
            return;
        }

        List<GameObject> currSpawnerKids = openSpawners[whichSpawnerSlide].GetComponent<spawner>().kidsInThisClass;
        //List<int> currPlacesOfKids = allSpawners[whichSpawnerSlide].GetComponent<spawner>().placeOfKids;

        if (currSpawnerKids.Count>0)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

            for (int i = 0; i < currSpawnerKids.Count; i++)
            {
                if (currSpawnerKids[i]!=null && currSpawnerKids[i].GetComponent<kids>().currentPlace == 0)
                {
                    foreach (Transform childTrans in openSpawners[whichSpawnerSlide].transform)
                    {
                        if (childTrans.name == "kidSliderPoint")
                        {

                            openSpawners[whichSpawnerSlide].GetComponent<spawner>().currNumStudents--;
                            currSpawnerKids[i].transform.position = childTrans.position + randomOffset;
                            currSpawnerKids[i].GetComponent<Rigidbody>().isKinematic = false;
                            currSpawnerKids[i].GetComponent<SkinnedMeshRenderer>().enabled = true;
                            currSpawnerKids[i].GetComponent<SphereCollider>().enabled = true;
                            currSpawnerKids[i].GetComponent<kids>().currentPlace = 1;
                            
                        }
                        
                    }

                    
                    break;
                }
            }


            
        }
    }



   
}
