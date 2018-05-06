using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingNorth : MonoBehaviour 
{
    public Transform compass;

	void Start () 
    {
        Input.compass.enabled = true;

        compass.eulerAngles = Vector3.forward * Input.compass.trueHeading;

	}

	private void Update()
	{
        if (!Input.touchSupported)
            return;

        if(Input.touchCount == 1)
        {
            Vector2 lastPos = Input.GetTouch(0).deltaPosition;

            compass.Rotate(Vector3.forward * lastPos.x, Space.World);
        }

		
	}

}
