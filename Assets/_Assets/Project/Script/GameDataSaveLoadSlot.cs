using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace GameSave
{
    public class GameDataSaveLoadSlot : MonoBehaviour
    {
        [field: Header("이 슬롯의 고유한 번호")]
        [field: SerializeField] public int slotId { private set; get; } = -1;

        private static event Action<int> _saveLoadEvent;
        public static GameDataSaveLoadSlot Instance { get; private set; }

        [Space(3)]
        [Header("슬롯의 버튼 (저장, 불러오기, 삭제)")]
        [SerializeField] private Button save_btn;
        [SerializeField] private Button load_btn;
        [SerializeField] private Button remove_btn;

        [SerializeField] private Transform playerPosition;

        private void Awake()
        {
            UpdateSlot();
        }

        private void Start()
        {
            // 버튼 이벤트 등록은 한 번만 실행
            save_btn.onClick.AddListener(CheckPointSave);
            load_btn.onClick.AddListener(CheckPointLoad);
            remove_btn.onClick.AddListener(CheckPointRemove);
        }

        public void UpdateSlot()
        {
            //슬롯 번호에 해당하는 세이브 파일이 있으면 버튼 활성화
            bool hasSave = PlayerPrefs.HasKey($"PlayerPosX_{slotId}");

            load_btn.interactable = hasSave;
            remove_btn.interactable = hasSave;
        }

        public void CheckPointSave()
        {
            Vector3 pos = playerPosition.position;

            PlayerPrefs.SetFloat($"PlayerPosX_{slotId}", pos.x);
            PlayerPrefs.SetFloat($"PlayerPosY_{slotId}", pos.y);
            PlayerPrefs.SetFloat($"PlayerPosZ_{slotId}", pos.z);

            // 현재 선택된 코스튬 & 스킨 ID 저장
            PlayerPrefs.SetInt($"SelectedCostume_{slotId}", PlayerPrefs.GetInt("SelectedCostume", 0));
            PlayerPrefs.SetInt($"SelectedSkined_{slotId}", PlayerPrefs.GetInt("SelectedSkined", 0));

            PlayerPrefs.SetInt($"IsSaved_{slotId}", 1); // 저장 여부 표시
            PlayerPrefs.Save(); //디스크에 강제 저장

            // 코스튬 UI 저장
            for (int i = 1; i <= 3; i++)
            {
                int hasCostume = PlayerPrefs.GetInt($"HasCostume_{i}", 0);
                PlayerPrefs.SetInt($"Slot{slotId}_HasCostume_{i}", hasCostume);
            }

            Debug.Log($"슬롯 {slotId}: 현재 위치 저장 완료 - {pos}");
            UpdateSlot(); // 버튼 활성화 갱신
        }

        public void CheckPointLoad()
        {
            // 저장된 위치 불러오기
            if (PlayerPrefs.HasKey($"PlayerPosX_{slotId}"))
            {
                float x = PlayerPrefs.GetFloat($"PlayerPosX_{slotId}");
                float y = PlayerPrefs.GetFloat($"PlayerPosY_{slotId}");
                float z = PlayerPrefs.GetFloat($"PlayerPosZ_{slotId}");

                playerPosition.position = new Vector3(x, y, z);
                Debug.Log($"슬롯 {slotId}: 위치 불러오기 완료 - {playerPosition.position}");

                // 저장된 코스튬 불러와 적용
                int savedCostumeId = PlayerPrefs.GetInt($"SelectedCostume_{slotId}", 0);
                int savedSkinId = PlayerPrefs.GetInt($"SelectedSkined_{slotId}", 0);

                // 코스튬 UI 갱신
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

                // CostumeItem 제거 로직 추가
                RemoveCollectedCostumeItems();

                Debug.Log($"슬롯 {slotId}: 코스튬 {savedCostumeId} / 스킨 {savedSkinId} 적용 완료!");
            }
            else
            {
                Debug.Log($"슬롯 {slotId}: 저장된 위치 없음. 기본 위치 사용");
            }
        }

        public void CheckPointRemove()
        {
            PlayerPrefs.DeleteKey($"PlayerPosX_{slotId}");
            PlayerPrefs.DeleteKey($"PlayerPosY_{slotId}");
            PlayerPrefs.DeleteKey($"PlayerPosZ_{slotId}");

            // 스튬 & 스킨 정보 삭제
            PlayerPrefs.DeleteKey($"SelectedCostume_{slotId}");
            PlayerPrefs.DeleteKey($"SelectedSkined_{slotId}");

            PlayerPrefs.DeleteKey($"IsSaved_{slotId}");

            PlayerPrefs.DeleteKey($"HasCostume_{slotId}");

            Debug.Log($"슬롯 {slotId}: 저장된 위치 데이터 삭제 완료");
            UpdateSlot();
        }

        private void RemoveCollectedCostumeItems()
        {
            for (int i = 1; i <= 3; i++)
            {
                if (PlayerPrefs.GetInt($"HasCostume_{i}", 0) == 1)
                {
                    // 씬에 있는 모든 CostumeItem을 검색
                    var items = FindObjectsOfType<CostumeItem>();
                    foreach (var item in items)
                    {
                        if (item.costumeId == i)
                        {
                            Destroy(item.gameObject);
                            Debug.Log($"CostumeItem {i} 이미 보유 중 → 제거");
                        }
                    }
                }
            }
        }
    }
}

