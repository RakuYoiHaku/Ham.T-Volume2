using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnStart : MonoBehaviour
{
    void Awake()
    {
        ResetAllCostumeDataOnStart();
    }

    private void ResetAllCostumeDataOnStart()
    {
        PlayerPrefs.DeleteKey("SelectedCostume");
        PlayerPrefs.DeleteKey("SelectedSkined");

        // 모든 코스튬 보유 정보 초기화 (예: 1~3번 코스튬)
        for (int i = 1; i <= 3; i++)
        {
            PlayerPrefs.SetInt($"HasCostume_{i}", 0);
        }

        PlayerPrefs.Save();
        Debug.Log("게임 시작 시 모든 HasCostume 정보 초기화 완료!");
    }
}
