using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{   
    private const float ZeroRotationX = 0f;
    private const float ZeroRotationY = 0f;
    
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    
    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _maxRotation = Quaternion.Euler(ZeroRotationX, ZeroRotationY, _maxRotationZ);
        _minRotation = Quaternion.Euler(ZeroRotationX, ZeroRotationY, _minRotationZ);

        Reset();
    }

    public void Move()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
    
    public void Jump()
    {
        _rigidbody2D.linearVelocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }
    
    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody2D.linearVelocity = Vector2.zero;
    }
}