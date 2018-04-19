using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour {

    Gyroscope gyroscope;
    bool gyroSupported;

	void Start () 
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        if(gyroSupported)
        {
            print("Gyro Supported!");
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
        }
	}
	
	void Update () 
    {
        transform.rotation = gyroscope.attitude;
	}
}
