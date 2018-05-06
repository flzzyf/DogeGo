using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            ShowAd("reward");
        }
	}

    public void ShowAd(string _id)
    {
        if (Advertisement.IsReady(_id))
        {
            Advertisement.Show(_id);
        }
    }
}
