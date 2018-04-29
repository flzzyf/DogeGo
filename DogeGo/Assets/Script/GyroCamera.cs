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

            if(Input.location.status == LocationServiceStatus.Initializing)
                GameManager.instance.t[0].text = "初始化中";
            else if (Input.location.status == LocationServiceStatus.Running)
                GameManager.instance.t[0].text = "运转中";

            if (Input.location.isEnabledByUser)
                GameManager.instance.t[1].text = "可用";

            //GameManager.instance.t[1].text = "Rotation: " + transform.eulerAngles;
            GameManager.instance.t[2].text = "location: " + 
                Input.location.lastData.latitude + "," + 
                Input.location.lastData.longitude;

        }
    }

}
