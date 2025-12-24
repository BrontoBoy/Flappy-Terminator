using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour, IDestructible
{
    private const float MinimumValidDirectionMagnitude = 0.01f;
    
    [SerializeField] private float _speed = 10f;
    [SerializeField] private LayerMask _targetLayer;
    
    private Rigidbody2D _rigidbody;
    private ProjectilePool _pool;
    private Player _playerOwner;
    private GameObject _owner;
    private bool _hasHit = false;
    
    public ProjectilePool Pool { set { _pool = value; } }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Collider2D collider = GetComponent<Collider2D>();
        
        if (collider != null)
            collider.isTrigger = true;

    }
    
    public void SetOwner(GameObject owner)
    {
        _owner = owner;
        
        if (_owner != null)
        {
            _playerOwner = _owner.GetComponent<Player>();
        }
        else
        {
            _playerOwner = null;
        }
    }
    
    public bool IsOwnedByPlayer()
    {
        return _playerOwner != null;
    }
    
    public void Initialize(Vector2 position, Vector2 direction, Quaternion rotation)
    {
        _hasHit = false;
        
        if (_rigidbody == null)
            return;
        
        transform.position = position;
        transform.rotation = rotation;
        
        if (direction.magnitude > MinimumValidDirectionMagnitude)
        {
            _rigidbody.linearVelocity = direction.normalized * _speed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector2.right * _speed;
        }
    }
    
    public void Destroy()
    {
        if (_pool != null)
        {
            _pool.ReturnObject(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled == false) 
            return;
        
        if (_hasHit) 
            return;
        
        if (_owner != null && other.gameObject == _owner)
            return;
        
        if (((1 << other.gameObject.layer) & _targetLayer) != 0)
        {
            _hasHit = true;
            
            if (other.TryGetComponent(out IDestructible destructible))
            {
                if (destructible is Enemy enemy && IsOwnedByPlayer())
                    enemy.MarkAsDestroyedByPlayer();
                
                destructible.Destroy();
            }
            
            Destroy();
        }
    }
}