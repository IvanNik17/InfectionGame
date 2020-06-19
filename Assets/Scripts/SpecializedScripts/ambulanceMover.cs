using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for moving the catcher object with the mouse. It captures the mouse's position in screen space and transforms it to world space
/// </summary>

public class ambulanceMover : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private float mYCoord;

    GameObject ambulanceObj;

    public GameObject bubbleObj;

    float prevX;

    Camera mainCam;

    public GameObject minMoveX;
    public GameObject maxMoveX;


    private void Start()
    {

        mainCam = Camera.main;

        foreach (Transform childTrans in transform)
        {
            if (childTrans.name == "Ambulance")
            {
                ambulanceObj = childTrans.gameObject;
            }
        }

        prevX = ambulanceObj.transform.position.x;

        mZCoord = mainCam.WorldToScreenPoint(gameObject.transform.position).z;

        mYCoord = mainCam.WorldToScreenPoint(gameObject.transform.position).y;
    }


    private void Update()
    {

        if (gameEndInitializer.current.whatEnd == -1)
        {
            Vector3 curPos = GetMouseAsWorldPoint();

           


            curPos.x = Mathf.Clamp(curPos.x, minMoveX.transform.position.x, maxMoveX.transform.position.x);

            transform.position = curPos;


            bubbleObj.transform.position = curPos;

            if (prevX < transform.position.x)
            {
                ambulanceObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (prevX > transform.position.x)
            {
                ambulanceObj.transform.rotation = Quaternion.Euler(0, -180, 0);
            }

            prevX = transform.position.x;
        }

        
    }

    //void OnMouseDown()
    //{
    //    mZCoord = mainCam.WorldToScreenPoint(gameObject.transform.position).z;

    //    // Store offset = gameobject world pos - mouse world pos

    //    mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        

    //}



    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.y = mYCoord;
        // z coordinate of game object on screen

        mousePoint.z = mZCoord;

        // Convert it to world points

        return mainCam.ScreenToWorldPoint(mousePoint);
    }



    //void OnMouseDrag()

    //{

    //    Vector3 curPos = GetMouseAsWorldPoint() + mOffset;

    //    curPos.x = Mathf.Clamp(curPos.x, minMoveX.transform.position.x, maxMoveX.transform.position.x);

    //    transform.position = curPos;

    //    if (prevX< transform.position.x)
    //    {
    //        ambulanceObj.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }
    //    else if (prevX > transform.position.x)
    //    {
    //        ambulanceObj.transform.rotation = Quaternion.Euler(0, -180, 0);
    //    }

    //    prevX = transform.position.x;
    //}
}
