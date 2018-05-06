using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    string adID = "reward";

	void Start ()
    {
        //Advertisement.Initialize(adID);
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            ShowAd(adID);
        }

        if(Advertisement.IsReady(adID))
        {
            GameManager.instance.SetText("广告状态", "就绪");
        }
        else
        {
            GameManager.instance.SetText("广告状态", "准备中");

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

