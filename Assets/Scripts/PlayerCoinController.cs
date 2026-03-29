using System;
using UnityEngine;

public class PlayerCoinController : MonoBehaviour
{
    [SerializeField] private PlayerScorer scorer;

    private int _currentCountCouns;

    private void OnEnable()
    {
        scorer.OnScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        scorer.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int coins)
    {
        _currentCountCouns = DataController.Instance.LoadCoins();
        _currentCountCouns += coins;
        DataController.Instance.SaveCoins(_currentCountCouns);
    }
}
