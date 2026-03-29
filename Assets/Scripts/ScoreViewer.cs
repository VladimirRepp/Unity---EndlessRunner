using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [Header("Event Throwers Settings")]
    [SerializeField] private PlayerScorer scorer;

    [Header("UI Settings")]
    [SerializeField] private TMP_Text scoreText;

    public void OnEnable()
    {
        scorer.OnScoreChanged += HandleScoreChanged;
    }

    public void OnDisable()
    {
        scorer.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
}
