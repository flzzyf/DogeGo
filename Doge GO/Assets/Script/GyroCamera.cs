using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroCamera : MonoBehaviour 
{
    Quaternion rotationFix;

    Gyroscope gyroscope;
    bool gyroSupported;

	void Start () 
    {
        Init();
	}

    void Init()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        //GameManager.instance.t[0].text = "Gyro Supported: " + gyroSupported.ToString();

        GameObject camParent = new GameObject("CamParent");
        camParent.transform.position = transform.position;
        transform.parent = camParent.transform;

        if (gyroSupported)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0);
            rotationFix = new Quaternion(0, 0, 1, 0);

        }

        Input.location.Start(10, 5);
    }

	void Update () 
    {
        if (gyroSupported)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;

            transform.localRotation = gyroscope.attitude * rotationFix;

        }
    }

}
