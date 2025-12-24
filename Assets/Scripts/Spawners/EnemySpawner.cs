using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private const float DefaultSpawnDistanceFromPlayer = 15f; 
    
    [SerializeField] private float _spawnDistanceFromPlayer = DefaultSpawnDistanceFromPlayer;
    [SerializeField] private Player _player;
    
    public void ReturnAllObjects()
    {
        if (ObjectPool != null)
            ObjectPool.ReturnAll();
    }
    
    protected override void Spawn()
    {
        if (CanSpawn() == false)
            return;
        
        Enemy enemy = CreateEnemy();
        Vector3 spawnPosition = GetRandomSpawnPosition();
        enemy.transform.position = spawnPosition;
        
        InitializeEnemy(enemy);
        NotifyEnemySpawned(enemy);
    }
    
    protected override Vector3 GetRandomSpawnPosition()
    {
        if (_player == null)
            return base.GetRandomSpawnPosition(); 
        
        return CalculateEnemySpawnPosition();
    }
    
    private bool CanSpawn()
    {
        bool isObjectPoolSet = ObjectPool != null;
        bool isPlayerSet = _player != null;
        bool areSettingsValid = AreSpawnSettingsValid();
        
        return isObjectPoolSet && isPlayerSet && areSettingsValid;
    }
    
    private Enemy CreateEnemy()
    {
        Enemy enemy = ObjectPool.GetObject();
        
        if (enemy == null)
            return null;
        
        return enemy;
    }
    
    private Vector3 CalculateEnemySpawnPosition()
    {
        float spawnX = CalculateSpawnXPosition();
        float randomY = GetRandomYPosition();
        
        return new Vector3(spawnX, randomY, ZeroFloatValue);
    }
    
    private float CalculateSpawnXPosition()
    {
        if (_player == null)
            return SpawnX;
        
        return _player.transform.position.x + _spawnDistanceFromPlayer;
    }
    
    private void InitializeEnemy(Enemy enemy)
    {
        if (enemy == null)
            return;
        
        EnemyPool enemyPool = ObjectPool as EnemyPool;
        
        if (enemyPool == null)
            return;
        
        enemy.Initialize(enemyPool);
    }
    
    private void NotifyEnemySpawned(Enemy enemy)
    {
        if (enemy == null)
            return;
        
        ObjectSpawned?.Invoke(enemy);
    }
}