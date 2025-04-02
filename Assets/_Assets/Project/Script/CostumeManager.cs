using System.Collections.Generic;
using UnityEngine;

public class CostumeManager : MonoBehaviour
{
    private static CostumeManager _instance;

    public static CostumeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 씬에 추가된 코스튬 매니저를 찾는다.
                _instance = FindObjectOfType<CostumeManager>();
            }
                
            return _instance;
        }
    }

    private List<int> ownedCostumes = new List<int>(); // 획득한 코스튬 ID 저장
    public IReadOnlyList<int> OwnedCostumes => ownedCostumes; // 읽기 전용 리스트

    private int latestCostumeId  = -1; //초기값은 -1 (기본상태)


    private void Awake()
    {
        // 싱글톤을 구현하기 위해 중복되는 게임오브젝트는 파괴
        if (_instance != null)
            Destroy(gameObject);
    }

    //private void Start()
    //{
    //    UpdateCostume();
    //}

    public void AddCostume(int costumeId)
    {
        if (!ownedCostumes.Contains(costumeId)) //중복 방지
        {
            ownedCostumes.Add(costumeId); //코스튬 추가
        }

        // UI를 찾아서 새 코스튬 버튼 추가
        CostumeUI uiManager = FindObjectOfType<CostumeUI>();
        if (uiManager != null)
        {
            //uiManager.ShowNewCostumeUI(costumeId);
        }
    }
    //public void EquipCostume(int costumeId)
    //{
    //    if(ownedCostumes.Contains(costumeId))   //획득한 코스튬만 장착 가능
    //    {
    //         foreach (GameObject costume in costumes)
    //        {
    //            costume.SetActive(false);
    //        }
    //        costumes[costumeId].SetActive(true);
    //    }
    //}
    //private void UpdateCostume()
    //{
    //    // 모든 코스튬 비활성화
    //    foreach (GameObject costume in costumes)
    //    {
    //        costume.SetActive(false);
    //    }

    //    // 소유한 코스튬 중 마지막으로 추가된 코스튬을 활성화 (기본 장착 개념)
    //    if (ownedCostumes.Count > 0)
    //    {
    //        int lastCostumeId = ownedCostumes[ownedCostumes.Count - 1]; // 마지막으로 추가된 코스튬 ID
    //        costumes[lastCostumeId].SetActive(true);
    //    }
    //}
}
