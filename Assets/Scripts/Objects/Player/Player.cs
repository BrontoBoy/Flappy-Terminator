using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(CollisionHandler))]
public class Player : MonoBehaviour, IDestructible
{
    [SerializeField] private float _maxY = 5f;
    
    private InputReader _inputReader;
    private Mover _mover;
    private Attacker _attacker;
    private ScoreCounter _scoreCounter;
    private CollisionHandler _handler;

    public event Action GameOver;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }
    
    private void Update()
    {
        if (transform.position.y > _maxY)
            ClampToUpperBound();
    }
    
    private void FixedUpdate()
    {
        UpdateMovement();
    }
    
    public void Destroy()
    {
        GameOver?.Invoke(); 
    }
    
    public void Reset()
    {
        _scoreCounter?.Reset();
        _mover?.Reset();
        _inputReader?.ResetAllInput();
    }
    
    private void UpdateMovement()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }
    
    private void ClampToUpperBound()
    {
        Vector3 position = transform.position;
        position.y = _maxY;
        transform.position = position;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        
        if (rigidbody != null && rigidbody.linearVelocity.y > 0)
        {
            Vector2 velocity = rigidbody.linearVelocity;
            velocity.y = 0;
            rigidbody.linearVelocity = velocity;
        }
    }
    
    private void HandleMovement()
    {
        _mover.Move();
    }

    private void HandleJump()
    {
        if (_inputReader.IsJump)
        {
            _mover.Jump();
            _inputReader.ResetJump();
        }
    }

    private void HandleAttack()
    {
        if (_inputReader.IsAttack)
        {
            _inputReader.ResetAttack();
            _attacker.Attack();
        }
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Enemy enemy)
        {
            enemy.Destroy();
            Destroy();
        }
        else if (interactable is Ground ground)
        {
            Destroy();
        }
    }
}