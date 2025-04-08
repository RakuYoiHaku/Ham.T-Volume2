using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDataSlotUI : MonoBehaviour
{
    [SerializeField] public int slotId;

    [Header("UI 요소들")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI costumeText;
    [SerializeField] private TextMeshProUGUI skinText;

    private void Awake()
    {
        UpdateSlotUI();    
    }

    public void UpdateSlotUI()
    {
        // 저장된 시간 불러오기
        string saveTime = PlayerPrefs.GetString($"SaveTime_{slotId}", "No Save");
        timeText.text = saveTime;

        // 코스튬 개수 계산
        int costumeCount = 0;
        int totalCostumes = 3; // 총 코스튬 수
        for (int i = 1; i <= totalCostumes; i++)
        {
            if (PlayerPrefs.GetInt($"Slot{slotId}_HasCostume_{i}", 0) == 1)
                costumeCount++;
        }
        costumeText.text = $"{costumeCount}/{totalCostumes}";

        // 스킨 개수 계산
        int skinCount = 0;
        int totalSkins = 3; // 총 스킨 수 (예시)
        for (int i = 1; i <= totalSkins; i++)
        {
            if (PlayerPrefs.GetInt($"Slot{slotId}_HasSkin_{i}", 0) == 1)
                skinCount++;
        }
        skinText.text = $"{skinCount}/{totalSkins}";
    }

}

