using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AdditiveSceneLoader : MonoBehaviour
{
    private static AdditiveSceneLoader _instance;
    private HamsterController2 _player;
    [SerializeField] GameObject _playUICanvas;

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

    private void Start()
    {
        _player = FindObjectOfType<HamsterController2>();
    }

    //���ϴ� Ÿ�ֿ̹� �� ���ε嵵 ����
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenCostumeScene();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCostumeScene();
            CloseMGScene();
        }
    }

    public void OpenCostumeScene()
    {
        //UIScene�� �̹� �ε�Ǿ� ���� ������ Additive�� �ε�
        if (!SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.LoadScene("CostumeScene", LoadSceneMode.Additive);
            _playUICanvas.gameObject.SetActive(false);
            if (_player != null)
            {
                _player.allowInput = false; // �Է� ����
            }
            Debug.Log("CostumeScene�� Additive ���� �ε��� + player �Է� ����");
        }
    }

    public void CloseCostumeScene()
    {
        if (SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("CostumeScene");
            _playUICanvas.gameObject.SetActive(true);
            if (_player != null)
            {
                _player.allowInput = true; // �ٽ� �Է� ���
            }
            Debug.Log("CostumeScene ��ε� ��û + player �Է� ���");
        }
    }

    public void OpenMGScene()
    {
        //UIScene�� �̹� �ε�Ǿ� ���� ������ Additive�� �ε�
        if (!SceneManager.GetSceneByName("MiniGameScene").isLoaded)
        {
            SceneManager.LoadScene("MiniGameScene", LoadSceneMode.Additive);
            _playUICanvas.gameObject.SetActive(false);
            if (_player != null)
            {
                _player.allowInput = false; // �Է� ����
            }
            Debug.Log("CostumeScene�� Additive ���� �ε��� + player �Է� ����");
        }
    }

    public void CloseMGScene()
    {
        if (SceneManager.GetSceneByName("MiniGameScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MiniGameScene");
            _playUICanvas.gameObject.SetActive(true);
            if (_player != null)
            {
                _player.allowInput = true; // �ٽ� �Է� ���
            }
            Debug.Log("CostumeScene ��ε� ��û + player �Է� ���");
        }
    }
}
