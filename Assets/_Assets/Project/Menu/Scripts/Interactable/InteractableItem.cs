using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ۿ� ���
public class InteractableItem : MonoBehaviour
{
    public bool canEnterZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ClearInteractable();
        }
    }

    //�÷��̾ ������ ���� �ȿ� ������ �� ȣ��
    public void SetInteractable(InteractableItem item)
    {
        ItemInteractor.Instance.currentInteractable = item;

        UIManager.Instance.InteractUI(true);
    }

    //�÷��̾ ������ �������� ����� �� ȣ��
    public void ClearInteractable()
    {
        if (ItemInteractor.Instance.heldItem != null)
            return;

        ItemInteractor.Instance.currentInteractable = null;

        UIManager.Instance.InteractUI(false);
    }
}
