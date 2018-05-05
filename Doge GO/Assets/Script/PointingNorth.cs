using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingNorth : MonoBehaviour 
{
	void Start () 
    {
        transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);

        Input.compass.enabled = true;
        GameManager.instance.SetText("north", transform.eulerAngles.ToString());
        GameManager.instance.SetText("compass", Input.compass.enabled.ToString());

	}

}
