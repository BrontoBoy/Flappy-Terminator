using UnityEngine;
using UnityEngine.Serialization;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour
{
    [SerializeField] protected float SpawnRate = 2f;
    [SerializeField] protected float MinY = -4f;
    [SerializeField] protected float MaxY = 4f;
    [SerializeField] protected float SpawnX = 12f;
    [SerializeField] protected ObjectPool<T> ObjectPool;
    
    protected float TimeSinceLastSpawn;
    protected bool IsSpawning = false;
    
    public System.Action<T> ObjectSpawned { get; set; }
    
    public virtual void Initialize()
    {
        TimeSinceLastSpawn = 0f;
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
    
    protected virtual void Update()
    {
        if (IsSpawning == false) 
            return;
        
        TimeSinceLastSpawn += Time.deltaTime;
        
        if (TimeSinceLastSpawn >= SpawnRate)
        {
            Spawn();
            TimeSinceLastSpawn = 0f;
        }
    }
    
    protected abstract void Spawn();
    
    protected Vector3 GetRandomSpawnPosition()
    {
        float randomY = Random.Range(MinY, MaxY);
        
        return new Vector3(SpawnX, randomY, 0);
    }
}