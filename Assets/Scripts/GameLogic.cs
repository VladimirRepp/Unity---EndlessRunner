using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Event Throwers Settings")]
    [SerializeField] private PlayerChecksObstacles playerChecksObstacles;
    [SerializeField] private PlayerScorer scorer;

    [Header("Systems Settings")]
    [SerializeField] private AchievementSystem achievementSystem;

    [Header("Observer Level Config Settings")]
    [SerializeField] private ObserverLevelConfig[] levelConfigSubscribers;

    [Header("Level Balance Settings")]
    [SerializeField] private LevelConfig[] _levelConfigs;

    [Header("UI Settings")]
    [SerializeField] private GameObject ui_gameEnd;

    [Header("Move Settings")]
    [SerializeField] private Treadmill treadmill;
    [SerializeField] private PlayerInput playerInput;

    private int _currentIndexSettings = 0;
    private int _prevScoreSettingsActivation = 0;

    private bool _statusAchievId2 = false;
    private bool _statusAchievId3 = false;
    private string _achievNameId2;
    private string _achievNameId3;

    public Action<LevelConfig> OnChangeLevelBalanceSettings;

    private void Awake()
    {
        playerInput.enabled = true;
        ui_gameEnd.SetActive(false);

        foreach (var s in levelConfigSubscribers)
            s.Startup(_levelConfigs[_currentIndexSettings]);
    }

    private void OnEnable()
    {
        playerChecksObstacles.OnCrashed += HandleCrashed;

        scorer.OnScoreChanged += HandleScoreChanged;

        AdvManager.Instance.OnRevarded += HandleRevarded;
        AdvManager.Instance.OnErrorRevarded += HandleErrorRevarded;
    }

    private void OnDisable()
    {
        playerChecksObstacles.OnCrashed -= HandleCrashed;

        scorer.OnScoreChanged += HandleScoreChanged;

        if (AdvManager.Instance != null)
        {
            AdvManager.Instance.OnRevarded -= HandleRevarded;
            AdvManager.Instance.OnErrorRevarded -= HandleErrorRevarded;
        }        
    }

    private void NotifyLevelConfigSubscribers()
    {
        Debug.Log("NotifyLevelConfigSubscribers called!");

        foreach (var s in levelConfigSubscribers)
            s.UpadteSettings(_levelConfigs[_currentIndexSettings]);
    }

    private void CheckAchievs(int currentScore)
    {
        // Пример оптимизации  
        if (!_statusAchievId2)
            _statusAchievId2 = achievementSystem.Verify(refID: 2, currentScore);

        if (!_statusAchievId3)
            _statusAchievId3 = achievementSystem.Verify(refID: 3, currentScore);

        _achievNameId2 = achievementSystem.GetById(2).header;
        _achievNameId3 = achievementSystem.GetById(3).header;

        // todo: выдать уведомление о получении достижения
        Debug.Log($"{_achievNameId2} status - {_statusAchievId2} |" +
            $" {_achievNameId3} status - {_statusAchievId3}");

        if (_currentIndexSettings == _levelConfigs.Length - 1)
            return;
    }

    private void HandleCrashed()
    {
        ui_gameEnd.SetActive(true);
        playerInput.enabled = false;
        treadmill.StopMove();
    }

    private void HandleRevarded()
    {
        ui_gameEnd.SetActive(false);
        playerInput.enabled = true;
        treadmill.StartMove();
    }

    private void HandleErrorRevarded()
    {
        Debug.Log("Ошибка показа рекламы за вознаграждение!");
    }

    private void HandleScoreChanged(int currentScore)
    {
        CheckAchievs(currentScore);

        int next = _currentIndexSettings < _levelConfigs.Length - 1 ?
                _currentIndexSettings + 1 : _levelConfigs.Length - 1;

        // Проверка активации  
        if (currentScore == _prevScoreSettingsActivation + _levelConfigs[next].stepActivation)
        {
            _prevScoreSettingsActivation = currentScore;

            _currentIndexSettings = _currentIndexSettings < _levelConfigs.Length - 1 ?
                _currentIndexSettings + 1 : _levelConfigs.Length - 1;

            NotifyLevelConfigSubscribers();
        }
    }
}
