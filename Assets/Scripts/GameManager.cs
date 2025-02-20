using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private AudioSource systemAudioPlayer;
    public AudioClip eatingSound;
    public AudioClip jumpSound;
    public AudioClip pickItemSound;
    public AudioClip dropItemSound;

    // 싱글톤 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; //싱글톤이 할당될 static 변수

    private int score = 0;

    private InteractableItem currentInteractable; // 현재 상호작용 가능한 오브젝트
    public InteractableItem heldItem; // 현재 플레이어가 들고 있는 아이템
    public Transform chinTransform; // 아이템을 붙일 위치(플레이어의 손)
    public GameObject interactText; // "E 키를 눌러 집기" UI

    private void Awake()
    {
        systemAudioPlayer = GetComponent<AudioSource>();

        if(instance != this)
        {
            Destroy(this.gameObject);
        } 
    }

    private void Start()
    {
        if(interactText != null)
        {
            interactText.gameObject.SetActive(false); // 시작할 때 숨기기
        }
    }

    // 점프 소리를 재생하는 함수
    public void PlayJumpSound()
    {
        // 소리가 이미 재생 중인지 확인
        if (systemAudioPlayer != null && jumpSound != null)
        {
            systemAudioPlayer.PlayOneShot(jumpSound); // 점프 소리 재생
        }
    }

    private void Update()
    {
        if(currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            ToggleItem();
        }
    }
    #region CollectItem() 아이템을 수집하는 코드
    /*
    public void CollectItem()
    {
        if (currentInteractable != null)
        {
            Debug.Log("아이템을 획득했습니다: " + currentInteractable.name);
            Destroy(currentInteractable); // 아이템 제거
            currentInteractable = null;

            if (interactText != null)
            {
                interactText.gameObject.SetActive(false); // UI 숨기기
            }
        }
    }*/
    #endregion

    public void ToggleItem()
    {
        if (heldItem == null)
        {
            systemAudioPlayer.PlayOneShot(pickItemSound);
            PickupItem(); // 아이템 집기
        }
        else
        {
            systemAudioPlayer.PlayOneShot(dropItemSound);
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

    //플레이어가 아이템 범위 안에 들어왔을 때 호출
    public void SetInteractable(InteractableItem item)
    {
        currentInteractable = item;

        if (interactText != null)
        {
            interactText.gameObject.SetActive(true);
        }
    }

    //플레이어가 아이템 범위에서 벗어났을 때 호출
    public void ClearInteractable()
    {
        if (heldItem != null)
            return;

        currentInteractable = null;

        if(interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

    //점수를 증가시킴
    public void CollectSeed(int newScore)
    {
        score += newScore;
        UIManager.instance.UpdateScoreText(score);

        //먹는 소리 재생
        if (systemAudioPlayer != null && eatingSound != null)
        {
            systemAudioPlayer.PlayOneShot(eatingSound);
        }
    }

    //현재 점수를 반환함
    public int GetScore()
    {
        return score; // 점수를 반환
    }
}
