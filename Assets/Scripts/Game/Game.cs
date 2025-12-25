using UnityEngine;

public class Game : MonoBehaviour
{
    private const float StoppedTimeScale = 0f;
    private const float TimeScale = 1f;
    
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    
    private void Start()
    {
        Time.timeScale = StoppedTimeScale;
        _startScreen.Open();
        
        if (_enemySpawner != null)
            _enemySpawner.Initialize();
    }
    
   private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
        
        if (_enemySpawner != null)
            _enemySpawner.ObjectSpawned += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
        
        if (_enemySpawner != null)
            _enemySpawner.ObjectSpawned -= OnEnemySpawned;
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
        
        enemy.DestroyedByPlayer -= OnEnemyDestroyedByPlayer;
    }

    private void OnGameOver()
    {
        Time.timeScale = StoppedTimeScale;
        
        if (_enemySpawner != null)
            _enemySpawner.StopSpawning();
        
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
        Time.timeScale = TimeScale;
        
        if (_enemySpawner != null)
        {
            _enemySpawner.ReturnAllObjects();
            _enemySpawner.StartSpawning();
        }
        
        _player.Reset();
    }
}