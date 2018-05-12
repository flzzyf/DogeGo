using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour 
{
    #region Singleton
    public static TouchControl instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    float currentDistance;  //当前两点距离
    [SerializeField]
    float doubleTouchSensitivity = 0.2f;

    [HideInInspector]
    public float doubleTouchScaleValue;

    public Vector2 singleTouchMovedValue;

	void Start () 
	{
        if (!Input.touchSupported)
            this.enabled = false;
	}

	void Update () 
	{
        if (Input.touchCount > 0)
        {
            //有触摸
            if (Input.touchCount == 1)
            {
                //单点触摸
                singleTouchMovedValue = Input.GetTouch(0).deltaPosition;
            }
            else
            {
                //多点触摸，只考虑前两点
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

                    doubleTouchScaleValue = currentDistance - formerDistance;

                    doubleTouchScaleValue *= doubleTouchSensitivity;
                    doubleTouchScaleValue *= Time.deltaTime;

                }

            }
        }
		
	}
}
