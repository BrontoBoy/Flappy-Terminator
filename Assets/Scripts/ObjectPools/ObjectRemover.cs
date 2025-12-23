using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    [SerializeField] private float _destroyOffset = 20f;
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (_mainCamera == null) return;
        
        float cameraHeight = 2f * _mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * _mainCamera.aspect;
        Vector3 cameraPosition = _mainCamera.transform.position;
        
        float leftBound = cameraPosition.x - cameraWidth / 2 - _destroyOffset;
        float rightBound = cameraPosition.x + cameraWidth / 2 + _destroyOffset;
        float bottomBound = cameraPosition.y - cameraHeight / 2 - _destroyOffset;
        float topBound = cameraPosition.y + cameraHeight / 2 + _destroyOffset;
        
        Vector3 position = transform.position;
        
        if (position.x < leftBound || position.x > rightBound ||
            position.y < bottomBound || position.y > topBound)
        {
            DestroyObject();
        }
    }
    
    private void DestroyObject()
    {
        if (TryGetComponent<IDestructible>(out var destructible))
        {
            destructible.Destroy();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    public void ForceDestroy()
    {
        DestroyObject();
    }
}