using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour, IDestructible
{
    private const float MinimumValidDirectionMagnitude = 0.01f;
    
    [SerializeField] private float _speed = 10f;
    
    private Rigidbody2D _rigidbody;
    private bool _hasHit = false;
    
    public event Action<Projectile> Destroyed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Projectile hit: {other.name}");
        
        if (enabled == false || _hasHit)
            return;
    
        _hasHit = true;
    
        Destroy();
    
        if (other.TryGetComponent(out IDestructible destructible))
        {
            Debug.Log($"Found destructible: {destructible}");
            
            if (destructible is Enemy enemy)
            {
                Debug.Log("Marking enemy as destroyed by player");
                enemy.MarkAsDestroyedByPlayer();
            }
            else
            {
                destructible.Destroy();
            }
        }
    }
    
    private void OnDisable()
    {
        if (_rigidbody != null)
            _rigidbody.linearVelocity = Vector2.zero;
    }
    
    public void Initialize(Vector2 position, Vector2 direction, Quaternion rotation)
    {
        _hasHit = false;
        
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
        
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
        Destroyed?.Invoke(this);
    }
}