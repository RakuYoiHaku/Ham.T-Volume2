using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AudioManager;

// �÷��̾�� ���
public class ItemInteractor : MonoBehaviour
{
    public static ItemInteractor Instance {  get; private set; }

    public InteractableItem currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ
    public InteractableItem heldItem; // ���� �÷��̾ ��� �ִ� ������
    public Transform chinTransform; // �������� ���� ��ġ(�÷��̾��� ��)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �ߺ� ���� ����
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
            PickupItem(); // ������ ����
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundType.Drop);
            DropItem(); // ������ ��������
        }
    }

    // �������� �÷��̾� �տ� ����
    public void PickupItem()
    {
        if (currentInteractable != null)
        {
            heldItem = currentInteractable;

            // Rigidbody ��Ȱ��ȭ (���� ���� ����)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            heldItem.transform.SetParent(chinTransform); // �θ� ������ ����
            heldItem.transform.localPosition = Vector3.zero; // ���� ��ġ�� �̵�
            heldItem.transform.localRotation = Quaternion.identity; // ���� ȸ���� ����

            Debug.Log("�������� �������ϴ�: " + heldItem.name);
        }
    }

    // �������� ��������
    public void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null); // �θ� ���� ����

            // Rigidbody Ȱ��ȭ (�ڿ������� ����������)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                // ������ Rigidbody�� ������ ���� ���� �̵��ϰ� ��
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(Vector3.down * 2, ForceMode.Impulse);
            }

            Debug.Log("�������� �������ҽ��ϴ�: " + heldItem.name);
            heldItem = null; // ��� �ִ� ������ ����
        }
    }
}
