using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private const float DefaultSpawnDistanceFromPlayer = 15f; 
    
    [SerializeField] private float _spawnDistanceFromPlayer = DefaultSpawnDistanceFromPlayer;
    [SerializeField] private Player _player;
    
    protected override void InitializeObject(Enemy enemy, Vector2 direction = default)
    {
        base.InitializeObject(enemy, direction);
        
        enemy.DestroyedByPlayer += OnEnemyDestroyedByPlayer;
    }
    
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
        
        return new Vector3(spawnX, randomY, ZeroZValue);
    }
    
    private float CalculateSpawnXPosition()
    {
        if (_player == null)
            return SpawnX;
        
        return _player.transform.position.x + _spawnDistanceFromPlayer;
    }
    
    private void OnEnemyDestroyedByPlayer(Enemy enemy)
    {
        enemy.DestroyedByPlayer -= OnEnemyDestroyedByPlayer;
        
        if (ObjectPool != null)
            ObjectPool.ReturnObject(enemy);
    }
}