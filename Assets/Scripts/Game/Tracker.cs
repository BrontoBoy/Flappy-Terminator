using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _xOffset;
    
    private void Update()
    {
        if (_player == null)
            return;
        
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 position = transform.position;
        position.x = _player.transform.position.x + _xOffset;
        transform.position = position;
    }
}