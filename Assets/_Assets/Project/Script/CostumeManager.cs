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
                // ���� �߰��� �ڽ�Ƭ �Ŵ����� ã�´�.
                _instance = FindObjectOfType<CostumeManager>();
            }
                
            return _instance;
        }
    }

    private List<int> ownedCostumes = new List<int>(); // ȹ���� �ڽ�Ƭ ID ����
    public IReadOnlyList<int> OwnedCostumes => ownedCostumes; // �б� ���� ����Ʈ

    private int latestCostumeId  = -1; //�ʱⰪ�� -1 (�⺻����)


    private void Awake()
    {
        // �̱����� �����ϱ� ���� �ߺ��Ǵ� ���ӿ�����Ʈ�� �ı�
        if (_instance != null)
            Destroy(gameObject);
    }

    //private void Start()
    //{
    //    UpdateCostume();
    //}

    public void AddCostume(int costumeId)
    {
        if (!ownedCostumes.Contains(costumeId)) //�ߺ� ����
        {
            ownedCostumes.Add(costumeId); //�ڽ�Ƭ �߰�
        }

        // UI�� ã�Ƽ� �� �ڽ�Ƭ ��ư �߰�
        CostumeUI uiManager = FindObjectOfType<CostumeUI>();
        if (uiManager != null)
        {
            //uiManager.ShowNewCostumeUI(costumeId);
        }
    }
    //public void EquipCostume(int costumeId)
    //{
    //    if(ownedCostumes.Contains(costumeId))   //ȹ���� �ڽ�Ƭ�� ���� ����
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
    //    // ��� �ڽ�Ƭ ��Ȱ��ȭ
    //    foreach (GameObject costume in costumes)
    //    {
    //        costume.SetActive(false);
    //    }

    //    // ������ �ڽ�Ƭ �� ���������� �߰��� �ڽ�Ƭ�� Ȱ��ȭ (�⺻ ���� ����)
    //    if (ownedCostumes.Count > 0)
    //    {
    //        int lastCostumeId = ownedCostumes[ownedCostumes.Count - 1]; // ���������� �߰��� �ڽ�Ƭ ID
    //        costumes[lastCostumeId].SetActive(true);
    //    }
    //}
}
