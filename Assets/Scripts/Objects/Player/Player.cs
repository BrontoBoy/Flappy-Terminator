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
    
    private bool _isAlive = true;
    public bool IsAlive => _isAlive; 

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
        if (_isAlive == false)
            return;
        
        if (transform.position.y > _maxY)
            ClampToUpperBound();
    }
    
    private void FixedUpdate()
    {
        if (_isAlive == false)
            return;
        
        UpdateMovement();
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
            Destroy();
        }
        else if (interactable is Ground ground)
        {
            Destroy();
        }
    }
    
    public void Destroy()
    {
        if (_isAlive == false)
            return;
        
        _isAlive = false;
        GameOver?.Invoke(); 
    }

    public void AddScore(int points = 1)
    {
        if (_scoreCounter == null || _isAlive == false)
            return;
        
        for (int i = 0; i < points; i++) 
            _scoreCounter.Add();
    }
    
    public void Reset()
    {
        _isAlive = true;
        
        if (_scoreCounter != null)
            _scoreCounter.Reset();
        
        if (_mover != null)
            _mover.Reset();
        
        if (_inputReader != null)
            _inputReader.ResetAllInput();
    }
}
