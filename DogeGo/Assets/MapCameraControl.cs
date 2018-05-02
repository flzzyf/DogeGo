using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraControl : MonoBehaviour 
{
    public float scaleRate = 1;

    public Vector2 scaleLimit = new Vector2(0, 8);

    public Transform cam;

	void Start () {
		
	}

    float currentDistance;  //当前两点距离

	void Update () 
    {
        if(Input.touchCount > 1)    //多点齐下
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began ||
               Input.GetTouch(1).phase == TouchPhase.Began) //点击开始
            {
                currentDistance = Vector2.Distance(Input.GetTouch(0).position, 
                                                   Input.GetTouch(1).position);
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved ||
                    Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                float scaleValue = Vector2.Distance(Input.GetTouch(0).position,
                                                   Input.GetTouch(1).position)
                                          - currentDistance;
                scaleValue *= scaleRate;

                Vector3 pos = transform.position;
                pos.z += scaleValue;

                pos.z = Mathf.Clamp(pos.z, scaleLimit.x, scaleLimit.y);

                GameManager.instance.t[0].text = scaleValue.ToString();

                transform.position = pos;
            }
        }
        else //单点
        {
            float mouseX = Input.GetAxis("Mouse X");

            transform.Rotate(transform.up * mouseX);
        }
	}
}
