using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace ChangeScene
{
    // <YSA> chatGPT
    public class SceneLoader : MonoBehaviour
    {
        public string loadSceneName;
        public GameObject loadingCanvas;

        public void LoadGameScene()
        {
            StartCoroutine(LoadGameSceneCoroutine());
        }

        private IEnumerator LoadGameSceneCoroutine()
        {
            if (loadingCanvas != null) loadingCanvas.SetActive(true);
            VideoManager.Instance.PlayLoadVideo();

            AsyncOperation operation = SceneManager.LoadSceneAsync(loadSceneName);
            operation.allowSceneActivation = false; // 씬 자동 전환 막기

            float minimumWaitTime = Random.Range(3f, 6f);
            float startTime = Time.time;

            // 나머지 씬 로딩은 계속 진행
            while (!operation.isDone)
            {
                while (Time.time - startTime < minimumWaitTime)
                {
                    yield return null; // 매 프레임 체크하여 부드럽게 진행
                }

                if (operation.progress >= 0.9f) // 씬 로딩이 거의 끝날 때
                {
                    operation.allowSceneActivation = true; //  씬 전환
                    yield break; // 종료
                }

                yield return null; // 씬 로딩 진행 상태를 체크
            }
        }
    }
}
