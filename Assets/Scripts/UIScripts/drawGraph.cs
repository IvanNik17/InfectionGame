using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for collecting data and drawing the graphs for the captured values. Six graphs are drawn - for sick people in society, hospitalized people and one graph for the mental health of each of the schools
/// The data is taken from GlobalScoreKeeper class.
/// Methods implemented in the class:
/// - makeAxes - captures how big the drawing space for the graph is
/// - setupPoints - creates a list of object points that will be used to draw the graph. A point is created for each day in the level
/// - CreateDotConnection - creates lines between the dots for a connected graph visualization
/// - GetAngleFromVectorFloat - calculates the angle between each two dots so the lines can be oriented correctly
/// - putTextOnGraph - draws labels on the top of the graph
/// - putImageNextToGraph - puts the specific image to the left of the graph and moves it's position depending on where the last point's X and Y position on the graph are
/// - makeLine_horizontal - draw a horizontal line from beginning of X axis to the end
/// - makeLine_vertical - draw a vertical line from bottom of Y axis to the top of it
/// - moveTime - move time vertical axis depending on the slider bar
/// - ShowGraph - draws the graph points and graph lines depending on the values given as input in a list. The values are normalized to fit inside the graph space
/// - endGraphShow - combines all the above functions to draw the full graph, depending on whichGraph value one of the six graphs is drawn
/// </summary>


public class drawGraph : MonoBehaviour
{

    List<GameObject> pointsInfected;
    List<GameObject> pointsHospitalized;
    List<GameObject> pointsUnhappy_1_3;
    List<GameObject> pointsUnhappy_4_6;
    List<GameObject> pointsUnhappy_7_9;
    List<GameObject> pointsUnhappy_10_12;

    RectTransform graphContainer;

    GameObject vertLinePoint;

    GameObject horizLinePoint;

    public float graphMaxYvalue;

    public float graphMaxXvalue;

    public float offset = 10f;

    public int whichGraph; // 0 - infected, 1-hospitalized, 2- unhappy1, 3- unhappy2, 4- unhappy3, 5-unhappy4


    public GameObject graphText;

    public GameObject graphImage;

    // Start is called before the first frame update

    private void Awake()
    {
        GlobalEvents.current.onShowStatisticsEvent += endGraphShow;

        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();


        vertLinePoint = new GameObject("dotConnection", typeof(Image));
        vertLinePoint.transform.SetParent(graphContainer, false);


        horizLinePoint = new GameObject("dotConnection", typeof(Image));
        horizLinePoint.transform.SetParent(graphContainer, false);



        switch (whichGraph)
        {
            case 0:
                pointsInfected = setupPoints(globalTimer.current.maxDays, Color.red);
                graphMaxYvalue = globalScoreKeeper.current.maxSickSociety + offset;
                makeLine_horizontal(horizLinePoint, graphMaxYvalue, globalScoreKeeper.current.maxSickSociety, Color.gray);
                putTextOnGraph("Maksimum inficerede mennesker");
                break;
            case 1:
                pointsHospitalized = setupPoints(globalTimer.current.maxDays, Color.blue);
                graphMaxYvalue = globalScoreKeeper.current.maxHospitalCapacity + offset;
                makeLine_horizontal(horizLinePoint, graphMaxYvalue, globalScoreKeeper.current.maxHospitalCapacity, Color.gray);
                putTextOnGraph("Hospitalets kapacitet");
                break;
            case 2:
                pointsUnhappy_1_3 = setupPoints(globalTimer.current.maxDays, new Color(1f,0.42f,0f));
                graphMaxYvalue = globalScoreKeeper.current.maxDaysSchoolClosed + offset;
                makeLine_horizontal(horizLinePoint, graphMaxYvalue, globalScoreKeeper.current.maxDaysSchoolClosed, Color.gray);
                putTextOnGraph("Depression");
                break;
            case 3:
                pointsUnhappy_4_6 = setupPoints(globalTimer.current.maxDays, new Color(1f, 0.42f, 0f));
                graphMaxYvalue = globalScoreKeeper.current.maxDaysSchoolClosed + offset;
                makeLine_horizontal(horizLinePoint, graphMaxYvalue, globalScoreKeeper.current.maxDaysSchoolClosed, Color.gray);
                putTextOnGraph("Depression");
                break;
            case 4:
                pointsUnhappy_7_9 = setupPoints(globalTimer.current.maxDays, new Color(1f, 0.42f, 0f));
                graphMaxYvalue = globalScoreKeeper.current.maxDaysSchoolClosed + offset;
                makeLine_horizontal(horizLinePoint, graphMaxYvalue, globalScoreKeeper.current.maxDaysSchoolClosed, Color.gray);
                putTextOnGraph("Depression");
                break;
            case 5:
                pointsUnhappy_10_12 = setupPoints(globalTimer.current.maxDays, new Color(1f, 0.42f, 0f));
                graphMaxYvalue = globalScoreKeeper.current.maxDaysSchoolClosed + offset;
                makeLine_horizontal(horizLinePoint, graphMaxYvalue, globalScoreKeeper.current.maxDaysSchoolClosed, Color.gray);
                putTextOnGraph("Depression");
                break;
            default:
                break;
        }
    }



    public void makeAxes(float xMaximum, float yMaximum, int deltaXAxis, int deltaYAxis)
    {
        //RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        RectTransform labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        RectTransform labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();



        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;
        //float totalSpace = (graphWidth - xMaximum) / xMaximum + 1;



        for (int i = 0; i <= deltaXAxis; i++)
        {
            //float xPosition = i * totalSpace + 10f;
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            float normalizedValue = (i * 1f) / deltaXAxis;
            labelX.anchoredPosition = new Vector2(normalizedValue * graphWidth, -5f);
            labelX.GetComponent<Text>().text = (normalizedValue * xMaximum).ToString();


        }


        for (int i = 0; i <= deltaYAxis; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = (i * 1f) / deltaYAxis;
            labelY.anchoredPosition = new Vector2(-5f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = (normalizedValue * yMaximum).ToString();
        }


    }



    public List<GameObject> setupPoints(float xMaximum, Color currColor)
    {
        List<GameObject> dotList = new List<GameObject>();

        for (int i = 0; i < xMaximum; i++)
        {
            //GameObject circleGameObject = CreateCircle(new Vector2(0, 0));

            GameObject gameObject = new GameObject("dotConnection", typeof(Image));

            //RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = currColor;

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

            gameObject.SetActive(false);
            dotList.Add(gameObject);
        }

        return dotList;
    }


    public void ShowGraph(List<GameObject> dotList, List<float> valueList, float yMaximum, float xMaximum)
    {

        //RectTransform graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();


        float graphHeight = graphContainer.sizeDelta.y;

        

        float graphWidth = graphContainer.sizeDelta.x;

        
        float totalSpace = (graphWidth - xMaximum) / xMaximum + 1;

        //Debug.Log(totalSpace);

        Vector2 lastPos = new Vector2(-1, -1);
        for (int i = 0; i < valueList.Count; i++)
        {

            //float xPosition = totalSpace/2 + i * totalSpace;

            float xPosition = ((float)(i+1f) / xMaximum) * graphWidth;
            

            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = dotList[i];
            circleGameObject.SetActive(true);

            Vector2 currPos = new Vector2(xPosition, yPosition);


            if (lastPos.x != -1)
            {
                CreateDotConnection(circleGameObject, lastPos, currPos);
            }
            else
            {
                CreateDotConnection(circleGameObject, Vector2.zero, currPos);
            }
            lastPos = currPos;


        }




    }

    private void CreateDotConnection(GameObject currObj, Vector2 dotPositionA, Vector2 dotPositionB)
    { // ,Color currColor


        RectTransform rectTransform = currObj.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }





    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }


    public void putTextOnGraph(string textMsg)
    {

        RectTransform textTransform = graphText.GetComponent<RectTransform>();


        textTransform.anchoredPosition = horizLinePoint.GetComponent<RectTransform>().anchoredPosition;

        graphText.GetComponent<Text>().text = textMsg;

    }

    public void putImageNextToGraph(float yPos, float yMaximum, float xPos, float xMaximum)
    {

        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;



        RectTransform imageTransform = graphImage.GetComponent<RectTransform>();

        float xPosition = (xPos / xMaximum) * graphWidth;


        float yPosition = (yPos / yMaximum) * graphHeight;


        imageTransform.anchorMin = new Vector2(0, 0);
        imageTransform.anchorMax = new Vector2(0, 0);

        imageTransform.anchoredPosition = new Vector2(xPosition, yPosition);

        graphImage.transform.SetAsLastSibling();

    }


    public void makeLine_horizontal(GameObject linePoint ,float yMaximum, float lineMarkerHeight, Color currColor)
    {
        
        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;


        GameObject gameObject = linePoint;

        gameObject.GetComponent<Image>().color = currColor;

        CreateDotConnection(gameObject, new Vector2(0, (lineMarkerHeight / yMaximum) * graphHeight), new Vector2(graphWidth, (lineMarkerHeight / yMaximum) * graphHeight));
    }

    public void makeLine_vertical(GameObject linePoint, float xMaximum, float lineMarkerWidth, Color currColor)
    {

        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;


        GameObject gameObject = linePoint;


        //gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = currColor;

        CreateDotConnection(gameObject, new Vector2( (lineMarkerWidth / xMaximum) * graphWidth,0), new Vector2( (lineMarkerWidth / xMaximum) * graphWidth, graphHeight));
    }



    public void moveTime(float currDay)
    {

        makeLine_vertical(vertLinePoint, globalTimer.current.maxDays, currDay, Color.blue);
    }


    public void endGraphShow()
    {

        //makeLine_horizontal(30, 10, Color.blue);

        //Debug.Log("here");

        

        makeLine_vertical(vertLinePoint, globalTimer.current.maxDays, globalTimer.current.daysPassed, Color.blue);

        switch (whichGraph)
        {
            case 0:
                //List<float> valueListInfected = globalScoreKeeper.current.infectedEachDay;
                ShowGraph(pointsInfected, globalScoreKeeper.current.infectedEachDay, graphMaxYvalue, (float)globalTimer.current.maxDays);
                putImageNextToGraph(globalScoreKeeper.current.infectedEachDay[globalScoreKeeper.current.infectedEachDay.Count - 1], graphMaxYvalue, (float)globalTimer.current.daysPassed, (float)globalTimer.current.maxDays);
                break;
            case 1:
                //List<float> valueListHospitalized = globalScoreKeeper.current.inHospitalEachDay;
                ShowGraph(pointsHospitalized, globalScoreKeeper.current.inHospitalEachDay, graphMaxYvalue, (float)globalTimer.current.maxDays);
                putImageNextToGraph(globalScoreKeeper.current.inHospitalEachDay[globalScoreKeeper.current.inHospitalEachDay.Count - 1], graphMaxYvalue, (float)globalTimer.current.daysPassed, (float)globalTimer.current.maxDays);
                break;
            case 2:

                //List<float> valueListUnhappy_1_3 = new List<float>() { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15 };

                //List<float> valueListUnhappy_1_3 = globalScoreKeeper.current.happinessClassesEachDay[0];
                ShowGraph(pointsUnhappy_1_3, globalScoreKeeper.current.happinessClassesEachDay[0], graphMaxYvalue, (float)globalTimer.current.maxDays);
                putImageNextToGraph(globalScoreKeeper.current.happinessClassesEachDay[0][globalScoreKeeper.current.happinessClassesEachDay[0].Count - 1], graphMaxYvalue, (float)globalTimer.current.daysPassed, (float)globalTimer.current.maxDays);
                break;

            case 3:
                //List<float> valueListUnhappy_4_6 = new List<float>() { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                ShowGraph(pointsUnhappy_4_6, globalScoreKeeper.current.happinessClassesEachDay[1], graphMaxYvalue, (float)globalTimer.current.maxDays);
                putImageNextToGraph(globalScoreKeeper.current.happinessClassesEachDay[1][globalScoreKeeper.current.happinessClassesEachDay[1].Count - 1], graphMaxYvalue, (float)globalTimer.current.daysPassed, (float)globalTimer.current.maxDays);
                break;
            case 4:
                //List<float> valueListUnhappy_7_9 = new List<float>() { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                ShowGraph(pointsUnhappy_7_9, globalScoreKeeper.current.happinessClassesEachDay[2], graphMaxYvalue, (float)globalTimer.current.maxDays);
                putImageNextToGraph(globalScoreKeeper.current.happinessClassesEachDay[2][globalScoreKeeper.current.happinessClassesEachDay[2].Count - 1], graphMaxYvalue, (float)globalTimer.current.daysPassed, (float)globalTimer.current.maxDays);
                break;
            case 5:
                //List<float> valueListUnhappy_10_12 = new List<float>() { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                ShowGraph(pointsUnhappy_10_12, globalScoreKeeper.current.happinessClassesEachDay[3], graphMaxYvalue, (float)globalTimer.current.maxDays);
                putImageNextToGraph(globalScoreKeeper.current.happinessClassesEachDay[3][globalScoreKeeper.current.happinessClassesEachDay[3].Count - 1], graphMaxYvalue, (float)globalTimer.current.daysPassed, (float)globalTimer.current.maxDays);
                break;
            default:
                break;
        }


        















    }

}
