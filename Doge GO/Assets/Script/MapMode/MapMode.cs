using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMode : MonoBehaviour 
{
    public Transform player;

	void Start () 
    {
        Input.compass.enabled = true;
	}
	
	void Update () 
    {
        float compassNorth = Input.compass.trueHeading;

        player.eulerAngles = Vector3.up * compassNorth;
	}
}
