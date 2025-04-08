using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace GameSave
{
    public class GameDataSaveLoadSlot : MonoBehaviour
    {
        [field: Header("�� ������ ������ ��ȣ")]
        [field: SerializeField] public int slotId { private set; get; } = -1;

        private static event Action<int> _saveLoadEvent;
        public static GameDataSaveLoadSlot Instance { get; private set; }

        [Space(3)]
        [Header("������ ��ư (����, �ҷ�����, ����)")]
        [SerializeField] private Button save_btn;
        [SerializeField] private Button load_btn;
        [SerializeField] private Button remove_btn;

        [SerializeField] private Transform playerPosition;
        [SerializeField] private TMP_Text saveTimeText;

        private void Awake()
        {
            UpdateSlot();
        }

        private void Start()
        {
            // ��ư �̺�Ʈ ����� �� ���� ����
            save_btn.onClick.AddListener(CheckPointSave);
            load_btn.onClick.AddListener(CheckPointLoad);
            remove_btn.onClick.AddListener(CheckPointRemove);

            // ���� �� ���� �� �ڵ� �ε� ó��
            int selectedSlot = PlayerPrefs.GetInt("SelectedSlot", -1);
            if (selectedSlot == slotId && PlayerPrefs.HasKey($"PlayerPosX_{slotId}"))
            {
                CheckPointLoad();
            }
        }

        public void UpdateSlot()
        {
            bool hasSave = PlayerPrefs.HasKey($"PlayerPosX_{slotId}");
            load_btn.interactable = hasSave;
            remove_btn.interactable = hasSave;

            // UI �ʿ��� PlayerPrefs�� ���� Ȯ���Ͽ� ǥ���ϵ��� ����
        }

        public void CheckPointSave()
        {
            // ���� ��¥�� �ð� ����
            string saveTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            PlayerPrefs.SetString($"SaveTime_{slotId}", saveTime);

            Vector3 pos = playerPosition.position;

            PlayerPrefs.SetFloat($"PlayerPosX_{slotId}", pos.x);
            PlayerPrefs.SetFloat($"PlayerPosY_{slotId}", pos.y);
            PlayerPrefs.SetFloat($"PlayerPosZ_{slotId}", pos.z);

            // ���� ���õ� �ڽ�Ƭ & ��Ų ID ����
            PlayerPrefs.SetInt($"SelectedCostume_{slotId}", PlayerPrefs.GetInt("SelectedCostume", 0));
            PlayerPrefs.SetInt($"SelectedSkined_{slotId}", PlayerPrefs.GetInt("SelectedSkined", 0));

            PlayerPrefs.SetInt($"IsSaved_{slotId}", 1); // ���� ���� ǥ��

            // �ڽ�Ƭ/��Ų UI ����
            for (int i = 1; i <= 3; i++)
            {
                int hasCostume = PlayerPrefs.GetInt($"HasCostume_{i}", 0);
                PlayerPrefs.SetInt($"Slot{slotId}_HasCostume_{i}", hasCostume);

                int hasSkin = PlayerPrefs.GetInt($"HasSkin_{i}", 0);
                PlayerPrefs.SetInt($"Slot{slotId}_HasSkin_{i}", hasSkin);
            }

            Debug.Log($"���� {slotId}: ���� ��ġ ���� �Ϸ� - {pos}");
            PlayerPrefs.Save(); //��ũ�� ���� ����
            UpdateSlot(); // ��ư Ȱ��ȭ ����
        }

        public void CheckPointLoad()
        {
            // ����� ��ġ �ҷ�����
            if (PlayerPrefs.HasKey($"PlayerPosX_{slotId}"))
            {
                float x = PlayerPrefs.GetFloat($"PlayerPosX_{slotId}");
                float y = PlayerPrefs.GetFloat($"PlayerPosY_{slotId}");
                float z = PlayerPrefs.GetFloat($"PlayerPosZ_{slotId}");

                playerPosition.position = new Vector3(x, y, z);
                Debug.Log($"���� {slotId}: ��ġ �ҷ����� �Ϸ� - {playerPosition.position}");

                // ����� �ڽ�Ƭ �ҷ��� ����
                int savedCostumeId = PlayerPrefs.GetInt($"SelectedCostume_{slotId}", 0);
                int savedSkinId = PlayerPrefs.GetInt($"SelectedSkined_{slotId}", 0);

                // ����� �ð� ǥ��
                string savedTime = PlayerPrefs.GetString($"SaveTime_{slotId}");
                Debug.Log($"���� {slotId}�� ���� �ð�: {savedTime}");

                // �ڽ�Ƭ UI ����
                for (int i = 1; i <= 3; i++)
                {
                    int hasCostume = PlayerPrefs.GetInt($"Slot{slotId}_HasCostume_{i}", 0);
                    PlayerPrefs.SetInt($"HasCostume_{i}", hasCostume);

                    if (hasCostume == 1)
                    {
                        CostumeUI.Instance.UpdateBtn(i);
                    }
                }

                CostumeUI.Instance.SelectCostume(savedCostumeId);
                CostumeUI.Instance.SelectSkin(savedSkinId);

                // CostumeItem ���� ���� �߰�
                RemoveCollectedCostumeItems();

                Debug.Log($"���� {slotId}: �ڽ�Ƭ {savedCostumeId} / ��Ų {savedSkinId} ���� �Ϸ�!");
            }
            else
            {
                Debug.Log($"���� {slotId}: ����� ��ġ ����. �⺻ ��ġ ���");
            }
        }

        public void CheckPointRemove()
        {
            PlayerPrefs.DeleteKey($"PlayerPosX_{slotId}");
            PlayerPrefs.DeleteKey($"PlayerPosY_{slotId}");
            PlayerPrefs.DeleteKey($"PlayerPosZ_{slotId}");

            // ��Ƭ & ��Ų ���� ����
            PlayerPrefs.DeleteKey($"SelectedCostume_{slotId}");
            PlayerPrefs.DeleteKey($"SelectedSkined_{slotId}");

            PlayerPrefs.DeleteKey($"IsSaved_{slotId}");

            PlayerPrefs.DeleteKey($"HasCostume_{slotId}");
            PlayerPrefs.DeleteKey($"SaveTime_{slotId}");

            Debug.Log($"���� {slotId}: ����� ��ġ ������ ���� �Ϸ�");
            UpdateSlot();
        }

        private void RemoveCollectedCostumeItems()
        {
            for (int i = 1; i <= 3; i++)
            {
                if (PlayerPrefs.GetInt($"HasCostume_{i}", 0) == 1)
                {
                    // ���� �ִ� ��� CostumeItem�� �˻�
                    var items = FindObjectsOfType<CostumeItem>();
                    foreach (var item in items)
                    {
                        if (item.costumeId == i)
                        {
                            Destroy(item.gameObject);
                            Debug.Log($"CostumeItem {i} �̹� ���� �� �� ����");
                        }
                    }
                }
            }
        }
    }
}

