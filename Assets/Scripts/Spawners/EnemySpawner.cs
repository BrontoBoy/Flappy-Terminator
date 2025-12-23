using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private float _spawnDistanceFromPlayer = 15f;
    [SerializeField] private Player _player; 
    
    private float _initialSpawnX;
    private bool _isInitialized = false;
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    protected override void Spawn()
    {
        if (ObjectPool == null || _player == null) return;
        
        Enemy enemy = ObjectPool.GetObject();
        Vector3 spawnPosition = GetRandomSpawnPosition();
        enemy.transform.position = spawnPosition;
        
        enemy.Initialize(ObjectPool as EnemyPool);
        
        ObjectSpawned?.Invoke(enemy);
    }
    
    protected override Vector3 GetRandomSpawnPosition()
    {
        if (_player == null)
        {
            return base.GetRandomSpawnPosition();
        }
        
        float spawnX = _player.transform.position.x + _spawnDistanceFromPlayer;
        float randomY = Random.Range(MinY, MaxY);
        return new Vector3(spawnX, randomY, 0);
    }
    
    public void ReturnAllObjects()
    {
        if (ObjectPool != null)
        {
            ObjectPool.ReturnAll();
        }
    }
}
