using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : PoolManager
{
    GameObject EnemyPrefab;
    Transform Origin;
    float Speed;
    float ReturnDistance = -15;
    Vector3 SpawnPoint;
    public EnemyPoolManager(GameObject prefab, Transform originTransform, float speed)
    {
        EnemyPrefab = prefab;
        Origin = originTransform;
        Speed = speed;
    }

    public override GameObject CreateObject()
    {
        var obj = GameObject.Instantiate(EnemyPrefab, SpawnPoint, Quaternion.identity);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.pool = Pool;
        enemy.Speed = Speed;
        enemy.ReturnDistance = ReturnDistance;
        return obj;
    }
}
