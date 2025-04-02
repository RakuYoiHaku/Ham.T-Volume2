using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursor; // 기본 커서 이미지
    public Texture2D clickCursor;  // 클릭 시 커서 이미지

    private static CursorManager _instance;
    public static CursorManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<CursorManager>();
            return _instance;
        }
    }

    UIManager _uiManager;

    private void Awake()
    {
        DontDestroyOnLoad(Instance.gameObject);

        _uiManager = FindObjectOfType<UIManager>();

        _uiManager.PauseEvent += SetCursorVisable;
    }

    private void Start()
    {
        // 게임 시작 시 기본 커서 설정
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 왼쪽 클릭
        {
            // 클릭 시 커서를 clickCursor로 변경
            Cursor.SetCursor(clickCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))  // 클릭 해제
        {
            // 클릭을 떼면 다시 normalCursor로 변경
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void SetCursorVisable(bool value)
    {
        Cursor.visible = value;

        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
