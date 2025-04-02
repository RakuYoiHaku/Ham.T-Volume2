using UnityEngine;
using UnityEngine.UI;

namespace ChangeScene
{
    // <YSA> chatGPT
    public class LoadScene : MonoBehaviour
    {
        public Button startButton; //  버튼 오브젝트
        public GameObject save_Pop;
        public SceneLoader sceneLoader; // SceneLoader 스크립트 참조

        private void Start()
        {
            // 버튼 클릭 이벤트를 코드에서 직접 등록 (OnClick 사용 X)
            startButton.onClick.AddListener(LoadingPlay);
        }

        public void LoadingPlay()
        {
            save_Pop.SetActive(false);

            sceneLoader.LoadGameScene(); // SceneLoader의 씬 로딩 실행
        }

        private void OnDestroy()
        {
            //씬이 변경될 때 메모리 누수를 방지하기 위해 이벤트 제거
            startButton.onClick.RemoveListener(LoadingPlay);
        }
    }
}
