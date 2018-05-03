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

	public Text screenText;

    Dictionary<string, string> txt = new Dictionary<string, string>();

    public void SetText(string _key, string _string)
    {
        txt[_key] = _string;

        string s = "";

        foreach (KeyValuePair<string, string> it in txt)
        {
            s += it.Key;
            s += ":";
            s += it.Value;
            s += "\r\n";
        }

        screenText.text = s;
    }

    public void LoadScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }

    public void Terminate()
    {
        Application.Quit();
    }

}
