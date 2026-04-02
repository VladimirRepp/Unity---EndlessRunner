using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VladimirFps.Utils;

public class AchievementViewer : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text status;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text description;

    [Header("System Settings")]
    [SerializeField] private AchievementSystem system;

    private int _currentIndexConf = 0;

    private void Start()
    {
        ViewAchievement();      
    }

    private void ViewAchievement()
    {
        if (system.Configs.Length == 0)
            return;

        var conf = system.Configs[_currentIndexConf];
        icon.sprite = conf.icon;
        header.text = conf.header;
        description.text = conf.description;

        status.text = system.GetCollectedStatus(conf.id) ?
            "Collected" : "Not Collected";
    }

    public void OnClick_Next()
    {
        _currentIndexConf = _currentIndexConf < system.Configs.Length - 1 ?
            _currentIndexConf + 1 : 0;

        ViewAchievement();
    }

    public void OnClick_Prev()
    {
        _currentIndexConf = _currentIndexConf > 0 ?
            _currentIndexConf - 1 : system.Configs.Length - 1;

        ViewAchievement();
    }

    public void OnClick_Menu()
    {
        MySceneManager.LoadScene(0);
    }
}
