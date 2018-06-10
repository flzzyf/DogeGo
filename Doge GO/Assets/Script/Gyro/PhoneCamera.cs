using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour 
{
    public RawImage image;
    WebCamTexture camTexture;

    public AspectRatioFitter aspectRatioFitter;

    Texture defaultBackground;

    bool cameraAvailable = false;

	void Start () 
    {
        defaultBackground = image.texture;

        EnableCamera();
    }

    public void EnableCamera()
    {
        WebCamDevice[] device = WebCamTexture.devices;

        if (device.Length == 0)
        {
            //GameManager.instance.t[2].text = "无可用摄像头";

            return;
        }

        //GameManager.instance.t[2].text = "可用摄像头数：" + device.Length;

        for (int i = 0; i < device.Length; i++)
        {
            if (!device[i].isFrontFacing)
            {
                camTexture = new WebCamTexture(device[i].name, Screen.width, Screen.height);

                camTexture.Play();
                image.texture = camTexture;

                cameraAvailable = true;
            }
        }
    }

	void Update () 
    {
        if (!cameraAvailable)
            return;

        float ratio = camTexture.width / camTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        float scaleY = camTexture.videoVerticallyMirrored ? -1 : 1;
        image.rectTransform.localScale = new Vector3(1, scaleY, 1);

        int orient = -camTexture.videoRotationAngle;
        image.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
	}
}
