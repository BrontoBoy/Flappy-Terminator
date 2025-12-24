using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _scoreValueText;
    
    private void Awake()
    {
        if (_scoreCounter != null)
            _scoreCounter.ScoreChanged += OnScoreChanged;
    }
    
    private void Start()
    {
        OnScoreChanged(0); 
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
        if (_scoreValueText != null)
            _scoreValueText.text = score.ToString();
    }
}