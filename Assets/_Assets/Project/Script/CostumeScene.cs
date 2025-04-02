using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CostumeUI.Open();
    }

    private void OnDestroy()
    {
        CostumeUI.Close();
    }
}
