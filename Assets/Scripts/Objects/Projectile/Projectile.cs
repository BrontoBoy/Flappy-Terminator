using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour, IDestructible
{
    private const float DestroyBoundaryX = 20f;
    private const float DestroyBoundaryY = 15f;
    
    [SerializeField] private float _speed = 10f;
    [SerializeField] private LayerMask _targetLayer;
    
    private Rigidbody2D _rigidbody;
    private ProjectilePool _pool;
    
    public ProjectilePool Pool { set { _pool = value; } }
    
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
    
    private void Update()
    {
        if (transform.position.x < -DestroyBoundaryX || transform.position.x > DestroyBoundaryX ||
            transform.position.y < -DestroyBoundaryY || transform.position.y > DestroyBoundaryY)
        {
            Destroy();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _targetLayer) != 0)
        {
            if (other.TryGetComponent(out IDestructible destructible))
            {
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