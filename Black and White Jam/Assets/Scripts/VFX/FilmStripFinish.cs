﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FilmStripFinish : MonoBehaviour
{
    //left and right screen objects
    [SerializeField] private GameObject leftScreen;
    [SerializeField] private GameObject rightScreen;

    //start position transforms
    [SerializeField] private Transform leftScreenStartPos;
    public Transform rightScreenStartPos;

    //final position transforms
    [SerializeField] private Transform leftScreenFinalPos;
    [SerializeField] private Transform rightScreenFinalPos;

    //bool to check if the doors are closed
    private bool doorsClosed = false;

    //time 
    public float doorTime;

    //debug input map 
    private InputMap debugScreens;

    //bool to check if the film strip is moving
    private bool stripMoving = false;

    private void Awake()
    {
        debugScreens = new InputMap();
    }

    private void OnEnable()
    {
        debugScreens.Enable();
    }

    private void OnDisable()
    {
        debugScreens.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        debugScreens.Debug.Screendoors.started += StartStrip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStrip(InputAction.CallbackContext ctx)
    {
        if (stripMoving) return;
        
        //start rolling the film

    }

    /// <summary>
    /// rolls the strip onscreen using smoothstep
    /// </summary>
    /// <returns></returns>
    public IEnumerator CloseDoors()
    {
        //doors are moving
        stripMoving = true;

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
        //CameraShake.instance.ShakeCamera(closeIntensity, 0.1f);

        //the film strip is now stationary
        stripMoving = false;
    }

    /// <summary>
    /// rolls the strip offscreen using smoothstep
    /// </summary>
    /// <returns></returns>
    public IEnumerator OpenDoors()
    {
        //doors are moving
        stripMoving = true;

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

        //the strip is now stationary
        stripMoving = false;
    }
}
