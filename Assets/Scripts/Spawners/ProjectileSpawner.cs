using UnityEngine;

public class ProjectileSpawner : Spawner<Projectile>
{
    protected override void Start()
    {
        base.Start();
        UseAutoSpawn = false;
    }
    
    public override Projectile SpawnObject(Vector3 position, Vector2 direction, Quaternion rotation)
    {
        if (ObjectPool == null)
            return null;
        
        Projectile projectile = ObjectPool.GetObject();
        
        if (projectile != null)
            projectile.Initialize(position, direction, rotation);
        
        return projectile;
    }
    
    protected override void InitializeObject(Projectile projectile) { }
}