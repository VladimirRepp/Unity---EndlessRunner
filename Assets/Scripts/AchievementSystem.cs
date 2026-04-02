using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private AchievementConfig[] configs;

    private List<int> _idsCollected;

    public AchievementConfig[] Configs => configs;

    private void Awake()
    {
        _idsCollected = new List<int>();
        LoadCollected();
    }

    private void LoadCollected()
    {
        string idsLine = DataController.Instance.LoadCollectedAchievements();
        if (string.IsNullOrEmpty(idsLine))
            return;

        string[] ids = idsLine.Split(' ');
        int id = -1;

        foreach (string idStr in ids)
        {
            if (int.TryParse(idStr, out id))
            {
                _idsCollected.Add(id);
            }
        }
    }

    private void SaveCollected()
    {
        string ids = "";
        foreach(int id in _idsCollected)
        {
            ids += id.ToString() + " ";
        }

        DataController.Instance.SaveCollectedAchievements(ids);
    }

    public bool GetCollectedStatus(int id)
    {
        foreach (int idS in _idsCollected)
        {
            if (idS == id)
                return true;
        }

        return false;
    }

    public AchievementConfig GetById(int id)
    {
        foreach (AchievementConfig c in configs)
        {
            if(c.id == id)
                return c;
        }

        return null;
    }

    public bool Verify(int refID, int value)
    {
        if(GetCollectedStatus(refID))
            return true; 

        foreach (AchievementConfig c in configs)
        {
            if (c.id == refID)
            {
                if (c.Verify(value))
                {
                    _idsCollected.Add(c.id);
                    SaveCollected();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
}
