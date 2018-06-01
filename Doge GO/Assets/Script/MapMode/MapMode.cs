using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Start () 
    {
        Input.compass.enabled = true;

        InitCreateObjects();
	}
	
	void Update () 
    {
        float compassNorth = Input.compass.trueHeading;

        player.localEulerAngles = Vector3.up * compassNorth;
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
            Instantiate(dogePrefab, pos[index], Quaternion.identity, world);

        }

    }
}
