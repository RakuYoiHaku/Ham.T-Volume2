using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonManager_Game : MonoBehaviour
{
    [Header("Pause")]
    //[SerializeField] Button bTN_Pause_Continue;
    [SerializeField] Button bTN_Pause_Save;
    [SerializeField] Button bTN_Pause_Option;
    [SerializeField] Button bTN_Pause_Quit;

    [Header("Pause_Save")]
    [SerializeField] Button bTN_P_Slot_Quit;

    [Header("Pause_Option")]
    [SerializeField] Button bTN_P_Option_Control;
    [SerializeField] Button bTN_P_Option_Game;
    [SerializeField] Button bTN_P_Option_Quit;

    [Header("Pause_Pop-up")]
    //[SerializeField] Button bTN_P_Save_Yes;
    [SerializeField] Button bTN_P_Save_No;
    [SerializeField] Button bTN_P_Quit_No;

    public Dictionary<ButtonType, Button> buttonDictionary = new Dictionary<ButtonType, Button>();

    private void Awake()
    {
        // ���� Dictionary �ʱ�ȭ ����
        if (buttonDictionary == null)
        {
            buttonDictionary = new Dictionary<ButtonType, Button>();
        }
        else
        {
            buttonDictionary.Clear();
        }

        buttonDictionary = new Dictionary<ButtonType, Button>
        {
            //{ButtonType.Pause_Continue,bTN_Pause_Continue},
            {ButtonType.Pause_Save,bTN_Pause_Save},
            {ButtonType.Pause_Option,bTN_Pause_Option},
            {ButtonType.Pause_Quit,bTN_Pause_Quit},

            {ButtonType.P_Slot_Quit,bTN_P_Slot_Quit},

            {ButtonType.P_Option_Control,bTN_P_Option_Control},
            {ButtonType.P_Option_Game,bTN_P_Option_Game},
            {ButtonType.P_Option_Quit,bTN_P_Option_Quit},

            {ButtonType.P_Save_No,bTN_P_Save_No},
            {ButtonType.P_Quit_No,bTN_P_Quit_No}
        };
    }

    void Start()
    {
        foreach (var kvp in buttonDictionary)
        {
            ButtonType type = kvp.Key;
            Button button = kvp.Value;

            if (button != null)
            {
                // ���� �̺�Ʈ ���� �� �� �̺�Ʈ �߰�
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Pause_OnButtonClicked(type));
            }
        }
    }

    void Pause_OnButtonClicked(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Pause_Save:
                UIPanelManager.Instance.ShowPanel("Save", false);
                break;
            case ButtonType.Pause_Option:
                UIPanelManager.Instance.ShowPanel("Option", false);
                UIPanelManager.Instance.ShowPanel("Option_Game", true);
                break;
            case ButtonType.Pause_Quit:
                UIPanelManager.Instance.ShowPanel("Quit_P", true);
                break;
            case ButtonType.P_Option_Control:
                UIPanelManager.Instance.ShowPanel("Option_Control", false);
                UIPanelManager.Instance.ShowPanel("Option", true);
                break;
            case ButtonType.P_Option_Game:
                UIPanelManager.Instance.ShowPanel("Option_Game", false);
                UIPanelManager.Instance.ShowPanel("Option", true);
                break;
            case ButtonType.P_Save_No:
                UIPanelManager.Instance.ClosePanel("Save_P");
                break;
            case ButtonType.P_Quit_No:
                UIPanelManager.Instance.ClosePanel("Quit_P");
                break;
            //case ButtonType.Pause_Continue:
            //    UIPanelManager.Instance.CloseAllPanels();
            //    break;
            case ButtonType.P_Slot_Quit:
            case ButtonType.P_Option_Quit:
            case ButtonType.P_Save_Yes:
                UIPanelManager.Instance.ShowPanel("Pause", false);
                break;
            default:
                Debug.LogWarning($"[ButtonManager] ó������ ���� ��ư Ÿ��: {type}");
                break;
        }
    }
}
