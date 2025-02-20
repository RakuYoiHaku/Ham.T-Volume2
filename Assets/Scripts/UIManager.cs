using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            //만약 인스턴스를 찾는 타이밍에 싱글톤이 없다면
            if (m_instance == null)
            {
                //씬 내에서 UI Manager 클래스를 찾아서 싱글톤으로 등록한다.
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance;

    public TMP_Text scoreText;
    public TMP_Text achievementText;

    public void UpdateScoreText(int newScore)
    {
        if(scoreText != null) scoreText.text = " X " + newScore;
    }

    public void UpdateAchieveText(string newText)
    {
        if (achievementText != null) achievementText.text = " Achievement : " + newText;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
