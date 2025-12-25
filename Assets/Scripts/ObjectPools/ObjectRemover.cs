using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
            return;
        
        if (other.TryGetComponent(out IDestructible destructible))
        {
            destructible.Destroy();
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }
}