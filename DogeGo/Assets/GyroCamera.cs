using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour 
{
    public Transform worldObj;
    float startY;
    Quaternion rotationFix;

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
        if(gyroSupported && startY == 0)
        {
            ResetGyroRotation();
        }

        transform.rotation = gyroscope.attitude * rotationFix;
	}

    void ResetGyroRotation()
    {
        startY = transform.eulerAngles.y;
        worldObj.rotation = Quaternion.Euler(0, startY, 0);
    }
}
