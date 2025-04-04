using GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameDataSaveLoadSlot.Instance.CheckPointSave();
        }
    }
}
