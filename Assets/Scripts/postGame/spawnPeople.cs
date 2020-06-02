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

    public List<GameObject> positionMarkers;

    List<GameObject> healthyPeople;

    public GameObject positionPrefab;


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


    List<int> sickOrNot;


    public int numEmpty = 0;

    public int numberOfPeople = 30;

    public int numberSick = 10;

    List<int> chosenIds;

    List<List<int>> sickThroughTime;


    //public int daysPassed;

    public int maxDays;

    public int currDay;

    // Start is called before the first frame update
    void Start()
    {

        //numberOfPeople = (int)(numEmpty / 0.2f);

        //numEmpty = 5;


        //Check if area is big enough
        float areaOfAllSpawnPos = (float)(numberOfPeople) * (Mathf.PI * radBetweenPeople * radBetweenPeople);

        Debug.Log(areaOfAllSpawnPos + " ? " + colliderSpace.transform.localScale.x * colliderSpace.transform.localScale.z);
        if (areaOfAllSpawnPos >= colliderSpace.transform.localScale.x * colliderSpace.transform.localScale.z)
        {
            float sizeDelta = areaOfAllSpawnPos - (colliderSpace.transform.localScale.x * colliderSpace.transform.localScale.z);
            sizeDelta += radBetweenPeople * 5;
            sizeDelta = Mathf.Sqrt(sizeDelta);
            Vector3 newArea = new Vector3(sizeDelta, 0, sizeDelta);
            colliderSpace.transform.localScale += newArea;
        }

        positionMarkers = new List<GameObject>();

        healthyPeople = new List<GameObject>();

        allSickPeople = new List<GameObject>();

        isStillEmpty = new List<bool>();

        chosenIds = new List<int>();


        sickOrNot = new List<int>();

        sickThroughTime = new List<List<int>>();

        spaceSize = colliderSpace.transform.localScale;
        spacePosition = colliderSpace.transform.position;



        int posCounter = 0;

        while (posCounter <= (numberOfPeople))
        {
            Vector3 spawnPos_caster = spacePosition + new Vector3(Random.Range(-spaceSize.x / 2, spaceSize.x / 2), 10, Random.Range(-spaceSize.z / 2, spaceSize.z / 2));

            raycasterObj.transform.position = spawnPos_caster;


            Debug.DrawRay(raycasterObj.transform.position, -raycasterObj.transform.up * 1000f, Color.red);

            RaycastHit hit;
            int layerMask = 1 << 8;

            if (Physics.Raycast(raycasterObj.transform.position, -raycasterObj.transform.up, out hit, Mathf.Infinity, layerMask))
            {
                int layerMaskPeople = 1 << 10;

                Collider[] hitColliders = Physics.OverlapSphere(hit.point, radBetweenPeople, layerMaskPeople);

                if (hitColliders.Length == 0)
                {
                    GameObject spawnPosCurr = Instantiate(positionPrefab, hit.point, Quaternion.identity);

                    positionMarkers.Add(spawnPosCurr);

                    posCounter++;

                    
                }

            }
        }


        int counter = 0;
        while (numberOfPeople > 0)
        {
            int index = Random.Range(0, positionMarkers.Count - 1);

            Vector3 curPos = positionMarkers[index].transform.position;

            curPos.y += 1;

            Destroy(positionMarkers[index]);
            positionMarkers.RemoveAt(index);


            GameObject currHealthy = Instantiate(personPrefab, curPos, Quaternion.identity);

            healthyPeople.Add(currHealthy);

            sickThroughTime.Add(new List<int>());

            sickOrNot.Add(0);



            changeMaterial(0, currHealthy);

            numberOfPeople--;

            counter++;
        }


        

        while (numberSick >= 0)
        {

            for (int i = 0; i < healthyPeople.Count; i++)
            {
                List<int> currList = sickThroughTime[i];

                currList.Add(sickOrNot[i]);

                sickThroughTime[i] = currList;
            }



            bool isSet = false;

            while (!isSet)
            {
                int index = Random.Range(0, healthyPeople.Count - 1);

                if (!chosenIds.Contains(index))
                {

                    //Vector3 curPos = healthyPeople[index].transform.position;

                    //curPos.y += 10;

                    GameObject currPerson = healthyPeople[index];

                    //currPerson.transform.position = curPos;


                    //currPerson.tag = "kids";

                    // changeMaterial(2, currPerson);

                    sickOrNot[index] = 1;

                    numberSick--;


                    chosenIds.Add(index);

                    isSet = true;
                }


            }


        }
        //Debug.Log(sickThroughTime[0].Count);



        //while (numEmpty > 0)
        //{
        //    int index = Random.Range(0, positionMarkers.Count - 1);

        //    Vector3 curPos = positionMarkers[index].transform.position;

        //    curPos.y += 10;

        //    Destroy(positionMarkers[index]);
        //    positionMarkers.RemoveAt(index);

        //    GameObject currSick = Instantiate(personPrefab, curPos, Quaternion.identity);

        //    allSickPeople.Add(currSick);






        //    changeMaterial(2, currSick);

        //    numEmpty--;
        //}


        //for (int i = 0; i < positionMarkers.Count; i++)
        //{
        //    isStillEmpty.Add(true);
        //}


        //personSize = personPrefab.transform.localScale;

        //allSickPeople = new List<GameObject>();

        //emptySpaces = new List<Vector3>();

        //isStillEmpty = new List<bool>();

        //xNumber = (int)Mathf.Round(spaceSize.x / personSize.x);
        //zNumber = (int)Mathf.Round(spaceSize.z / personSize.z);

        //allPossibleObjs = new List<GameObject>();

        //for (int i = 0; i <= xNumber; i+=5)
        //{
        //    float xPos = (spacePosition.x - spaceSize.x/2) + (personSize.x) * i;



        //    for (int j = 0; j <= zNumber; j+=5)
        //    {
        //        //int emptyChance = Random.Range(0, 2);

        //        //if (emptyChance == 0)
        //        //{
        //        //    continue;
        //        //}

        //        float zPos = (spacePosition.z - spaceSize.z / 2) + (personSize.z) * j;

        //        GameObject currPerson = Instantiate(personPrefab, new Vector3(xPos,1,zPos ), Quaternion.identity);

        //        currPerson.GetComponent<Rigidbody>().isKinematic = true;

        //        currPerson.layer = 9;

        //        changeMaterial(0, currPerson);

        //        allPossibleObjs.Add(currPerson);

        //        isStillEmpty.Add(true);
        //    }
        //}




        //while (numEmpty>0)
        //{
        //    int index = Random.Range(0, allPossibleObjs.Count - 1);

        //    emptySpaces.Add(allPossibleObjs[index].transform.position);

        //    Destroy(allPossibleObjs[index]);
        //    allPossibleObjs.RemoveAt(index);

        //    numEmpty--;
        //}



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;



        List<int> currDayVals = new List<int>();
        for (int i = 0; i < sickOrNot.Count; i++)
        {
            currDayVals.Add(sickThroughTime[i][currDay]);

            sickOrNot[i] = sickThroughTime[i][currDay];
        }

        //List<int> test = sickThroughTime[currDay];

        //string str = "";
        //for (int i = 0; i < test.Count; i++)
        //{
        //    str += " | " + test[i];
        //}

        //Debug.Log(str);

        


        for (int i = 0; i < healthyPeople.Count; i++)
        {
            if (sickOrNot[i] == 0)
            {
                changeMaterial(0, healthyPeople[i]);
            }
            else if (sickOrNot[i] == 1)
            {
                changeMaterial(2, healthyPeople[i]);
            }
        }


        if (timer >= spawnTime)
        {

            //for (int i = 0; i < positionMarkers.Count; i++)
            //{

            //    if (isStillEmpty[i])
            //    {

            //        Vector3 spawnPos = positionMarkers[i].transform.position;

            //        spawnPos.y = 20;

            //        raycasterObj.transform.position = spawnPos;

            //        GameObject sickPerson = Instantiate(personPrefab, raycasterObj.transform.position, Quaternion.identity);

            //        sickPerson.tag = "kids";

            //        changeMaterial(2, sickPerson);

            //        //allSickPeople.Add(sickPerson);

            //        //Destroy(positionMarkers[i]);
            //        //positionMarkers.RemoveAt(i);


            //        isStillEmpty[i] = false;

            //        break;
            //    }
            //}




            //if (numberSick > 0)
            //{

            //    for (int i = 0; i < healthyPeople.Count; i++)
            //    {
            //        List<int> currList = sickThroughTime[i];

            //        currList.Add(sickOrNot[i]);

            //        sickThroughTime[i] = currList;
            //    }


                
            //    bool isSet = false;

            //    while (!isSet)
            //    {
            //        int index = Random.Range(0, healthyPeople.Count - 1);

            //        if (!chosenIds.Contains(index))
            //        {

            //            //Vector3 curPos = healthyPeople[index].transform.position;

            //            //curPos.y += 10;

            //            GameObject currPerson = healthyPeople[index];

            //            //currPerson.transform.position = curPos;


            //            //currPerson.tag = "kids";

            //            // changeMaterial(2, currPerson);

            //            sickOrNot[index] = 1;

            //            numberSick--;


            //            chosenIds.Add(index);

            //            isSet = true;
            //        }


            //    }
                

                



            //}



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
