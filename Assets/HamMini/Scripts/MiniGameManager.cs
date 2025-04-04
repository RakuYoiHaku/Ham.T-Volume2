using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager ����� ���� ���ӽ����̽�

public class MiniGameManager : MonoBehaviour
{
    // ������ ����ŸƮ�ϴ� �޼���
    public void RestartGame()
    {
        Time.timeScale = 1f;
        // ���� ���� �ٽ� �ε��Ͽ� ������ ó�� ���·� ���ư��� ��
        if (SceneManager.GetSceneByName("MiniGameScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MiniGameScene");
            SceneManager.LoadScene("MiniGameScene", LoadSceneMode.Additive);
        }
    }
}
