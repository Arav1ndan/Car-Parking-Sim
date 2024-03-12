using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    public AudioSource RunningSound;
    public float RunningMaxVolume;
    public float runningMaxPitch;
    public AudioSource idelSound;
    public float idelMaxVolume;
  

    private CarController carController;
    private float speedRatio;
    public float newMaxSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
       // carController.GetSpeed(newMaxSpeed);
    }

  
    void Update()
    {
        if(carController!= null)
        {
            //float currentSpeed = carController.GetSpeed(newMaxSpeed);
        }
        idelSound.volume = Mathf.Lerp(0.1f,idelMaxVolume,speedRatio);
        RunningSound.volume = Mathf.Lerp(0.3f,runningMaxPitch,speedRatio);
        RunningSound.pitch = Mathf.Lerp(0.3f,runningMaxPitch,speedRatio );
    }
}
