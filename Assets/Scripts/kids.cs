using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for each kid object, it keeps track of the condition of the object - sick or healthy, as well as the current place of the object.
/// The script also implements a IEnumerator method for the movement coroutine of object movement from the catcher to the hospital. The coroutine is started in the hopistalUpload script.
/// </summary>

public class kids : MonoBehaviour
{

    public int condition = -1; // 0= healthy, 1= sick

    public int currentPlace = -1; // 0 - spawner, 1 - falling, 2- cough, 3-ground

    public Material[] possibleConditions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeMaterial(int condition)
    {
        switch (condition)
        {
            case 0:
                transform.GetComponentInChildren<Renderer>().material = possibleConditions[0];
                break;
            case 1:
                transform.GetComponentInChildren<Renderer>().material = possibleConditions[1];
                break;
            default:
                break;
        }

    }


    public IEnumerator MoveToPosition(Vector3 startPos, Vector3 newPosition, float time)
    {
        float elapsedTime = 0f;

        float upwardsT = 0f;

        float heightRand = Random.Range(2f, 5f);

        

        while (elapsedTime < time)
        {

            if (elapsedTime < (time / 2))
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

        transform.position = newPosition;

        Destroy(this.gameObject);
    }
}
