using UnityEngine;

public class ProjectileSpawner : Spawner<Projectile>
{
    public override Projectile SpawnObject(Vector3 position, Vector2 direction, Quaternion rotation)
    {
        if (ObjectPool == null)
            return null;
        
        Projectile projectile = ObjectPool.GetObject();
        
        if (projectile != null)
            projectile.Initialize(position, direction, rotation);
        
        return projectile;
    }
    
    public override void StartSpawning()
    {
        IsSpawningActive = true;
    }
    
    public override void StopSpawning()
    {
        IsSpawningActive = true;
    }
    
    public Projectile SpawnProjectile(Vector3 position, Vector2 direction, Quaternion rotation)
    {
        return SpawnObject(position, direction, rotation);
    }
    
    protected override void InitializeObject(Projectile projectile) { }
}