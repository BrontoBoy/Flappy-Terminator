using UnityEngine;

public class ProjectilePool : GameObjectPool<Projectile>
{
    protected override void Awake()
    {
        base.Awake();
    }
    
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
    
    protected override void OnTakeFromPool(Projectile item)
    {
        base.OnTakeFromPool(item);
        
        item.Destroyed -= OnProjectileDestroyed;
        item.Destroyed += OnProjectileDestroyed;
    }
    
    protected override void OnReturnedToPool(Projectile projectile)
    {
        if (projectile != null)
            projectile.Destroyed -= OnProjectileDestroyed;
        
        base.OnReturnedToPool(projectile);
    }  
    
    private void OnProjectileDestroyed(Projectile projectile)
    {
        if (projectile != null)
        {
            projectile.Destroyed -= OnProjectileDestroyed;
            
            ReturnObject(projectile);
        }
    }
}