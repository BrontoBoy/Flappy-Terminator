using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _scoreValueText;
    
    private void OnEnable()
    {
        if (_scoreCounter != null)
        {
            _scoreCounter.ScoreChanged += OnScoreChanged;
            
            OnScoreChanged(_scoreCounter.CurrentScore);
        }
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        if (_scoreValueText != null)
            _scoreValueText.text = score.ToString();
    }
}