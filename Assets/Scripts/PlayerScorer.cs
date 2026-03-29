using System;
using UnityEngine;

public class PlayerScorer : MonoBehaviour
{
    [SerializeField] private Treadmill treadmill;

    private int _currentScore = 0;
    private int _prevScore = 0;

    public Action<int> OnScoreChanged;

    private void FixedUpdate()
    {
        _prevScore = (int)(treadmill.gameObject.transform.position.z / 100 * -1);
       
        if(_currentScore != _prevScore)
        {
            _currentScore = _prevScore;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}
