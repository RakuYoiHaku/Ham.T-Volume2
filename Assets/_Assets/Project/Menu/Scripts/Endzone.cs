using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endzone : MonoBehaviour
{
    public int targetScore = 10; //���� ���� ���� (��ǥ ����)
    public GameObject warningText; // UI �ؽ�Ʈ ����

    private void Start()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false); // ������ �� �����
        }
    }

    //public bool CanEndGame()
    //{
    //    //GameManager�� ������ ������ ��
    //    return GameManager.instance != null && GameManager.instance.GetScore() >= targetScore;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        var heldItem = GameManager.instance.heldItem;

    //        if (heldItem != null && heldItem.canEnterZone)
    //        {
    //            if (CanEndGame())
    //            {
    //                GameEnd2();
    //            }
    //            else
    //            {
    //                ShowWarning();
    //            }
    //        }
    //        else
    //        {
    //            if (CanEndGame())
    //            {
    //                GameEnd();
    //            }
    //            else
    //            {
    //                ShowWarning();
    //            }
    //        }
    //    }

    //    //else if (other.CompareTag("TeddyBear") && other.CompareTag("Player"))
    //    //else if (other.CompareTag("Player"))
    //    //{
            
    //    //}
    //}

    public void GameEnd()
    {
        Debug.Log("���� Ŭ����123!");
        SceneManager.LoadScene("EndScene");
    }

    public void GameEnd2()
    {
        Debug.Log("���� Ŭ����!");
        SceneManager.LoadScene("EndScene2");
    }

    private void ShowWarning()
    {
        if(warningText != null)
        {
            warningText.gameObject.SetActive(true);
            StartCoroutine(HideWarning()); //�� �� �� ������� �����
        }
    }

    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(2f); // 2�� �� �����
        warningText.gameObject.SetActive(false);
    }
}
