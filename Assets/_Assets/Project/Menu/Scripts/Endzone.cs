using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endzone : MonoBehaviour
{
    public int targetScore = 10; //게임 종료 조건 (목표 점수)
    public GameObject warningText; // UI 텍스트 연결

    private void Start()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false); // 시작할 때 숨기기
        }
    }

    //public bool CanEndGame()
    //{
    //    //GameManager의 점수를 가져와 비교
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
        Debug.Log("게임 클리어123!");
        SceneManager.LoadScene("EndScene");
    }

    public void GameEnd2()
    {
        Debug.Log("게임 클리어!");
        SceneManager.LoadScene("EndScene2");
    }

    private void ShowWarning()
    {
        if(warningText != null)
        {
            warningText.gameObject.SetActive(true);
            StartCoroutine(HideWarning()); //몇 초 후 사라지게 만들기
        }
    }

    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(2f); // 2초 후 사라짐
        warningText.gameObject.SetActive(false);
    }
}
