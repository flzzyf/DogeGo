using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraControl : MonoBehaviour 
{
    public float scaleRate = 1;

	void Start () {
		
	}
	
	void Update () 
    {
        if(Input.touchCount > 1)    //多点齐下
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                
            }
        }
	}
}
