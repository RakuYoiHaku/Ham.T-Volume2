using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager 사용을 위한 네임스페이스

public class MiniGameManager : MonoBehaviour
{
    // 게임을 리스타트하는 메서드
    public void RestartGame()
    {
        Time.timeScale = 1f;
        // 현재 씬을 다시 로드하여 게임을 처음 상태로 돌아가게 함
        if (SceneManager.GetSceneByName("MiniGameScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MiniGameScene");
            SceneManager.LoadScene("MiniGameScene", LoadSceneMode.Additive);
        }
    }
}
