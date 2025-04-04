using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoader : MonoBehaviour
{
    private static AdditiveSceneLoader _instance;

    public static AdditiveSceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AdditiveSceneLoader>();
                

                if (_instance == null)
                {
                    GameObject obj = new GameObject("AdditiveSceneLoader");
                    _instance = obj.AddComponent<AdditiveSceneLoader>();
                    DontDestroyOnLoad(obj);
                }
            }

            return _instance;
        }
    }

    //원하는 타이밍에 씬 업로드도 가능
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenScene();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseScene();
        }
    }

    public void OpenScene()
    {
        //UIScene이 이미 로드되어 있지 않으면 Additive로 로드
        if (!SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.LoadScene("CostumeScene", LoadSceneMode.Additive);
            Debug.Log("CostumeScene을 Additive 모드로 로드함");
        }
    }

    public void CloseScene()
    {
        if (SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("CostumeScene");
            Debug.Log("CostumeScene 언로드 요청");
        }
    }
}
