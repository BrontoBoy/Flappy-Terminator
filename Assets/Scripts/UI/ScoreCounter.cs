using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _scoreValue;
    private int _scoreStartValue = 0;

    public event Action<int> ScoreChanged;

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