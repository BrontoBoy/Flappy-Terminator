using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    
    protected override void Spawn()
    {
        if (ObjectPool == null) 
            return;
        
        Enemy enemy = ObjectPool.GetObject();
        enemy.transform.position = GetRandomSpawnPosition();
        enemy.Initialize(ObjectPool);

        ObjectSpawned?.Invoke(enemy);
    }
    
    public void ReturnAllObjects()
    {
        if (ObjectPool != null)
        {
            ObjectPool.ReturnAll();
        }
    }
}
