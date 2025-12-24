using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDestructible, IInteractable
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _shootCooldown = 2f;
    [SerializeField] private ProjectilePool _enemyProjectilePool;
    
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
        _timeSinceLastShot = Random.Range(0f, _shootCooldown);
        _isScoreAlreadyAdded = false;
    }
    
    private void Update()
    {
        TryShoot();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(-_moveSpeed, 0);
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
        if (_enemyProjectilePool == null || _attackPoint == null) 
            return;
        
        Projectile projectile = _enemyProjectilePool.GetObject();
        
        if (projectile == null)
            return;

        projectile.Pool = _enemyProjectilePool;
        projectile.Initialize(_attackPoint.position, Vector2.left, Quaternion.identity);
        projectile.SetOwner(gameObject);
    }
}