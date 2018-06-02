using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMode : MonoBehaviour 
{
    #region Singleton
    public static MapMode instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    public Transform player;
    public Transform world;
    public GameObject dogePrefab;
    public float dogeDistance = 3f;

    MapCameraControl mapCameraControl;

    public Toggle toggleAutoFacingNorth;

	void Start () 
    {
        mapCameraControl = GetComponent<MapCameraControl>();

        Input.compass.enabled = true;

        InitCreateObjects();
	}
	
	void Update () 
    {
        //人物朝向旋转
        float compassNorth = Input.compass.trueHeading;
        player.localEulerAngles = Vector3.up * compassNorth;

        //镜头旋转
        if (toggleAutoFacingNorth.isOn)
        {
            mapCameraControl.RotateViewHorizontal(compassNorth);
        }
	}

    void InitCreateObjects()
    {
        int mapSize = 30;
        int count = (int)(mapSize / dogeDistance);
        int dogeCount = 4;

        List<Vector3> pos = new List<Vector3>();

        for (float i = -count; i < count; i += dogeDistance)
        {
            for (float j = -count; j < count; j += dogeDistance)
            {
                pos.Add(new Vector3(i, 0, j));

            }
        }

        for (int i = 0; i < dogeCount; i++)
        {
            int index = Random.Range(0, pos.Count);
            pos.RemoveAt(index);
            Instantiate(dogePrefab, pos[index], Quaternion.EulerRotation(Vector3.up * Random.Range(0, 360)), world);

        }

    }

    /*
    public void ToggleAutoFacing()
    {
        autoFacingNorth = _toggle.isOn;
        GameManager.instance.SetText("自动朝向开启", autoFacingNorth.ToString());

    }*/
}
