using UnityEngine;
using System;

public class ProjectilePool : GameObjectPool<Projectile>
{
    public new Projectile GetObject()
    {
        Projectile projectile = base.GetObject();
        
        if (projectile != null)
        {
            projectile.Destroyed -= OnProjectileDestroyed;
            projectile.Destroyed += OnProjectileDestroyed;
        }
        
        return projectile;
    }
    
    private void OnProjectileHitTarget(Projectile projectile, Collider2D target)
    {
        if (target.TryGetComponent(out IDestructible destructible))
        {
            if (destructible is Enemy enemy)
            {
                enemy.MarkAsDestroyedByPlayer();
            }
            else
            {
                destructible.Destroy();
            }
        }
    }
    
    protected override void OnTakeFromPool(Projectile projectile)
    {
        base.OnTakeFromPool(projectile);
        
        projectile.HitTarget -= OnProjectileHitTarget;
        projectile.Destroyed -= OnProjectileDestroyed;
        
        projectile.HitTarget += OnProjectileHitTarget;
        projectile.Destroyed += OnProjectileDestroyed;
    }
    
    protected override void OnReturnedToPool(Projectile projectile)
    {
        if (projectile != null)
        {
            projectile.HitTarget -= OnProjectileHitTarget;
            projectile.Destroyed -= OnProjectileDestroyed;
        }
        
        base.OnReturnedToPool(projectile);
    }  
    
    private void OnProjectileDestroyed(Projectile projectile)
    {
        projectile.HitTarget -= OnProjectileHitTarget;
        projectile.Destroyed -= OnProjectileDestroyed;
        
        ReturnObject(projectile);
    }
}