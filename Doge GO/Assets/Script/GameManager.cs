using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    #region Singleton
    public static GameManager instance;

	private void Awake()
	{
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
	}
#endregion

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

    public void Test()
    {
        SetText("test", "click");
    }

	private void OnApplicationQuit()
	{
		
	}

	private void OnApplicationPause(bool pause)
	{
        //按下Home键
        Debug.Log("暂停");

        if(pause)
        {
            Debug.Log("pause");

        }
        else
        {
            Debug.Log("!pause");

        }
	}

	private void OnApplicationFocus(bool focus)
	{
        Debug.Log("取消暂停");
		
	}

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("退出");
            Application.Quit();
        }

        if(!Application.isFocused)
        {
            Debug.Log("非激活状态");
            Application.Quit();
        }
	}

}
