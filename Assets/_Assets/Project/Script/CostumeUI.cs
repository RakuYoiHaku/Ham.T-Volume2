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
                // ���� �߰��� �ڽ�Ƭ �Ŵ����� ã�´�.
                _instance = FindObjectOfType<CostumeUI>();
            }

            // ã�ƺ��� ������ ���� ����
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
    /// �ڽ�ƬUI ����
    /// </summary>
    public static void Open()
    {
        Instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// �ڽ�ƬUI �ݱ�
    /// </summary>
    public static void Close()
    {
        Instance.gameObject.SetActive(false);
    }

    public Button costumeUI_btn;
    public Button sunflowerHat_btn; // ��ư ������
    public Button bearHat_btn;
    public Button chefHat_btn;
    public Button quit_btn;
    public Button reset_btn;

    [SerializeField] Sprite nullImage;
    [SerializeField] Sprite sunflowerHat_icon;
    [SerializeField] Sprite bearHat_icon;
    [SerializeField] Sprite chefHat_icon;

    private int selectedCostumeId = 0;

    private Costume costume;
    //private Dictionary<int, GameObject> costumeButtons = new Dictionary<int, GameObject>(); // ��ư ����

    private void Awake()
    {
        // ��ư �ʱ�ȭ
        ResetButtons();
    }

    private void Start()
    {
        ResetSkinButtons(); // ��Ų��ư �ʱ�ȭ

        // ����� �ڽ�Ƭ �ҷ��� ����
        int savedCostumeId = PlayerPrefs.GetInt("SelectedCostume", 0);
        SelectCostume(savedCostumeId);

        // ����� ��Ų �ҷ��� ���� (���ٸ� �⺻�� 0)
        int savedSkinId = PlayerPrefs.GetInt("SelectedSkined", 0);
        SelectSkin(savedSkinId);

        quit_btn.interactable = true;
        quit_btn.onClick.AddListener(() => AdditiveSceneLoader.Instance.CloseScene());

        //���� ��ư�� ��� �߰�
        reset_btn.onClick.AddListener(ResetToDefault);
    }

    /// <summary>
    /// ��� ��ư�� ��������Ʈ�� �ʱ�ȭ
    /// </summary>
    private void ResetButtons()
    {
        sunflowerHat_btn.GetComponent<Image>().sprite = nullImage; // �⺻ �̹����� ����
        bearHat_btn.GetComponent<Image>().sprite = nullImage;
        chefHat_btn.GetComponent<Image>().sprite = nullImage;
    }

    /// <summary>
    /// costumeId�� skinId�� 0���� �����ϴ� �Լ�
    /// </summary>
    private void ResetToDefault()
    {
        // �⺻��(0)���� ����
        selectedCostumeId = 0;
        selectedSkinId = 0;

        // PlayerPrefs�� ����
        PlayerPrefs.SetInt("SelectedCostume", selectedCostumeId);
        PlayerPrefs.SetInt("SelectedSkined", selectedSkinId);
        PlayerPrefs.Save();

        // �⺻ ���� ����
        SelectCostume(0);
        SelectSkin(0);

        Debug.Log("�ڽ�Ƭ�� ��Ų�� �⺻��(0)���� ���µ�!");
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
        Costume.Instance.ActiveCostume(0);  // ��� �ڽ�Ƭ ��Ȱ��ȭ
        Costume.Instance.ActiveCostume(costumeId);
        selectedCostumeId = costumeId;  // ���� ���õ� �ڽ�Ƭ ���� (�ӽ�)

        PlayerPrefs.SetInt("SelectedCostume", selectedCostumeId);
        PlayerPrefs.Save();
        Debug.Log($"�ڽ�Ƭ {selectedCostumeId} ���� �Ϸ�!");
    }

    #region SkinUI
    // ��ŲUI
    public Button skinUI_btn;

    [SerializeField] Button skin1_btn;
    //[SerializeField] GameObject skinPanel; 

    private SkinChange skinChange;

    private int selectedSkinId = 0;

    public void ResetSkinButtons()
    {
        reset_btn.interactable = true; 
    }

    public void SkinButton(int skinId)
    {
        switch (skinId)
        {
            case 0:
                reset_btn.onClick.RemoveAllListeners();
                reset_btn.onClick.AddListener(() => SelectSkin(0));
                break;
            case 1:
                skin1_btn.onClick.RemoveAllListeners();
                skin1_btn.onClick.AddListener(() => SelectSkin(1));
                Debug.Log("��Ų1 ����");
                break;
        }
    }

    private void SelectSkin(int skinId)
    {
        SkinChange.ActiveSkined(0);  // ��� �ڽ�Ƭ ��Ȱ��ȭ
        SkinChange.ActiveSkined(skinId);
        selectedSkinId = skinId;  // ���� ���õ� �ڽ�Ƭ ���� (�ӽ�)

        PlayerPrefs.SetInt("SelectedSkined", selectedSkinId);
        PlayerPrefs.Save();
        Debug.Log($" {selectedSkinId} ���� �Ϸ�!");
    }
    #endregion
}
