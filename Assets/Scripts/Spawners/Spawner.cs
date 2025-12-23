using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour
{
    private const float DefaultSpawnCooldown = 2f;
    private const float DefaultMinSpawnHeight = -2f;
    private const float DefaultMaxSpawnHeight = 4f;
    private const float DefaultSpawnDistance = 12f;
    protected const float ZeroFloatValue = 0f;
    
    [SerializeField] protected float SpawnRate = DefaultSpawnCooldown;
    [SerializeField] protected float MinY = DefaultMinSpawnHeight;
    [SerializeField] protected float MaxY = DefaultMaxSpawnHeight;
    [SerializeField] protected float SpawnX = DefaultSpawnDistance;
    [SerializeField] protected GameObjectPool<T> ObjectPool;
    
    protected float TimeSinceLastSpawn;
    protected bool IsSpawning = false;
    
    public System.Action<T> ObjectSpawned { get; set; }

    protected virtual void Update()
    {
        if (IsSpawning == false)
            return;
        
        UpdateSpawnTimer();
    }
    
    public virtual void Initialize()
    {
        TimeSinceLastSpawn = ZeroFloatValue;
        IsSpawning = false;
    }
    
    public virtual void StartSpawning()
    {
        IsSpawning = true;
        TimeSinceLastSpawn = SpawnRate;
    }
    
    public virtual void StopSpawning()
    {
        IsSpawning = false;
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