using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTest : MonoBehaviour
{
    public Text[] t;

    void Start ()
    {
        Input.gyro.enabled = true;
	}

    Vector3 rotationNow = Vector3.zero;

	void Update ()
    {
        rotationNow += new Vector3(Input.gyro.rotationRate.y, Input.gyro.rotationRate.x, 0);

        t[0].text = Input.gyro.attitude.ToString();
        t[1].text = rotationNow.ToString();
        t[2].text = Input.gyro.rotationRate.ToString();

        transform.rotation = Quaternion.Euler(rotationNow);
    }
}
