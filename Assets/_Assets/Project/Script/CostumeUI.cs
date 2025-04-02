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

    public Button sunflowerHat_btn; // ��ư ������
    public Button bearHat_btn;
    public Button chefHat_btn;
    public Button quit_btn;

    [SerializeField] Sprite nullImage;
    [SerializeField] Sprite sunflowerHat_icon;
    [SerializeField] Sprite bearHat_icon;
    [SerializeField] Sprite chefHat_icon;

    private int selectedCostumeId = 0;

    private Costume costume;
    //private Dictionary<int, GameObject> costumeButtons = new Dictionary<int, GameObject>(); // ��ư ����

    private void Start()
    {
        // ��ư �ʱ�ȭ
        ResetButtons();

        // ����� �ڽ�Ƭ �ҷ��� ����
        int savedCostumeId = PlayerPrefs.GetInt("SelectedCostume", 0);
        ApplyCostume(savedCostumeId);

        quit_btn.interactable = true;
        quit_btn.onClick.AddListener(() => AdditiveSceneLoader.Instance.CloseScene());
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

    public void ApplyCostume(int costumeId)
    {
        Costume.Instance.ActiveCostume(0);
        Costume.Instance.ActiveCostume(costumeId);
    }

}
