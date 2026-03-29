using UnityEngine;
using VladimirFps.Utils;

public class StoreUIHandler : MonoBehaviour
{
    [SerializeField] private SkinSelector skinSelector;
    [SerializeField] private StoreController controller;

    public void OnClick_NextSkin()
    {
        skinSelector.ToNext();
    }

    public void OnClick_PrevSkin()
    {
        skinSelector.ToPrev();
    }

    public void OnClick_ToMainMenu()
    {
        MySceneManager.LoadScene(0);
    }

    public void OnClick_PurchaseOrSelect()
    {
        int id = skinSelector.Configs[skinSelector.SelectedIndex].id;
        int cost = skinSelector.Configs[skinSelector.SelectedIndex].cost;

        controller.PurchaseOrSelect(id, cost);
    }
}
