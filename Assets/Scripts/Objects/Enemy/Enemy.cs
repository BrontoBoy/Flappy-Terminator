using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDestructible, IInteractable
{
    private const float ZeroVelocityY = 0f;
    
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _shootCooldown = 2f;
    [SerializeField] private float _minRandomDelay = 0f;
    [SerializeField] private float _maxRandomDelay = 2f;
    [SerializeField] private Spawner<Projectile> _enemyProjectileSpawner;
    
    private float _timeSinceLastShot;
    private Rigidbody2D _rigidbody;
    private EnemyPool _enemyPool;
    private bool _isScoreAlreadyAdded = false;
    
    public event Action<Enemy> DestroyedByPlayer;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();
        
        if (collider != null)
            collider.isTrigger = true;
    }
    
    private void OnEnable()
    {
        _timeSinceLastShot = Random.Range(_minRandomDelay, _maxRandomDelay);
        _isScoreAlreadyAdded = false;
    }
    
    private void Update()
    {
        TryShoot();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(-_moveSpeed, ZeroVelocityY);
    }
    
    public void Initialize(EnemyPool enemyPool)
    {
        if (enemyPool == null) 
            return;
        
        _enemyPool = enemyPool;
    }
    
    public void MarkAsDestroyedByPlayer()
    {
        if (gameObject.activeInHierarchy == false) 
            return;
        
        if (_isScoreAlreadyAdded) 
            return;
        
        _isScoreAlreadyAdded = true;
        
        DestroyedByPlayer?.Invoke(this);
        
        Destroy();
    }
    
    public void Destroy()
    {
        if (_enemyPool != null)
        {
            _enemyPool.ReturnObject(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    private void TryShoot()
    {
        _timeSinceLastShot += Time.deltaTime;
        
        if (_timeSinceLastShot >= _shootCooldown)
        {
            Shoot();
            
            _timeSinceLastShot = 0f;
        }
    }
    
    private void Shoot()
    {
        if (_enemyProjectileSpawner == null || _attackPoint == null) 
            return;
        
        Projectile projectile = _enemyProjectileSpawner.SpawnObject(_attackPoint.position, Vector2.left, Quaternion.identity);
        
        if (projectile == null)
            return;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Projectile projectile))
            MarkAsDestroyedByPlayer();
    }
}