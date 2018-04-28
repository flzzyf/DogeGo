using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour 
{
    RawImage image;
    WebCamTexture camTexture;

    AspectRatioFitter aspectRatioFitter;

    Texture defaultBackground;

	void Start () 
    {
        image = GetComponent<RawImage>();

        defaultBackground = image.texture;

    }

    public void EnableCamera()
    {
        WebCamDevice[] device = WebCamTexture.devices;

        if (device.Length == 0)
        {
            GameManager.instance.t[2].text = "无可用摄像头";

            return;
        }

        GameManager.instance.t[2].text = "可用摄像头数：" + device.Length;


        for (int i = 0; i < device.Length; i++)
        {
            if (!device[i].isFrontFacing)
            {
                camTexture = new WebCamTexture(device[i].name, Screen.width, Screen.height);

                camTexture.Play();
                image.texture = camTexture;
            }
        }
    }

	void Update () {
		
	}
}
