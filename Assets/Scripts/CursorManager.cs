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

    UIManager_HT _uiManager;

    private void Awake()
    {
        DontDestroyOnLoad(Instance.gameObject);

        _uiManager = FindObjectOfType<UIManager_HT>();

        _uiManager.PauseEvent += SetCursorVisable;

        //if (Instance == null)
        //{
            
        //    DontDestroyOnLoad(gameObject); // ✅ 씬이 바뀌어도 유지
        //}
        //else
        //{
        //    Destroy(gameObject); // ✅ 중복 생성 방지
        //}
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
        // 마우스를 화면 중앙에 고정하고 보이지 않게 설정
        //Cursor.lockState = CursorLockMode.Locked;  // 마우스를 화면 중앙에 고정
        //Cursor.visible = false;                    // 마우스를 화면에서 보이지 않게 설정

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
