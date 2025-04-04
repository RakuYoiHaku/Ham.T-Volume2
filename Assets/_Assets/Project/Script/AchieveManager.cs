using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCostume;
    public GameObject[] unlockCostume;
    public GameObject uiNotice;     //도전과제 알림

    WaitForSecondsRealtime wait;

    enum Achieve { UnlockSunflowerHat, UnlockBearHat, UnlockChefHat }
    Achieve[] achieves;

    private void Awake()
    {
        //데이터가 없다면 초기화
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));

        // yield return new WaitForSecond(5) 대신에 최적화를 위해 선언부에서 초기화 해주기
        wait = new WaitForSecondsRealtime(5f);
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    //저장 데이터 초기화 함수
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achieve achieve in achieves)
        {
            //초기화 설정
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCostume();
    }

    void UnlockCostume()
    {
        for (int index = 0; index < lockCostume.Length; index++)
        {
            string achiveName = achieves[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCostume[index].SetActive(!isUnlock);
            unlockCostume[index].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }

    void CheckAchieve(Achieve achieve)
    {
        bool isAchieve = false;

        //업적 달성 조건
        switch (achieve)
        {
            case Achieve.UnlockSunflowerHat:
                //isAchieve = GameManager.Instance.
                break;
            case Achieve.UnlockBearHat:
                //isAchieve = GameManager.instance.game
                break;
            case Achieve.UnlockChefHat:

                break;

                //StartCoroutine(NoticeRoutine());
        }

        //해당 업적이 처음 달성 했다는 조건
        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);

            //각각 Active로 바꿔서 알림창 뜨게 해줌
            //ChildCount로 자식의 수 계산
            for (int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achieve;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }

        IEnumerator NoticeRoutine()
        {
            uiNotice.SetActive(true);
            yield return wait;
            uiNotice.SetActive(false);
        }
    }
}




