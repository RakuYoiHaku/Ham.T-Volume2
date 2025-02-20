using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllRestartGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndLoadScene();
        }
    }
    public void EndLoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
