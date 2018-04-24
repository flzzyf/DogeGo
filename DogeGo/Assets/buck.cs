using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buck : MonoBehaviour
{
    public Text[] t;

    void Update()
    {
        if (buttoned)
        {
            t[2].text = Input.acceleration.ToString();

            Vector3 inputA = Input.acceleration;
            inputA *= 360;

            transform.rotation = Quaternion.Euler(inputA);
        }
    }

    bool buttoned = false;
    public void button()
    {
        buttoned = true;
    }
}
