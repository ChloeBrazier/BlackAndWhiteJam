using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreendoorFinish : MonoBehaviour
{
    //left and right screen objects
    public GameObject leftScreen;
    public GameObject rightScreen;

    //start position transforms
    public Transform leftScreenStartPos;
    public Transform rightScreenStartPos;

    //final position transforms
    public Transform leftScreenFinalPos;
    public Transform rightScreenFinalPos;

    //bool to check if the doors are closed
    private bool doorsClosed = false;

    //closing and opening time
    public float doorTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //debug screen doors closing
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    if(doorsClosed == false)
        //    {
        //        //start sliding the doors closed
        //        StartCoroutine(CloseDoors());
        //    }
        //    else
        //    {
        //        //start sliding the doors open
        //        StartCoroutine(OpenDoors());
        //    }
            
        //}
    }

    /// <summary>
    /// closes doors using smoothstep for easing in and out
    /// </summary>
    /// <returns></returns>
    public IEnumerator CloseDoors()
    {
        //temp vectors for screen door positions
        Vector3 tempLeft = leftScreen.transform.position;
        Vector3 tempRight = rightScreen.transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / doorTime)
        {
            //move the screen doors using smoothstep
            tempLeft.x = Mathf.SmoothStep(leftScreenStartPos.position.x, leftScreenFinalPos.position.x, i);
            tempRight.x = Mathf.SmoothStep(rightScreenStartPos.position.x, rightScreenFinalPos.position.x, i);

            //actually apply the transformation to the doors
            leftScreen.transform.position = tempLeft;
            rightScreen.transform.position = tempRight;

            //loop through again
            yield return null;
        }

        //shake the screen 
        CameraShake.instance.ShakeCamera(2.0f, 0.1f);

        //doors are now closed
        doorsClosed = true;
    }

    /// <summary>
    /// opens doors using smoothstep for easing in and out
    /// </summary>
    /// <returns></returns>
    public IEnumerator OpenDoors()
    {
        //temp vectors for screen door positions
        Vector3 tempLeft = leftScreen.transform.position;
        Vector3 tempRight = rightScreen.transform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / doorTime)
        {
            //move the screen doors using smoothstep
            tempLeft.x = Mathf.SmoothStep(leftScreenFinalPos.position.x, leftScreenStartPos.position.x, i);
            tempRight.x = Mathf.SmoothStep(rightScreenFinalPos.position.x, rightScreenStartPos.position.x, i);

            //actually apply the transformation to the doors
            leftScreen.transform.position = tempLeft;
            rightScreen.transform.position = tempRight;

            //loop through again
            yield return null;
        }

        //doors are now open
        doorsClosed = false;
    }
}
