using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_interactionVisualization : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainVisCamera;

    public GameObject daysVisualizer;
    List<GameObject> classesCapacityVisualizers;

    public GameObject hospitalCapacityVisualizer;
    //public GameObject societyCapacityVisualizer;
    public GameObject ambulanceCapacityVisualizer;

    public GameObject endScreen;
    public GameObject endTextField;

    public GameObject ambulanceObj;

    ambulanceTrigger ambulanceTrigObj;

    public GameObject[] allSpawners;

    public GameObject[] schoolCapacityVisualizers;


    void Start()
    {

        classesCapacityVisualizers = new List<GameObject>();
        //allSpawners = GameObject.FindGameObjectsWithTag("spawners");

        ambulanceCapacityVisualizer.transform.position = mainVisCamera.WorldToScreenPoint(ambulanceObj.transform.position);

        ambulanceCapacityVisualizer.GetComponent<healthBar>().setMaxValue((float)globalScoreKeeper.current.maxAmbulanceCapacity);

        ambulanceTrigObj = ambulanceObj.GetComponent<ambulanceTrigger>();

        ambulanceCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)ambulanceTrigObj.sickInAmbulance);


        hospitalCapacityVisualizer.GetComponent<healthBar>().setMaxValue((float)globalScoreKeeper.current.maxHospitalCapacity);
        hospitalCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)globalScoreKeeper.current.numberSickHospital);



        GameObject canvasObj = GameObject.Find("Canvas");


        for (int i = 0; i < allSpawners.Length; i++)
        {
            schoolCapacityVisualizers[i].GetComponent<healthBar>().setMaxValue((float)allSpawners[i].transform.GetComponent<spawner>().maxNumStudents);
            schoolCapacityVisualizers[i].GetComponent<healthBar>().setCurrValue((float)allSpawners[i].transform.GetComponent<spawner>().currNumStudents);
        }

        //foreach (GameObject currSpawner in allSpawners)
        //{
        //    //GameObject newTextObj = new GameObject();





        //    //newTextObj.transform.position = mainVisCamera.WorldToScreenPoint(currSpawner.transform.position);
        //    //newTextObj.AddComponent<Text>();

        //    //newTextObj.GetComponent<Text>().font= (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //    //newTextObj.GetComponent<Text>().fontSize = 20;
        //    //newTextObj.GetComponent<Text>().alignment = TextAnchor.LowerCenter;

        //    //newTextObj.GetComponent<Text>().text = "test";
        //    //newTextObj.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 50f);


        //    //newTextObj.name = "classVisual";
        //    //newTextObj.transform.SetParent(canvasObj.transform);
        //    //classesCapacityVisualizers.Add(newTextObj);

            

        //}
    }

    // Update is called once per frame
    void Update()
    {
        daysVisualizer.transform.GetComponent<Text>().text ="Day " + globalTimer.current.daysPassed.ToString() + " / " + globalTimer.current.maxDays.ToString() + "\n" + globalTimer.current.hourPassed.ToString() + ":00";

        //hospitalCapacityVisualizer.transform.GetComponent<Text>().text = "Capacity: " +(globalScoreKeeper.current.numberHealthyHospital + globalScoreKeeper.current.numberSickHospital).ToString()  + "/" + globalScoreKeeper.current.maxHospitalCapacity.ToString() + "\n Healthy: " + globalScoreKeeper.current.numberHealthyHospital.ToString() + "\n" + "Sick: " + globalScoreKeeper.current.numberSickHospital.ToString();
        //hospitalCapacityVisualizer.transform.GetComponent<Text>().text = "Capacity: " + (globalScoreKeeper.current.numberHealthyHospital + globalScoreKeeper.current.numberSickHospital).ToString() + "/" + globalScoreKeeper.current.maxHospitalCapacity.ToString();

        hospitalCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)globalScoreKeeper.current.numberSickHospital);


        //societyCapacityVisualizer.transform.GetComponent<Text>().text = "Healthy: " + globalScoreKeeper.current.numberHealthySociety.ToString() + "\n" + "Sick: " + globalScoreKeeper.current.numberSickSociety.ToString();


        //for (int i = 0; i < allSpawners.Length; i++)
        //{
        //    classesCapacityVisualizers[i].transform.GetComponent<Text>().text = allSpawners[i].transform.GetComponent<spawner>().currNumStudents.ToString() + "/" + allSpawners[i].transform.GetComponent<spawner>().maxNumStudents.ToString();
        //}


        for (int i = 0; i < allSpawners.Length; i++)
        {
            
            schoolCapacityVisualizers[i].GetComponent<healthBar>().setCurrValue((float)allSpawners[i].transform.GetComponent<spawner>().currNumStudents);
        }


        //ambulanceCapacityVisualizer.transform.GetComponent<Text>().text = (ambulanceTrigObj.healthyInAmbulance + ambulanceTrigObj.sickInAmbulance).ToString();

        ambulanceCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)ambulanceTrigObj.sickInAmbulance);
        ambulanceCapacityVisualizer.transform.position = mainVisCamera.WorldToScreenPoint(ambulanceObj.transform.position) + new Vector3(0,-30,0);


        
    }




}
