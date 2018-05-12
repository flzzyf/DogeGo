using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zyf : MonoBehaviour 
{
    public static Zyf instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    Dictionary<bool, bool> bools = new Dictionary<bool, bool>();

    bool ifBoolChanged(ref bool _bool)
    {
        bool previous = true;

        if (!bools.ContainsKey(_bool))   //以加入
        {
            bools.Add(_bool, _bool);
        }
        else
        {
            previous = bools[_bool];

        }

        if (_bool != previous)
            return true;
        else
            return false;

    }

    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
}
