using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPeople : MonoBehaviour
{

    public GameObject raycasterObj;
    public GameObject colliderSpace;

    public GameObject personPrefab;

    public List<GameObject> allSickPeople;

    public float radBetweenPeople = 2f;

    public Material[] possibleConditions;


    Vector3 spaceSize;
    Vector3 spacePosition;

    Vector3 personSize;

    float timer;

    public float spawnTime = 3f;

    float spawnOverTimer = 0f;
    float maxTimeToWait = 2f;


    int xNumber;
    int zNumber;

    List<GameObject> allPossibleObjs;

    List<Vector3> emptySpaces;

    List<bool> isStillEmpty;

    public int numEmpty = 40;



    // Start is called before the first frame update
    void Start()
    {
        spaceSize = colliderSpace.transform.localScale;
        spacePosition = colliderSpace.transform.position;

        personSize = personPrefab.transform.localScale;

        allSickPeople = new List<GameObject>();

        emptySpaces = new List<Vector3>();

        isStillEmpty = new List<bool>();

        xNumber = (int)Mathf.Round(spaceSize.x / personSize.x);
        zNumber = (int)Mathf.Round(spaceSize.z / personSize.z);

        allPossibleObjs = new List<GameObject>();

        for (int i = 0; i <= xNumber; i++)
        {
            float xPos = (spacePosition.x - spaceSize.x/2) + (personSize.x) * i;

            

            for (int j = 0; j <= zNumber; j++)
            {
                //int emptyChance = Random.Range(0, 2);

                //if (emptyChance == 0)
                //{
                //    continue;
                //}

                float zPos = (spacePosition.z - spaceSize.z / 2) + (personSize.z) * j;

                GameObject currPerson = Instantiate(personPrefab, new Vector3(xPos,1,zPos ), Quaternion.identity);

                currPerson.GetComponent<Rigidbody>().isKinematic = true;

                currPerson.layer = 9;

                changeMaterial(0, currPerson);

                allPossibleObjs.Add(currPerson);

                isStillEmpty.Add(true);
            }
        }

        
        

        while (numEmpty>0)
        {
            int index = Random.Range(0, allPossibleObjs.Count - 1);

            emptySpaces.Add(allPossibleObjs[index].transform.position);

            Destroy(allPossibleObjs[index]);
            allPossibleObjs.RemoveAt(index);

            numEmpty--;
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= spawnTime)
        {

            for (int i = 0; i < emptySpaces.Count; i++)
            {

                if (isStillEmpty[i])
                {

                    Vector3 spawnPos = emptySpaces[i];

                    spawnPos.y = 20;

                    raycasterObj.transform.position = spawnPos;

                    GameObject sickPerson = Instantiate(personPrefab, raycasterObj.transform.position, Quaternion.identity);

                    
                    changeMaterial(2, sickPerson);

                    allSickPeople.Add(sickPerson);


                    isStillEmpty[i] = false;

                    break;
                }
            }


            //int numToSpawn = 3;
            //int counter = 0;
            //while (counter <= numToSpawn)
            //{
            //    bool foundEmptyPos = false;

            //    spawnOverTimer += Time.fixedDeltaTime;

            //    while (!foundEmptyPos || spawnOverTimer >= maxTimeToWait)
            //    {
            //        Vector3 spawnPos_caster = spacePosition + new Vector3(Random.Range(-spaceSize.x / 2, spaceSize.x / 2), 10, Random.Range(-spaceSize.z / 2, spaceSize.z / 2));

            //        raycasterObj.transform.position = spawnPos_caster;


            //        Debug.DrawRay(raycasterObj.transform.position, -raycasterObj.transform.up * 1000f, Color.red);

            //        RaycastHit hit;
            //        int layerMask = 1 << 8;

            //        if (Physics.Raycast(raycasterObj.transform.position, -raycasterObj.transform.up, out hit, Mathf.Infinity, layerMask))
            //        {
            //            int layerMaskPeople = 1 << 9;

            //            Collider[] hitColliders = Physics.OverlapSphere(hit.point, radBetweenPeople, layerMaskPeople);

            //            if (hitColliders.Length == 0)
            //            {
            //                GameObject houseCurr = Instantiate(personPrefab, raycasterObj.transform.position, Quaternion.identity);

            //                allPeople.Add(houseCurr);

            //                foundEmptyPos = true;

            //                counter++;

            //                spawnOverTimer = 0;
            //            }

            //        }
            //    }
            //}




            timer = 0;
        }


    }



    public void changeMaterial(int condition, GameObject objChangeMat)
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
