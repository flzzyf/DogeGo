using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

	private void Awake()
	{
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
	}

	public Text[] t;

    public void LoadScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }

}
