using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    string adID = "reward";
    /*
	void Update ()
    {
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
            ShowOptions showOptions = new ShowOptions();
            showOptions.resultCallback = AdOver;

            Advertisement.Show(_id, showOptions);
        }
    }

    void AdOver(ShowResult result)
    {
        if(result == ShowResult.Failed)
        {
            GameManager.instance.SetText("广告播放", "失败");

        }
        else if(result == ShowResult.Skipped)
        {
            GameManager.instance.SetText("广告播放", "跳过");

        }
        else if(result == ShowResult.Finished)
        {
            GameManager.instance.SetText("广告播放", "完成");

        }
    }
     */
}

