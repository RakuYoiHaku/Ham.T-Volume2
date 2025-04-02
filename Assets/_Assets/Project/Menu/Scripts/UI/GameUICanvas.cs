using UnityEngine;
using UnityEngine.UI;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] Button btnContinue;

    UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();

         btnContinue.onClick.AddListener(OnContinuePressed);
    }

    private void OnContinuePressed()
    {
        if (_uiManager.isPaused == false)
            return;

        _uiManager.TogglePauseMenu();
    }

    private void OnDisable()
    {
        CursorManager.Instance.SetCursorVisable(false);
    }
}
