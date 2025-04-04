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

    //���ϴ� Ÿ�ֿ̹� �� ���ε嵵 ����
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
        //UIScene�� �̹� �ε�Ǿ� ���� ������ Additive�� �ε�
        if (!SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.LoadScene("CostumeScene", LoadSceneMode.Additive);
            Debug.Log("CostumeScene�� Additive ���� �ε���");
        }
    }

    public void CloseScene()
    {
        if (SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("CostumeScene");
            Debug.Log("CostumeScene ��ε� ��û");
        }
    }
}
