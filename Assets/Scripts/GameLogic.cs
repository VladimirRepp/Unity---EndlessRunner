using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Event Throwers Settings")]
    [SerializeField] private PlayerChecksObstacles playerChecksObstacles;
    [SerializeField] private PlayerScorer scorer;

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
        if (_currentIndexSettings == _levelConfigs.Length - 1)
            return;

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
