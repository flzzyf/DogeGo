using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    public bool movingObj = false;

    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

    }

    void Update () 
    {
        if (movingObj)
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

        }

    }

}
