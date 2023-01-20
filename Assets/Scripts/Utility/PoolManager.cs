using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ReturnToPool : MonoBehaviour
{

    public IObjectPool<GameObject> pool;

    public virtual void Return()
    {
        // Return to the pool

        pool.Release(gameObject);
    }

}
public class PoolManager
{
    IObjectPool<GameObject> m_Pool;

    public IObjectPool<GameObject> Pool
    {
        get
        {
            if (m_Pool == null)
            {

                m_Pool = new ObjectPool<GameObject>(CreateObject, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);

            }
            return m_Pool;
        }
    }
     public virtual GameObject CreateObject()
    {
        return default;
    }
    public virtual void OnReturnedToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    public virtual void OnTakeFromPool(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }
    public virtual void OnDestroyPoolObject(GameObject obj)
    {
        GameObject.Destroy(obj.gameObject);
    }

}
