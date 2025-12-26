using UnityEngine;
using System;

public class ProjectileSpawner : Spawner<Projectile>
{
    protected override void Start()
    {
        base.Start();
        UseAutoSpawn = false;
    }
    
    protected override void InitializeObject(Projectile projectile, Vector2 direction = default)
    {
        if (projectile != null)
            projectile.Initialize(projectile.transform.position, direction, projectile.transform.rotation);
    }
    
    protected override void NotifyObjectSpawned(Projectile projectile)
    {
        base.NotifyObjectSpawned(projectile);
    }
}