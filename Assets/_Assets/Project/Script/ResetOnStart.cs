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

        // ��� �ڽ�Ƭ ���� ���� �ʱ�ȭ (��: 1~3�� �ڽ�Ƭ)
        for (int i = 1; i <= 3; i++)
        {
            PlayerPrefs.SetInt($"HasCostume_{i}", 0);
        }

        PlayerPrefs.Save();
        Debug.Log("���� ���� �� ��� HasCostume ���� �ʱ�ȭ �Ϸ�!");
    }
}
