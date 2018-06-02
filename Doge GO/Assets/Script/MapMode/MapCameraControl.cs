﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraControl : MonoBehaviour
{
    public float scaleRate = 1;

    public Vector2 scaleLimit = new Vector2(0, 8);

    public Transform cam;
    public Transform camParent;

    float currentDistance;  //当前两点距离

    public Transform world;

    float scaleValue = 0.5f;

    public Vector2 camAngle;
    public Vector2 camDistance;

    public GameObject sky;
    public Transform compass;

	private void Start()
	{
        RotateViewVertical(scaleValue);
        compass.eulerAngles = Vector3.forward * Input.compass.trueHeading;

	}

	void Update()
    {


        if (!Input.touchSupported)
            return;

        if (Input.touchCount > 1)    //多点齐下
        {
            float distanceChange = TouchControl.instance.doubleTouchScaleValue;

            scaleValue -= distanceChange;

            scaleValue = Mathf.Clamp01(scaleValue);

            //GameManager.instance.SetText("scaleValue", scaleValue.ToString("f4"));

            RotateViewVertical(scaleValue);

        }
        else if(Input.touchCount == 1) //单点
        {
            float touchMovedX = TouchControl.instance.singleTouchMovedValue.x;

            RotateViewHorizontal(touchMovedX);
        }
    }

    void RotateViewVertical(float _value)
    {
        float angle = (camAngle.y - camAngle.x) * _value + camAngle.x;
        camParent.eulerAngles = new Vector3(angle, 0, 0);

        Vector3 pos = cam.localPosition;
        pos.z = (camDistance.y - camDistance.x) * _value + camDistance.x;
        cam.localPosition = pos;

    }

    float skyOffset = 0;

    float skyRotateRate = 6f;

    public void RotateViewHorizontal(float _amount)
    {
        world.Rotate(-Vector3.up * _amount, Space.World);

        compass.Rotate(Vector3.forward * _amount, Space.World);

        skyOffset += _amount * Time.deltaTime / skyRotateRate;

        sky.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(skyOffset, 0));
    }

    void SetViewRotationHorizontal()
    {
        
    }

}
