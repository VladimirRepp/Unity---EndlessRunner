using System;
using TMPro;
using UnityEngine;

public class SkinViewer : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text cost;

    [Header("SkinDecorator Settings")]
    [SerializeField] private SkinSelector skinSelector;

    private void OnEnable()
    {
        skinSelector.OnChangeHeaderSkin += HandleChangeHeaderSkin;
        skinSelector.StoreController.OnSelectedSkin += HandleSelectedSkin;
    }

    private void OnDisable()
    {
        skinSelector.OnChangeHeaderSkin -= HandleChangeHeaderSkin;
        skinSelector.StoreController.OnSelectedSkin -= HandleSelectedSkin;
    }

    private void HandleChangeHeaderSkin(int id, string header, float cost)
    {
        this.header.text = header;

        ESkinState skinState = skinSelector.StoreController.CheckSkinState(id);

        if (skinState == ESkinState.ForSale)
            this.cost.text = cost.ToString();
        else if (skinState == ESkinState.Selected)
            this.cost.text = "selected";
        else if (skinState == ESkinState.Purchased)
            this.cost.text = "can select";
    }

    private void HandleSelectedSkin(int id)
    {
        cost.text = "selected";
    }
}
