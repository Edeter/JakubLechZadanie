using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;




public class BulletPoolManager : PoolManager
{
   
    GameObject BulletPrefab;
    Transform Origin;
    float Speed;
    float ReturnDistance = 15;
    public BulletPoolManager(GameObject prefab, Transform originTransform,float speed)
    {
        BulletPrefab = prefab;
        Origin = originTransform;
        Speed = speed;
    }
    // Start is called before the first frame update
    public override GameObject CreateObject()
    { 
        var obj = GameObject.Instantiate(BulletPrefab, Origin.transform.position, Quaternion.identity, Origin.parent);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.pool = Pool;
        bullet.Speed = Speed;
        bullet.ReturnDistance = ReturnDistance;
        return obj;
    }

}
