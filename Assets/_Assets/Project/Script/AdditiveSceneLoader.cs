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
            //UIScene이 이미 로드되어 있지 않으면 Additive로 로드
            if (!SceneManager.GetSceneByName("CostumeScene").isLoaded)
            {
                SceneManager.LoadScene("CostumeScene", LoadSceneMode.Additive);
                Debug.Log("CostumeScene을 Additive 모드로 로드함");
            }
        }
    }

    //원하는 타이밍에 씬 업로드도 가능
    void Update()
    {
        if (SceneManager.GetSceneByName("CostumeScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("CostumeScene");
            Debug.Log("CostumeScene 언로드 요청");
        }
    }
}
