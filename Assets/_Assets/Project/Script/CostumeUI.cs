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
                var prefab = Resources.Load<CostumeUI>("CostumeUInew");
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

    //[SerializeField] Sprite nullImage;
    //[SerializeField] Sprite sunflowerHat_icon;
    //[SerializeField] Sprite bearHat_icon;
    //[SerializeField] Sprite chefHat_icon;

    private int selectedCostumeId = 0;

    private Costume costume;
    //private Dictionary<int, GameObject> costumeButtons = new Dictionary<int, GameObject>(); // ��ư ����

    private void Awake()
    {
        // ��ư �ʱ�ȭ
        ResetButtons();
        SkinButton(0);
    }

    private void OnEnable()
    {

        // ����� �ڽ�Ƭ �ҷ��� ����
        int savedCostumeId = PlayerPrefs.GetInt("SelectedCostume", 0);
        SelectCostume(savedCostumeId);

        // ����� ��Ų �ҷ��� ���� (���ٸ� �⺻�� 0)
        int savedSkinId = PlayerPrefs.GetInt("SelectedSkined", 0);
        SelectSkin(savedSkinId);

        // �ڽ�Ƭ ���� ���ο� ���� ��ư Ȱ��ȭ
        for(int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt($"HasCostume_{i}", 0) == 1)
            {
                UpdateBtn(i);
            }
        }

        quit_btn.interactable = true;
        quit_btn.onClick.AddListener(() => AdditiveSceneLoader.Instance.CloseCostumeScene());

        //���� ��ư�� ��� �߰�
        reset_btn.onClick.AddListener(ResetToDefault);
    }

    /// <summary>
    /// ��� ��ư�� ��������Ʈ�� �ʱ�ȭ
    /// </summary>
    private void ResetButtons()
    {
        sunflowerHat_btn.interactable = false;
        bearHat_btn.interactable = false;
        chefHat_btn.interactable = false;
        skin1_btn.interactable = false;
        skin2_btn.interactable = false;
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
                sunflowerHat_btn.interactable = true;  // ��ư Ȱ��ȭ
                break;
            case 2:
                bearHat_btn.interactable = true;  // ��ư Ȱ��ȭ
                break;
            case 3:
                chefHat_btn.interactable = true;  // ��ư Ȱ��ȭ
                break;
        }
    }

    public void SelectCostume(int costumeId)
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
    public Button skinUI_btn0;


    [SerializeField] Button skin1_btn;
    [SerializeField] Button skin2_btn;
    //[SerializeField] GameObject skinPanel; 

    private SkinChange skinChange;

    private int selectedSkinId = 0;

    public void SkinButton(int skinId)
    {
        switch (skinId)
        {
            case 0:
                skin1_btn.interactable = true;
                skin1_btn.onClick.RemoveAllListeners();
                skin1_btn.onClick.AddListener(() => SelectSkin(0));
                break;
            case 1:
                skin2_btn.interactable = true;
                skin2_btn.onClick.RemoveAllListeners();
                skin2_btn.onClick.AddListener(() => SelectSkin(1));
                Debug.Log("��Ų1 ����");
                break;
        }
    }

    public void SelectSkin(int skinId)
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
