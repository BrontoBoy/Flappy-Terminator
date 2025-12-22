using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _spawnOffsetX = 2f;
    [SerializeField] private float _minY = -4f;
    [SerializeField] private float _maxY = 4f;
    
    public float SpawnX { get; private set; }
    public float MinY => _minY;
    public float MaxY => _maxY;
    
    private void Update()
    {
        if (_camera == null) 
            return;
        
        float cameraRightEdge = _camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        SpawnX = cameraRightEdge + _spawnOffsetX;
        transform.position = new Vector3(SpawnX, (_minY + _maxY) / 2f, 0);
    }
    
    public Vector3 GetRandomSpawnPosition()
    {
        float randomY = Random.Range(_minY, _maxY);
        return new Vector3(SpawnX, randomY, 0);
    }
}