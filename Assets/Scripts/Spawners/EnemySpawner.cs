using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private const float DefaultSpawnDistanceFromPlayer = 15f; 
    
    [SerializeField] private float _spawnDistanceFromPlayer = DefaultSpawnDistanceFromPlayer;
    [SerializeField] private Player _player;
    
    protected override bool CanSpawn()
    {
        bool baseCanSpawn = base.CanSpawn();
        bool isPlayerSet = _player != null;
        
        return baseCanSpawn && isPlayerSet;
    }
    
    protected override Vector3 GetRandomSpawnPosition()
    {
        if (_player == null)
            return base.GetRandomSpawnPosition(); 
        
        float spawnX = CalculateSpawnXPosition();
        float randomY = GetRandomYPosition();
        
        return new Vector3(spawnX, randomY, ZeroFloatValue);
    }
    
    protected override void InitializeObject(Enemy enemy)
    {
        if (enemy == null)
            return;
        
        EnemyPool enemyPool = ObjectPool as EnemyPool;
        
        if (enemyPool == null)
            return;
        
        enemy.Initialize(enemyPool);
    }
    
    private float CalculateSpawnXPosition()
    {
        if (_player == null)
            return SpawnX;
        
        return _player.transform.position.x + _spawnDistanceFromPlayer;
    }
}