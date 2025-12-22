using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab; 
    [SerializeField] protected int InitialSize = 10;
    
    protected Queue<T> Prefabs;
    
    protected virtual void Awake()
    {
        InitializePool();
    }
    
    private void InitializePool()
    {
        Prefabs = new Queue<T>();
        
        for (int i = 0; i < InitialSize; i++)
        {
            CreateNewObject();
        }
    }
    
    protected virtual T CreateNewObject()
    {
        T newObject = Instantiate(Prefab);
        newObject.gameObject.SetActive(false);
        Prefabs.Enqueue(newObject);
        
        return newObject;
    }
    
    public virtual T GetObject()
    {
        if (Prefabs.Count == 0)
            return CreateNewObject();
        
        T prefab = Prefabs.Dequeue();
        prefab.gameObject.SetActive(true);
        prefab.transform.SetParent(null);
        
        return prefab;
    }
    
    public virtual void ReturnObject(T prefab)
    {
        if (prefab == null)
            return;
        
        prefab.gameObject.SetActive(false);
        prefab.transform.SetParent(transform);
        Prefabs.Enqueue(prefab);
    }
    
    public virtual void ReturnAll()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf && child.TryGetComponent(out T component))
            {
                ReturnObject(component);
            }
        }
    }
}