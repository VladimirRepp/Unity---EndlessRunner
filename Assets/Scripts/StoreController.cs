using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] private CoinsViewer coinsViewer;

    private int _currentCountCoins = 0;
    private List<int> _purchasedIdSkins;
    private int _selectedSkinId;

    public Action<int> OnSelectedSkin;

    private void Awake()
    {
        _purchasedIdSkins = new();

        _currentCountCoins = DataController.Instance.LoadCoins();
        coinsViewer.Display(_currentCountCoins);

        _selectedSkinId = DataController.Instance.GetSelectedSkin();
        LoadPurchasedSkins();
    }

    private void LoadPurchasedSkins()
    {
        string idsStrLine = DataController.Instance.LoadPurchasedSkins();

        if (string.IsNullOrEmpty(idsStrLine))
        {
            _purchasedIdSkins.Add(1);
            return;
        }

        string[] idsStrs = idsStrLine.Split(" ");
        int id = -1;

        foreach (string idStr in idsStrs) { 
            if(int.TryParse(idStr, out id))
            {
                _purchasedIdSkins.Add(id);
            }
        }
    }

    private void SavePurchasedSkins()
    {
        string idsStr = "";
        foreach(int id in _purchasedIdSkins)
        {
            idsStr += id + " ";
        }

        DataController.Instance.SavePurchasedSkins(idsStr);
    }

    private void Select(int id)
    {
        _selectedSkinId = id;
        DataController.Instance.SetSelectedSkin(id);
        OnSelectedSkin?.Invoke(id);
    }

    public ESkinState CheckSkinState(int id)
    {
        foreach(int id_ in _purchasedIdSkins)
        {
            if (id == id_)
            {
                if(id == _selectedSkinId)
                    return ESkinState.Selected;

                return ESkinState.Purchased;
            }
        }

        return ESkinState.ForSale;
    }

    public bool PurchaseOrSelect(int id, int cost)
    {
        foreach(int id_ in _purchasedIdSkins)
        {
            if(id == id_)
            {
                Select(id);
                return true;
            }
        }

        if(_currentCountCoins < cost)
            return false;

        _currentCountCoins -= cost;
        _purchasedIdSkins.Add(id);
        Select(id);

        DataController.Instance.SaveCoins(_currentCountCoins);
        SavePurchasedSkins();

        coinsViewer.Display(_currentCountCoins);
        return true;
    }
}

public enum ESkinState {
    ForSale,
    Purchased,
    Selected
}

