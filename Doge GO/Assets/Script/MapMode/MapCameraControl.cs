using System.Collections;
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

    public Transform compass;
    public GameObject sky;

    public float test;

	private void Start()
	{
        ChangeViewAngle(scaleValue);

        compass.eulerAngles = Vector3.forward * Input.compass.trueHeading;

	}

	void Update()
    {
        RotateView(test);

        if (!Input.touchSupported)
            return;

        if (Input.touchCount > 1)    //多点齐下
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began ||
               Input.GetTouch(1).phase == TouchPhase.Began) //点击开始
            {
                currentDistance = Vector2.Distance(Input.GetTouch(0).position,
                                                   Input.GetTouch(1).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved ||
                    Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                float formerDistance = currentDistance;

                currentDistance = Vector2.Distance(Input.GetTouch(0).position,
                                                   Input.GetTouch(1).position);

                float distanceChange = currentDistance - formerDistance;

                distanceChange *= scaleRate;
                distanceChange *= Time.deltaTime;

                scaleValue -= distanceChange;

                scaleValue = Mathf.Clamp01(scaleValue);

                //GameManager.instance.SetText("scaleValue", scaleValue.ToString("f4"));

                ChangeViewAngle(scaleValue);
 
            }

        }
        else //单点
        {
            Vector2 lastPos = Input.GetTouch(0).deltaPosition;

            RotateView(lastPos.x);
        }
    }

    void ChangeViewAngle(float _value)
    {
        float angle = (camAngle.y - camAngle.x) * _value + camAngle.x;
        camParent.eulerAngles = new Vector3(angle, 0, 0);

        Vector3 pos = cam.localPosition;
        pos.z = (camDistance.y - camDistance.x) * _value + camDistance.x;
        cam.localPosition = pos;

    }

    float skyOffset = 0;

    float skyRotateRate = 6f;

    void RotateView(float _amount)
    {
        world.Rotate(-Vector3.up * _amount, Space.World);

        compass.Rotate(Vector3.forward * _amount, Space.World);

        skyOffset += _amount * Time.deltaTime / skyRotateRate;
        GameManager.instance.SetText("skyOffset", skyOffset.ToString("f2"));


        sky.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(skyOffset, 0));
    }
}
