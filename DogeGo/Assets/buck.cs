using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buck : MonoBehaviour {
    bool draw = false;
    bool gyinfo;
    Gyroscope go;
    void Start()
    {
        gyinfo = SystemInfo.supportsGyroscope;
        go = Input.gyro;
        go.enabled = true;
    }
    void Update()
    {


        if (buttoned)
        {
            t[0].text = go.attitude.eulerAngles.ToString();
            if (gyinfo)
            {
                Vector3 a = go.attitude.eulerAngles;
                a = new Vector3(-a.x, -a.y, a.z); //直接使用读取的欧拉角发现不对，于是自己调整一下符号  
                this.transform.eulerAngles = a;
                this.transform.Rotate(Vector3.right * 90, Space.World);
                draw = false;
            }
            else
            {
                draw = true;
            }
        }
    }
    public Text[] t;

    void OnGUI()
    {
        if (draw)
        {
            GUI.Label(new Rect(100, 100, 100, 30), "启动失败");
        }
    }
    bool buttoned = false;
    public void button()
    {
        buttoned = true;
    }
}
