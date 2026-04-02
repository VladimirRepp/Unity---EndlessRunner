using UnityEngine;

public class DataController : Singleton<DataController>, IInitialized
{
    private const string _coinsKey = "Coins";
    private const string _purchasedSkinsKey = "P_Skins";
    private const string _selectedSkinsKey = "Selected_Skins";
    private const string _collectedAchievements = "Collected_Achievements";

    public void Startup()
    {

    }

    /// <summary>
    /// Перезаписать количество монет игрока
    /// </summary>
    /// <param name="currentCounCoins"></param>
    public void SaveCoins(int currentCounCoins)
    {
        PlayerPrefs.SetInt(_coinsKey, currentCounCoins);
    }

    public int LoadCoins()
    {
        return PlayerPrefs.GetInt(_coinsKey);
    }

    public void SavePurchasedSkins(string idsScins)
    {
        PlayerPrefs.SetString(_purchasedSkinsKey, idsScins);
    }

    public string LoadPurchasedSkins()
    {
        return PlayerPrefs.GetString(_purchasedSkinsKey);
    }

    public void SetSelectedSkin(int idSelectedSkin)
    {
        PlayerPrefs.SetInt(_selectedSkinsKey, idSelectedSkin);
    }

    public int GetSelectedSkin()
    {
        int id = PlayerPrefs.GetInt(_selectedSkinsKey);

        if (id == 0)
            return 1; // id скинов начинают с 1, по этому если ключа нет, то вернем 1

        return id;
    }

    public void SaveCollectedAchievements(string idsCollected)
    {
        PlayerPrefs.SetString(_collectedAchievements, idsCollected);
    }

    public string LoadCollectedAchievements() {
        return PlayerPrefs.GetString(_collectedAchievements);
    }
}
