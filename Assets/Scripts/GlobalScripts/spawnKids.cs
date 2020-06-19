using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code for spawning and dropping kids objects each level.
/// Methods present in the class:
/// - spawnKidNow() - called when spawnKids event is called, chooses a spawner at random and spawns a kid object with a weighted chance to be sick or healthy. Depending on that a different prefab is chosen
/// - sickHealthyChance() - weighted random chance to spawn sick or healthy
/// - slideKidNow() - called when slideKids event is called, chooses a spawner with spawned kids inside at random and slides a object from it to fall down and be caught.
/// </summary>

public class spawnKids : MonoBehaviour
{
    // Start is called before the first frame update

#pragma warning disable 0649
    [SerializeField]
    private GameObject kidPrefab;

    [SerializeField]
    private GameObject kidPrefabSick;

    [SerializeField]
    private GameObject[] allSpawners;
#pragma warning restore 0649

    int whichSpawnerSpawn = 0;
    int whichSpawnerSlide = 0;

    
    void Start()
    {



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

        List<GameObject> openSpawners = new List<GameObject>();

        foreach (GameObject spawner in allSpawners)
        {
            //This part is so we lower the amount of infected when we close school. (I)
            if (spawner.GetComponent<spawner>().isOpen)
            {
                openSpawners.Add(spawner);
            }
        }

        if (openSpawners.Count == 0)
        {
            return;
        }


        whichSpawnerSpawn = Random.Range(0, allSpawners.Length);

        //This part is so we lower the amount of infected when we close school. (II)
        if (!allSpawners[whichSpawnerSpawn].GetComponent<spawner>().isOpen)
        {
            return;
        }



        int currCondition = sickHealthyChance();


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


                currKid.GetComponent<kids>().condition = currCondition;

                currKid.GetComponent<kids>().currentPlace = 0;
            }
        }
        

    }

    void slideKidNow()
    {

        List<GameObject> openSpawners = new List<GameObject>();

        foreach (GameObject spawner in allSpawners)
        {
            //This part is so we lower the amount of infected when we close school. (I)
            if (spawner.GetComponent<spawner>().isOpen)
            {
                openSpawners.Add(spawner);
            }
        }

        if (openSpawners.Count == 0)
        {
            return;
        }

        whichSpawnerSlide = Random.Range(0, allSpawners.Length);


        //This part is so we lower the amount of infected when we close school. (II)
        if (!allSpawners[whichSpawnerSlide].GetComponent<spawner>().isOpen)
        {
            return;
        }

        List<GameObject> currSpawnerKids = allSpawners[whichSpawnerSlide].GetComponent<spawner>().kidsInThisClass;
        //List<int> currPlacesOfKids = allSpawners[whichSpawnerSlide].GetComponent<spawner>().placeOfKids;

        if (currSpawnerKids.Count>0)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

            for (int i = 0; i < currSpawnerKids.Count; i++)
            {
                if (currSpawnerKids[i]!=null && currSpawnerKids[i].GetComponent<kids>().currentPlace == 0)
                {
                    foreach (Transform childTrans in allSpawners[whichSpawnerSlide].transform)
                    {
                        if (childTrans.name == "kidSliderPoint")
                        {

                            allSpawners[whichSpawnerSlide].GetComponent<spawner>().currNumStudents--;
                            currSpawnerKids[i].transform.position = childTrans.position + randomOffset;
                            currSpawnerKids[i].transform.rotation = Quaternion.Euler(-90 + Random.Range(-50f, 50f), 0 + Random.Range(-50f, 50f), -180 + Random.Range(-50f, 50f));

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
