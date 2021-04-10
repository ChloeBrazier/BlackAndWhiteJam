using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    //singleton instance of this class
    public static CameraShake instance;

    //cinemachine camera
    private CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        //set up singleton
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        //get the cinemachine camera
        cam = GameObject.FindGameObjectWithTag("Cinemachine Vcam").GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin camShake = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        StartCoroutine(ShakeCam(intensity, time, camShake));
    }

    public void ShakeAnimCamera(float time)
    {
        CinemachineBasicMultiChannelPerlin camShake = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        StartCoroutine(ShakeCam(0.5f, time, camShake));
    }

    private IEnumerator ShakeCam(float intensity, float time, CinemachineBasicMultiChannelPerlin shake)
    {
        shake.m_AmplitudeGain = intensity;

        for (float i = 0; i < 1; i += Time.deltaTime / time)
        {
            yield return null;
        }

        shake.m_AmplitudeGain = 0;
    }
}
