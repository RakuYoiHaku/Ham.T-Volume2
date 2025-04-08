using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDataSlotUI : MonoBehaviour
{
    [SerializeField] public int slotId;

    [Header("UI ��ҵ�")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI costumeText;
    [SerializeField] private TextMeshProUGUI skinText;

    private void Awake()
    {
        UpdateSlotUI();    
    }

    public void UpdateSlotUI()
    {
        // ����� �ð� �ҷ�����
        string saveTime = PlayerPrefs.GetString($"SaveTime_{slotId}", "No Save");
        timeText.text = saveTime;

        // �ڽ�Ƭ ���� ���
        int costumeCount = 0;
        int totalCostumes = 3; // �� �ڽ�Ƭ ��
        for (int i = 1; i <= totalCostumes; i++)
        {
            if (PlayerPrefs.GetInt($"Slot{slotId}_HasCostume_{i}", 0) == 1)
                costumeCount++;
        }
        costumeText.text = $"{costumeCount}/{totalCostumes}";

        // ��Ų ���� ���
        int skinCount = 0;
        int totalSkins = 3; // �� ��Ų �� (����)
        for (int i = 1; i <= totalSkins; i++)
        {
            if (PlayerPrefs.GetInt($"Slot{slotId}_HasSkin_{i}", 0) == 1)
                skinCount++;
        }
        skinText.text = $"{skinCount}/{totalSkins}";
    }

}

