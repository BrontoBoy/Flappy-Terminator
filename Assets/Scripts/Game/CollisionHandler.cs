using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player; 
    
    public event Action<IInteractable> CollisionDetected;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            CollisionDetected?.Invoke(interactable);
        }
        else if (other.TryGetComponent(out Projectile projectile))
        {
            if (projectile.IsOwnedByPlayer() == false && _player != null)
                _player.Destroy();
        }
    }
}