using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchParticle : MonoBehaviour 
{
    public GameObject touchParticle;

    Dictionary<int, GameObject> touchParticles = new Dictionary<int, GameObject>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 particlePos = GetScreenPoint(touch.position, 7);

                    GameObject go = Instantiate(touchParticle, particlePos, Quaternion.identity);
                    touchParticles.Add(touch.fingerId, go);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 particlePos = GetScreenPoint(touch.position, 7);

                    touchParticles[touch.fingerId].transform.position = particlePos;

                    //GameManager.instance.SetText("touchPos", particlePos.ToString());

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    Destroy(touchParticles[touch.fingerId], 1f);
                    touchParticles.Remove(touch.fingerId);
                }

            }
        }
    }

    Vector3 GetScreenPoint(Vector2 _point, float _distance)
    {
        Vector3 point = new Vector3(_point.x, _point.y, _distance);

        point = Camera.main.ScreenToWorldPoint(point);

        return point;
    }
}
