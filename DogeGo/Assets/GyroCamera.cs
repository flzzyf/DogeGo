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

        GameObject camParent = new GameObject("CamParent");

        if(gyroSupported)
        {
            print("Gyro Supported!");
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90, 180, 0);
            rotationFix = new Quaternion(0, 0, 1, 0);
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
