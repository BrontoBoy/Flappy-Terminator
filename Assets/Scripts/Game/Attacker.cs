using UnityEngine;

public class Attacker : MonoBehaviour
{   
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private Spawner<Projectile> _projectileSpawner;
    
    private float _lastAttackTime;
    
    public void Attack()
    {
        if (Time.time - _lastAttackTime < _cooldown)
            return;
        
        if (_projectileSpawner == null || _attackPoint == null)
            return;
        
        Projectile projectile = _projectileSpawner.SpawnAtPosition(_attackPoint.position, _attackPoint.right, _attackPoint.rotation);
        
        _lastAttackTime = Time.time;
    }
}