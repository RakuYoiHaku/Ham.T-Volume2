using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveScebeLoader : MonoBehaviour
{
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //UIScene�� �̹� �ε�Ǿ� ���� ������ Additive�� �ε�
            if (!SceneManager.GetSceneByName("CostumeScene").isLoaded)
            {
                SceneManager.LoadScene("CostumeScene", LoadSceneMode.Additive);
                Debug.Log("CostumeScene�� Additive ���� �ε���");
            }
        }
    }

    //���ϴ� Ÿ�ֿ̹� �� ���ε嵵 ����
    void Update()
    {
        if (SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("CostumeScene");
            Debug.Log("CostumeScene ��ε� ��û");
        }
    }
}
