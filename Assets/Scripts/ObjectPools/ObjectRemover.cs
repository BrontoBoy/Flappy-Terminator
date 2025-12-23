using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    private const float CameraHeightMultiplier = 2f;
    private const float DefaultDestroyOffset = 20f;
    private const float HalfDivider = 2f;
    
    [SerializeField] private float _destroyOffset = DefaultDestroyOffset;
    
    private Camera _mainCamera;
    private float _leftBound;
    private float _rightBound;
    private float _bottomBound;
    private float _topBound;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        CalculateBounds();
    }
    
    private void Update()
    {
        if (_mainCamera == null)
            return;
        
        CalculateBounds();
        
        bool isObjectOutOfBounds = IsObjectOutOfBounds();
        
        if (isObjectOutOfBounds == true)
            Destroy();
    }
    
    public void ForceDestroy()
    {
        Destroy();
    }
    
    public static float CalculateRecommendedOffset(Camera camera)
    {
        if (camera == null)
            return DefaultDestroyOffset;
        
        float cameraHeight = CameraHeightMultiplier * camera.orthographicSize;
        return cameraHeight * CameraHeightMultiplier;
    }
    
    private void CalculateBounds()
    {
        if (_mainCamera == null)
            return;
        
        float cameraHeight = CameraHeightMultiplier * _mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * _mainCamera.aspect;
        Vector3 cameraPosition = _mainCamera.transform.position;
        
        _leftBound = cameraPosition.x - cameraWidth / HalfDivider - _destroyOffset;
        _rightBound = cameraPosition.x + cameraWidth / HalfDivider + _destroyOffset;
        _bottomBound = cameraPosition.y - cameraHeight / HalfDivider - _destroyOffset;
        _topBound = cameraPosition.y + cameraHeight / HalfDivider + _destroyOffset;
    }
    
    private bool IsObjectOutOfBounds()
    {
        Vector3 position = transform.position;
        bool isOutOfLeftBound = position.x < _leftBound;
        bool isOutOfRightBound = position.x > _rightBound;
        bool isOutOfBottomBound = position.y < _bottomBound;
        bool isOutOfTopBound = position.y > _topBound;
        
        return isOutOfLeftBound || isOutOfRightBound || isOutOfBottomBound || isOutOfTopBound;
    }
    
    private void Destroy()
    {
        
        if (TryGetComponent<IDestructible>(out IDestructible destructible))
        {
            destructible.Destroy();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}