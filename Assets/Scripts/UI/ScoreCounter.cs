using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const int InitialScore = 0;
    
    private int _scoreValue;
    private int _scoreStartValue = 0;

    public event Action<int> ScoreChanged;
    
    public int CurrentScore => _scoreValue;

    public void Add()
    {
        _scoreValue++;
        ScoreChanged?.Invoke(_scoreValue);
    }

    public void Reset()
    {
        _scoreValue = _scoreStartValue;
        ScoreChanged?.Invoke(_scoreValue);
    }
}