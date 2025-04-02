using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템에 사용
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

    //플레이어가 아이템 범위 안에 들어왔을 때 호출
    public void SetInteractable(InteractableItem item)
    {
        ItemInteractor.Instance.currentInteractable = item;

        UIManager.Instance.InteractUI(true);
    }

    //플레이어가 아이템 범위에서 벗어났을 때 호출
    public void ClearInteractable()
    {
        if (ItemInteractor.Instance.heldItem != null)
            return;

        ItemInteractor.Instance.currentInteractable = null;

        UIManager.Instance.InteractUI(false);
    }
}
