using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private ProjectilePool _projectilePool; 
    
    public Projectile SpawnProjectile(Vector3 position, Vector2 direction, Quaternion rotation)
    {
        if (_projectilePool == null)
            return null;
        
        Projectile projectile = _projectilePool.GetObject();
        
        projectile.Initialize(position, direction, rotation);
        
        return projectile;
    }
}