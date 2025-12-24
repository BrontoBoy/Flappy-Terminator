using UnityEngine;

public class Attacker : MonoBehaviour
{   
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    
    private float _lastAttackTime;
    private Player _player;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    
    public void Attack()
    {
        if (Time.time - _lastAttackTime < _cooldown)
            return;
        
        if (_projectileSpawner == null || _attackPoint == null)
            return;
        
        Projectile projectile =_projectileSpawner.SpawnProjectile(_attackPoint.position,_attackPoint.right, _attackPoint.rotation);
        
        if (projectile != null && _player != null)
            projectile.SetOwner(_player.gameObject);
        
        _lastAttackTime = Time.time;
    }
}