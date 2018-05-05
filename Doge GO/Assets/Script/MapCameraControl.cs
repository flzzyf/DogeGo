using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraControl : MonoBehaviour
{
    public float scaleRate = 1;

    public Vector2 scaleLimit = new Vector2(0, 8);

    public Transform cam;

    float currentDistance;  //当前两点距离

    public Transform world;

    void Update()
    {
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
                float scaleValue = Vector2.Distance(Input.GetTouch(0).position,
                                                   Input.GetTouch(1).position)
                                          - currentDistance;
                scaleValue *= scaleRate;
                scaleValue *= Time.deltaTime;

                Vector3 pos = cam.localPosition;
                pos.z *= -1;

                pos.z -= scaleValue;

                pos.z = Mathf.Clamp(pos.z, scaleLimit.x, scaleLimit.y);

                GameManager.instance.SetText("scaleValue", scaleValue.ToString());
                print(scaleValue);

                pos.z *= -1;
                cam.localPosition = pos;

                currentDistance = Vector2.Distance(Input.GetTouch(0).position,
                                                   Input.GetTouch(1).position);
            }

        }
        else //单点
        {
            Vector2 lastPos = Input.GetTouch(0).deltaPosition;

            //GameManager.instance.SetText("lastPos", lastPos.ToString());

            world.Rotate(-Vector3.up * lastPos.x, Space.World);
        }
    }
}
