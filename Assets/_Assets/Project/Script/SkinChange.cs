using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer bodyRenderer;

    private static event Action<int> _skinChangedEvent;

    public static void ActiveSkined(int skinId)
    {
        _skinChangedEvent?.Invoke(skinId);
    }

    public Material skin0;
    public Material skin1;

    private void Awake()
    {
        _skinChangedEvent += OnSkinChanged;
    }

    private void OnDestroy()
    {
        _skinChangedEvent -= OnSkinChanged;
    }

    public void Start()
    {
        ResetSkin();  // 기본 스킨으로 초기화

        if (PlayerPrefs.HasKey("SelectedSkined"))
        {
            int savedSkinId = PlayerPrefs.GetInt("SelectedSkined");
            ActiveSkined(savedSkinId);
        }
        else
        {
            // 기본값으로 초기화
            PlayerPrefs.SetInt("SelectedSkined", 0);
            PlayerPrefs.Save();
            ActiveSkined(0);
        }
    }
    private void ResetSkin()
    {
        OnSkinChanged(0);
    }

    public void OnSkinChanged(int skinId)
    {
        //if (bodyRenderer == null)
        //{
        //    print($"OBJ : {gameObject.name} SCENE : {gameObject.scene.name}");
        //    return;
        //}

        switch (skinId)
        {
            case 0:
                bodyRenderer.material = skin0;
                break;
            case 1:
                bodyRenderer.material = skin1;
                break;
        }
    }
}
