using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDestructible, IInteractable
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _shootCooldown = 2f;
    [SerializeField] private ProjectilePool _projectilePool;
    
    private float _timeSinceLastShot;
    private Rigidbody2D _rigidbody;
    private ObjectPool<Enemy> _enemyPool;
    private bool _destroyedByPlayer = false;
    
    public System.Action<Enemy> Destroyed;
    public bool WasDestroyedByPlayer => _destroyedByPlayer;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;
    }
    
    private void OnEnable()
    {
        _timeSinceLastShot = Random.Range(0f, _shootCooldown);
        _destroyedByPlayer = false;
    }
    
    private void Update()
    {
        TryShoot();
    }
    
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(-_moveSpeed, 0);
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
        if (_projectilePool == null || _attackPoint == null) 
            return;
        
        var projectile = _projectilePool.GetObject();
        projectile.Pool = _projectilePool;
        projectile.Initialize(_attackPoint.position, Vector2.left, Quaternion.identity);
        projectile.SetOwner(gameObject);
    }
    
    public void Initialize(ObjectPool<Enemy> enemyPool)
    {
        _enemyPool = enemyPool;
    }
    
    public void MarkAsDestroyedByPlayer()
    {
        _destroyedByPlayer = true;
    }
    
    public void Destroy()
    {
        Destroyed?.Invoke(this);
        
        if (_enemyPool != null)
        {
            _enemyPool.ReturnObject(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}