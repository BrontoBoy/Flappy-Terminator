using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour
{
    protected const float ZeroFloatValue = 0f;
    
    private const float DefaultSpawnCooldown = 2f;
    private const float DefaultMinSpawnHeight = -2f;
    private const float DefaultMaxSpawnHeight = 4f;
    private const float DefaultSpawnDistance = 12f;
    
    [SerializeField] protected float SpawnRate = DefaultSpawnCooldown;
    [SerializeField] protected float MinY = DefaultMinSpawnHeight;
    [SerializeField] protected float MaxY = DefaultMaxSpawnHeight;
    [SerializeField] protected float SpawnX = DefaultSpawnDistance;
    [SerializeField] protected GameObjectPool<T> ObjectPool;
    
    protected float TimeSinceLastSpawn;
    protected bool IsSpawningActive = false;
    
    public System.Action<T> ObjectSpawned { get; set; }

    protected virtual void Update()
    {
        if (IsSpawningActive == false)
            return;
        
        UpdateSpawnTimer();
    }
    
    public virtual void Initialize()
    {
        TimeSinceLastSpawn = ZeroFloatValue;
        IsSpawningActive = false;
    }
    
    public virtual void StartSpawning()
    {
        IsSpawningActive = true;
        TimeSinceLastSpawn = SpawnRate;
    }
    
    public virtual void StopSpawning()
    {
        IsSpawningActive = false;
    }
    
    protected abstract void Spawn();
    
    protected virtual Vector3 GetRandomSpawnPosition()
    {
        float randomY = GetRandomYPosition();
        
        return new Vector3(SpawnX, randomY, ZeroFloatValue);
    }
    
    protected float GetRandomYPosition()
    {
        return Random.Range(MinY, MaxY);
    }
    
    protected bool AreSpawnSettingsValid()
    {
        bool isMinYValid = MinY < MaxY;
        bool isSpawnRateValid = SpawnRate > ZeroFloatValue;
        bool isObjectPoolSet = ObjectPool != null;
        
        return isMinYValid && isSpawnRateValid && isObjectPoolSet;
    }
    
    private void UpdateSpawnTimer()
    {
        TimeSinceLastSpawn += Time.deltaTime;
        
        if (TimeSinceLastSpawn >= SpawnRate) 
        {
            Spawn();
            TimeSinceLastSpawn = ZeroFloatValue;
        }
    }
}