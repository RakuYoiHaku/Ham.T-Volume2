using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumeUI : MonoBehaviour
{
    private static CostumeUI _instance;

    public static CostumeUI Instance
    {
        get
        {
            if (_instance == null)
            {
                // 씬에 추가된 코스튬 매니저를 찾는다.
                _instance = FindObjectOfType<CostumeUI>();
            }

            // 찾아봐도 없으면 새로 생성
            if (_instance == null)
            {
                var prefab = Resources.Load<CostumeUI>("CostumeUI");
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
                Close();
            }

            return _instance;
        }
    }

    /// <summary>
    /// 코스튬UI 열기
    /// </summary>
    public static void Open()
    {
        Instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// 코스튬UI 닫기
    /// </summary>
    public static void Close()
    {
        Instance.gameObject.SetActive(false);
    }

    public Button sunflowerHat_btn; // 버튼 아이콘
    public Button bearHat_btn;
    public Button chefHat_btn;
    public Button quit_btn;

    [SerializeField] Sprite nullImage;
    [SerializeField] Sprite sunflowerHat_icon;
    [SerializeField] Sprite bearHat_icon;
    [SerializeField] Sprite chefHat_icon;

    private int selectedCostumeId = 0;

    private Costume costume;
    //private Dictionary<int, GameObject> costumeButtons = new Dictionary<int, GameObject>(); // 버튼 저장

    private void Start()
    {
        // 버튼 초기화
        ResetButtons();

        // 저장된 코스튬 불러와 적용
        int savedCostumeId = PlayerPrefs.GetInt("SelectedCostume", 0);
        ApplyCostume(savedCostumeId);

        quit_btn.interactable = true;
        quit_btn.onClick.AddListener(() => AdditiveSceneLoader.Instance.CloseScene());
    }

    /// <summary>
    /// 모든 버튼의 스프라이트를 초기화
    /// </summary>
    private void ResetButtons()
    {
        sunflowerHat_btn.GetComponent<Image>().sprite = nullImage; // 기본 이미지로 설정
        bearHat_btn.GetComponent<Image>().sprite = nullImage;
        chefHat_btn.GetComponent<Image>().sprite = nullImage;
    }


    public void UpdateBtn(int costumeId)
    {
        switch (costumeId)
        {
            case 1:
                sunflowerHat_btn.interactable = true;
                sunflowerHat_btn.onClick.RemoveAllListeners();
                sunflowerHat_btn.onClick.AddListener(() => SelectCostume(1));
                ImgSwap(1);
                break;
            case 2:
                bearHat_btn.interactable = true;
                bearHat_btn.onClick.RemoveAllListeners();
                bearHat_btn.onClick.AddListener(() => SelectCostume(2));
                ImgSwap(2);
                break;
            case 3:
                chefHat_btn.interactable = true;
                chefHat_btn.onClick.RemoveAllListeners();
                chefHat_btn.onClick.AddListener(() => SelectCostume(3));
                ImgSwap(3);
                break;
        }
    }

    private void ImgSwap(int costumeId)
    {
        switch (costumeId)
        {
            case 1:
                    sunflowerHat_btn.GetComponent<Image>().sprite = sunflowerHat_icon;
                break;
            case 2:
                    bearHat_btn.GetComponent<Image>().sprite = bearHat_icon;
                break;
            case 3:
                    chefHat_btn.GetComponent<Image>().sprite = chefHat_icon;
                break;
        }
    }

    private void SelectCostume(int costumeId)
    {
        Costume.Instance.ActiveCostume(0);  // 모든 코스튬 비활성화
        Costume.Instance.ActiveCostume(costumeId);
        selectedCostumeId = costumeId;  // 현재 선택된 코스튬 저장 (임시)

        PlayerPrefs.SetInt("SelectedCostume", selectedCostumeId);
        PlayerPrefs.Save();
        Debug.Log($"코스튬 {selectedCostumeId} 저장 완료!");
    }

    public void ApplyCostume(int costumeId)
    {
        Costume.Instance.ActiveCostume(0);
        Costume.Instance.ActiveCostume(costumeId);
    }

}
