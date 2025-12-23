using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour, IDestructible
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private LayerMask _targetLayer;
    
    private Rigidbody2D _rigidbody;
    private ProjectilePool _pool;
    private GameObject _owner;
    
    public ProjectilePool Pool { set { _pool = value; } }
    
    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }
    
    public bool IsOwnedByPlayer()
    {
        return _owner != null && _owner.CompareTag("Player");
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;
    }
    
    public void Initialize(Vector2 position, Vector2 direction, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        _rigidbody.linearVelocity = direction.normalized * _speed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_owner != null && other.gameObject == _owner)
        {
            return;
        }
        
        if (((1 << other.gameObject.layer) & _targetLayer) != 0)
        {
            if (other.TryGetComponent(out IDestructible destructible))
            {
                if (destructible is Enemy enemy && IsOwnedByPlayer())
                {
                    enemy.MarkAsDestroyedByPlayer();
                }
                
                destructible.Destroy();
            }
            
            Destroy();
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
}