using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroCamera : MonoBehaviour 
{
    public Transform worldObj;

    float startY;
    Quaternion rotationFix;

    Gyroscope gyroscope;
    bool gyroSupported;

    public Text[] t;

	void Start () 
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        t[0].text = "Gyro Supported: " + gyroSupported.ToString();

        GameObject camParent = new GameObject("CamParent");
        camParent.transform.position = transform.position;
        transform.parent = camParent.transform;

        if(gyroSupported)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0);
            rotationFix = new Quaternion(0, 0, 1, 0);
        }
	}

	void Update () 
    {
        if(gyroSupported && startY == 0)
        {
            ResetGyroRotation();
        }

        if (gyroSupported)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            transform.localRotation = gyroscope.attitude * rotationFix;
            //transform.rotation = gyroscope.attitude;

            t[1].text = "Att: " + Input.gyro.attitude;
            //t[2].text = "Att: " + Input.gyro.attitude.eulerAngles;
        }


    }

    void ResetGyroRotation()
    {
        startY = transform.eulerAngles.y;
        worldObj.rotation = Quaternion.Euler(0, startY, 0);
    }

}
