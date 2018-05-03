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

    Vector2 lastPos;

	void Start () 
    {
        StartCoroutine(LoadMap(zoom));

        Input.location.Start(10, 5);

        lastPos.x = Input.location.lastData.longitude;
        lastPos.y = Input.location.lastData.latitude;

	}
	
	void Update () 
    {
        if(Input.location.lastData.longitude != lastPos.x || 
           Input.location.lastData.latitude != lastPos.y)
        {

            Vector2 offset = new Vector2(Input.location.lastData.longitude - lastPos.x,
                                         Input.location.lastData.latitude - lastPos.y);

            //GameManager.instance.SetText(0, offset.ToString());

            StartCoroutine(LoadMap(zoom));

            lastPos.x = Input.location.lastData.longitude;
            lastPos.y = Input.location.lastData.latitude;
        }

        GameManager.instance.SetText("loc", 
            Input.location.lastData.longitude + "," +
            Input.location.lastData.latitude);



	}

    IEnumerator LoadMap(float _zoom = 16)
    {
        float lon = Input.location.lastData.longitude;
        float lat = Input.location.lastData.latitude;

        if (!Input.location.isEnabledByUser)
        {
            lon = 119.1657f;
            lat = 26.06639f;
        }

        string url = string.Format("https://api.mapbox.com/styles/v1/mapbox/{0}/static/{1},{2},{3},0/{4}x{5}@2x?access_token={6}&attribution=false&logo=false",
                                   style, lon, lat, _zoom, size.x, size.y, key);

        WWW www = new WWW(url);
        while (!www.isDone)
            yield return new WaitForSeconds(0.1f);

        mapPlane.material.mainTexture = www.texture;
    }
}
