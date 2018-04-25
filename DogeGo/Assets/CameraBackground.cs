using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBackground : MonoBehaviour
{
    RawImage image;
    WebCamTexture camTexture;

	void Start ()
    {
        image = GetComponent<RawImage>();
        camTexture = new WebCamTexture(Screen.width, Screen.height);

        image.texture = camTexture;
        camTexture.Play();
	}
	
	void Update () {
		
	}
}
