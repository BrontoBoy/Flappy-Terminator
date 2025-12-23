using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
        
        if (_enemySpawner != null)
        {
            _enemySpawner.ObjectSpawned += OnEnemySpawned;
        }
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
        
        if (_enemySpawner != null)
        {
            _enemySpawner.ObjectSpawned -= OnEnemySpawned;
        }
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
        
        if (_enemySpawner != null)
        {
            _enemySpawner.Initialize();
        }
    }
    
    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.Destroyed += OnEnemyDestroyed;
    }
    
    private void OnEnemyDestroyed(Enemy enemy)
    {
        if (enemy.WasDestroyedByPlayer)
        {
            _player.AddScore(1);
        }
        
        enemy.Destroyed -= OnEnemyDestroyed;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        
        if (_enemySpawner != null)
        {
            _enemySpawner.StopSpawning();
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
        Time.timeScale = 1;
        CleanupAllObjects();
        
        if (_enemySpawner != null)
        {
            _enemySpawner.ReturnAllObjects();
            _enemySpawner.StopSpawning();
            _enemySpawner.StartSpawning();
        }
        
        _player.Reset();
    }
}