using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AudioManager;

// 플레이어에게 사용
public class ItemInteractor : MonoBehaviour
{
    public static ItemInteractor Instance {  get; private set; }

    public InteractableItem currentInteractable; // 현재 상호작용 가능한 오브젝트
    public InteractableItem heldItem; // 현재 플레이어가 들고 있는 아이템
    public Transform chinTransform; // 아이템을 붙일 위치(플레이어의 손)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    private void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            ToggleItem();
        }
    }

    public void ToggleItem()
    {
        if (heldItem == null)
        {
            AudioManager.Instance.PlaySound(SoundType.Pick);
            PickupItem(); // 아이템 집기
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundType.Drop);
            DropItem(); // 아이템 내려놓기
        }
    }

    // 아이템을 플레이어 손에 고정
    public void PickupItem()
    {
        if (currentInteractable != null)
        {
            heldItem = currentInteractable;

            // Rigidbody 비활성화 (물리 적용 방지)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            heldItem.transform.SetParent(chinTransform); // 부모를 손으로 설정
            heldItem.transform.localPosition = Vector3.zero; // 손의 위치로 이동
            heldItem.transform.localRotation = Quaternion.identity; // 손의 회전에 맞춤

            Debug.Log("아이템을 집었습니다: " + heldItem.name);
        }
    }

    // 아이템을 내려놓음
    public void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null); // 부모 관계 해제

            // Rigidbody 활성화 (자연스럽게 떨어지도록)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                // 강제로 Rigidbody에 물리적 힘을 가해 이동하게 함
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(Vector3.down * 2, ForceMode.Impulse);
            }

            Debug.Log("아이템을 내려놓았습니다: " + heldItem.name);
            heldItem = null; // 들고 있는 아이템 제거
        }
    }
}
