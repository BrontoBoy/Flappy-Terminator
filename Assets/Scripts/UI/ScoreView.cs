using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _score;

    
    private void Start()
    {
        if (_scoreCounter != null)
        {
            _scoreCounter.ScoreChanged += OnScoreChanged;
            OnScoreChanged(0); 
        }
    }
    
    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        if (_score != null)
        {
            _score.text = score.ToString();
        }
    }
}
