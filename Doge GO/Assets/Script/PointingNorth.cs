using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingNorth : MonoBehaviour 
{
    public Transform compass;

	void Start () 
    {
        Input.compass.enabled = true;

	}

	private void Update()
	{
        compass.eulerAngles = Vector3.forward * Input.compass.trueHeading;
		
	}

}
