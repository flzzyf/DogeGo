using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour 
{
    public string key = "pk.eyJ1IjoiZmx6enlmIiwiYSI6ImNqZ2xzdTRqdTAzc3gzM2swaGJtYXhheXIifQ.7uQWwSTsaR6K8ysctPZO4Q";

    public string style = "light-v9";

    public Vector2 size;

    public int zoom = 15;

    public Renderer mapPlane;

    Vector2 originPos;
    Vector2 posOffset = Vector2.zero;

	void Start () 
    {
        if (!Input.location.isEnabledByUser)
        {
            GameManager.instance.SetText("警告", "定位服务无法使用");
            return;
        }

        StartCoroutine(StartLoadLocation());

	}
	
	void Update () 
    {
        if (!Input.location.isEnabledByUser)
        {
            return;
        }


        GameManager.instance.SetText("loc",
            Input.location.lastData.longitude + "," +
            Input.location.lastData.latitude);

        if (originPos == Vector2.zero)
        {
            originPos = new Vector2(Input.location.lastData.longitude,
                                Input.location.lastData.latitude);

            GameManager.instance.SetText("originPos", originPos.ToString("f5"));

            return;

        }

        posOffset = new Vector2(Input.location.lastData.longitude - originPos.x,
                                Input.location.lastData.latitude - originPos.y);

        if(posOffset.x != 0 || posOffset.y != 0)
        {
            GameManager.instance.SetText("GPS移动偏移", posOffset.ToString("f5"));

            mapPlane.material.SetTextureOffset("_MainTex", posOffset);
        }
	}

    IEnumerator StartLoadLocation()
    {
        float maxLoadingTime = 10f;

        Input.location.Start(10, 5);

        while(Input.location.status == LocationServiceStatus.Initializing && maxLoadingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            maxLoadingTime--;
        }

        if(maxLoadingTime <= 0)
        {
            GameManager.instance.SetText("错误", "位置信息加载失败");
        }
        else
        {
            GameManager.instance.SetText("咕", "位置信息加载完成");

            StartCoroutine(LoadMap(zoom));

        }
    }

    IEnumerator LoadMap(float _zoom = 16)
    {
        float lon = Input.location.lastData.longitude;
        float lat = Input.location.lastData.latitude;

        string url = string.Format("https://api.mapbox.com/styles/v1/mapbox/{0}/static/{1},{2},{3},0/{4}x{5}@2x?access_token={6}&attribution=false&logo=false",
                                   style, lon, lat, _zoom, size.x, size.y, key);

        WWW www = new WWW(url);
        while (!www.isDone)
            yield return new WaitForSeconds(0.1f);

        mapPlane.material.mainTexture = www.texture;
    }
}
