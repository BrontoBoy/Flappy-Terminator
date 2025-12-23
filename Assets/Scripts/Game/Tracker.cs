using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float _xOffset;

    private float _initialCameraX;
    
    private void Start()
    {
        _initialCameraX = transform.position.x;
    }
    
    private void Update()
    {
        var position = transform.position;
        position.x = player.transform.position.x + _xOffset;
        transform.position = position;
    }
}
