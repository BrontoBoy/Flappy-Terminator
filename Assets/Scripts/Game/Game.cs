using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const float PausedTimeScale = 0f;
    private const float PlayTimeScale = 1f;
    
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    
    private List<ISpawner> _spawners = new List<ISpawner>();
    
    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
        
        _spawners.Clear();
        
        if (_enemySpawner != null)
            _spawners.Add(_enemySpawner);
            
        if (_projectileSpawner != null)
            _spawners.Add(_projectileSpawner);
        
        foreach (ISpawner spawner in _spawners)
        {
            if (spawner != null)
                spawner.Initialize();
        }
        
        foreach (ISpawner spawner in _spawners)
        {
            if (spawner is EnemySpawner enemySpawner)
                enemySpawner.ObjectSpawned += OnEnemySpawned;
        }
    }
    
    private void Start()
    {
        Time.timeScale = PausedTimeScale;
        _startScreen.Open();
    }
    
    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
        
        foreach (ISpawner spawner in _spawners)
        {
            if (spawner is EnemySpawner enemySpawner)
                enemySpawner.ObjectSpawned -= OnEnemySpawned;
        }
    }
    
    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.DestroyedByPlayer -= OnEnemyDestroyedByPlayer;
        enemy.DestroyedByPlayer += OnEnemyDestroyedByPlayer;
    }
    
    private void OnEnemyDestroyedByPlayer(Enemy enemy)
    {
        if (_scoreCounter != null)
            _scoreCounter.Add();
    }

    private void OnGameOver()
    {
        Time.timeScale = PausedTimeScale;
        
        foreach (ISpawner spawner in _spawners)
        {
            if (spawner != null)
                spawner.StopSpawning();
        }
        
        _endGameScreen.Open();
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }
    
    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = PlayTimeScale;
        
        foreach (ISpawner spawner in _spawners)
        {
            if (spawner != null)
            {
                spawner.StopSpawning();
                spawner.ReturnAllObjects();
                
                if (spawner is EnemySpawner)
                    spawner.StartSpawning();
            }
        }
        
        _player.Reset();
    }
}