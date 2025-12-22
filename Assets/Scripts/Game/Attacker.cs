using UnityEngine;

public class Attacker : MonoBehaviour
{   
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private ProjectileSpawner _projectileSpawner;
    
    private float _lastAttackTime;
    
    public void Attack()
    {
        if (Time.time - _lastAttackTime < _cooldown)
        {
            return;
        }
        
        if (_projectileSpawner == null || _attackPoint == null)
            return;
        
        _projectileSpawner.SpawnProjectile(_attackPoint.position,_attackPoint.right,
            _attackPoint.rotation);
        
        _lastAttackTime = Time.time;
    }
    
    public bool IsReadyToAttack()
    {
        return Time.time - _lastAttackTime >= _cooldown;
    }
}