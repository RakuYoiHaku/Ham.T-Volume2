using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCostume;
    public GameObject[] unlockCostume;
    public GameObject uiNotice;     //�������� �˸�

    WaitForSecondsRealtime wait;

    enum Achieve { UnlockSunflowerHat, UnlockBearHat, UnlockChefHat }
    Achieve[] achieves;

    private void Awake()
    {
        //�����Ͱ� ���ٸ� �ʱ�ȭ
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));

        // yield return new WaitForSecond(5) ��ſ� ����ȭ�� ���� ����ο��� �ʱ�ȭ ���ֱ�
        wait = new WaitForSecondsRealtime(5f);
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    //���� ������ �ʱ�ȭ �Լ�
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achieve achieve in achieves)
        {
            //�ʱ�ȭ ����
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

        //���� �޼� ����
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

        //�ش� ������ ó�� �޼� �ߴٴ� ����
        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);

            //���� Active�� �ٲ㼭 �˸�â �߰� ����
            //ChildCount�� �ڽ��� �� ���
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




