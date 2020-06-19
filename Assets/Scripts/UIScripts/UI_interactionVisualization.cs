using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Global code used for visualizing, moving and changing the values for all UI elements visible on the main play screen - bars for catcher, hospital, schools, pause button, visible clock, etc.
/// The script updates these UI elements in the Update function currently.
/// </summary>

public class UI_interactionVisualization : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera mainVisCamera;

#pragma warning disable 0649
    [SerializeField]
    private GameObject daysVisualizer;
    [SerializeField]
    private GameObject hospitalCapacityVisualizer;
    [SerializeField]
    private GameObject ambulanceCapacityVisualizer;
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private GameObject endTextField;
    [SerializeField]
    private GameObject ambulanceObj;
    [SerializeField]
    private GameObject[] allSpawners;
    [SerializeField]
    private GameObject[] schoolCapacityVisualizers;

#pragma warning restore 0649

    private List<GameObject> classesCapacityVisualizers;
    private ambulanceTrigger ambulanceTrigObj;

    void Start()
    {
        classesCapacityVisualizers = new List<GameObject>();
        mainVisCamera = Camera.main;
        
        ambulanceCapacityVisualizer.transform.position = mainVisCamera.WorldToScreenPoint(ambulanceObj.transform.position);

        ambulanceCapacityVisualizer.GetComponent<healthBar>().setMaxValue((float)globalScoreKeeper.current.maxAmbulanceCapacity);

        ambulanceTrigObj = ambulanceObj.GetComponent<ambulanceTrigger>();

        ambulanceCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)ambulanceTrigObj.sickInAmbulance);


        hospitalCapacityVisualizer.GetComponent<healthBar>().setMaxValue((float)globalScoreKeeper.current.maxHospitalCapacity);
        hospitalCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)globalScoreKeeper.current.numberSickHospital);

        GameObject canvasObj = GameObject.Find("Canvas");

        for (int i = 0; i < allSpawners.Length; i++)
        {
            schoolCapacityVisualizers[i].GetComponent<mentalBar>().setMaxValueMental((float)allSpawners[i].transform.GetComponent<spawner>().maxDaysClosed);
            schoolCapacityVisualizers[i].GetComponent<mentalBar>().setCurrValueMental((float)allSpawners[i].transform.GetComponent<spawner>().daysClosed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        daysVisualizer.transform.GetComponent<Text>().text = "Dag " + globalTimer.current.daysPassed.ToString() + " / " + globalTimer.current.maxDays.ToString() + "\n" + globalTimer.current.hourPassed.ToString() + ":00";

        hospitalCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)globalScoreKeeper.current.numberSickHospital);

        for (int i = 0; i < allSpawners.Length; i++)
        {
            
            schoolCapacityVisualizers[i].GetComponent<mentalBar>().setCurrValueMental((float)allSpawners[i].transform.GetComponent<spawner>().daysClosed);
        }

        ambulanceCapacityVisualizer.GetComponent<healthBar>().setCurrValue((float)ambulanceTrigObj.sickInAmbulance);
        ambulanceCapacityVisualizer.transform.position = mainVisCamera.WorldToScreenPoint(ambulanceObj.transform.position) + new Vector3(0,-30,0);


        
    }




}
