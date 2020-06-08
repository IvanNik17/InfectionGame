using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSlerpPosition : MonoBehaviour
{

    public float timeCounter = 0f;

    Vector3 currPos;
    Vector3 destPos;

    //float upwardsT = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currPos = this.transform.position;

        destPos = new Vector3(5, 0, 0);

        //StartCoroutine(movePerson_slow(0, currPos, destPos, 1));

        StartCoroutine(MoveToPosition(currPos, destPos, 3));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //timeCounter += Time.fixedDeltaTime;


        
        //Vector3 slerpPos = Vector3.Slerp(currPos, destPos, timeCounter);

        

        //if (timeCounter < 0.5f)
        //{
        //    upwardsT +=2 * Time.fixedDeltaTime;
        //}
        //else
        //{
        //    upwardsT -= 2 * Time.fixedDeltaTime;
        //}

        ////this.transform.position = slerpPos;

        //this.transform.position = new Vector3(slerpPos.x, Mathf.Lerp(slerpPos.y, slerpPos.y + 5, upwardsT), slerpPos.z);
    }



    //public IEnumerator movePerson_slow(float delayTime, Vector3 oldPositions, Vector3 newPositions, float speedMovement)
    //{

    //    float startTime = Time.time; // Time.time contains current frame time, so remember starting point

    //    float upwardsTime = 0f;
    //    while (Time.time - startTime <= speedMovement)
    //    { // until one second passed

    //        Debug.Log(upwardsTime);

    //        if ((Time.time - startTime) <= speedMovement/2)
    //        {
    //            upwardsTime += ((Time.time - startTime) / (speedMovement*50f) );
    //        }
    //        else
    //        {
    //            upwardsTime -= ((Time.time - startTime) / (speedMovement * 50f));
    //        }



    //        //transform.position = Vector3.Lerp(oldPositions, newPositions, (Time.time - startTime) / speedMovement); // lerp from A to B in one second

    //        Vector3 horizontalPos = Vector3.Lerp(oldPositions, newPositions, (Time.time - startTime) / (speedMovement)); // lerp from A to B in one second

    //        transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(horizontalPos.y, horizontalPos.y + 5f, upwardsTime), horizontalPos.z);
            
    //         yield return 1; // wait for next frame
    //    }

    //    transform.position = newPositions;





    //    yield return new WaitForSeconds(delayTime); // start at time X

    //}


    public IEnumerator MoveToPosition(Vector3 startPos, Vector3 newPosition, float time)
    {
        float elapsedTime= 0f;

        float upwardsT = 0f;

        float heightRand = Random.Range(2f, 5f);

        while (elapsedTime < time)
        {

            if (elapsedTime < (time/2) )
            {
                upwardsT += 2 * Time.deltaTime;
            }
            else
            {
                upwardsT -= 2 * Time.deltaTime;
            }


            Vector3 horizontalPos = Vector3.Lerp(startPos, newPosition, (elapsedTime / time));

            transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(horizontalPos.y, horizontalPos.y + heightRand, upwardsT), horizontalPos.z);
            elapsedTime += Time.deltaTime;
            yield return 1;
        }

        Destroy(this.gameObject);
    }


}
