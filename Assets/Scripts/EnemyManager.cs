using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ReturnToPool
{
    [SerializeField] float EnemySpeed;
    float lowerBound, HigherBound;
   [SerializeField] GameObject EnemyPrefab;
    float distance;
    [SerializeField] float ReturnDistance;

    public float MaxDelay;
    float currentDelay;

    EnemyPoolManager enemyPoolManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyPoolManager = new EnemyPoolManager(EnemyPrefab, transform, EnemySpeed);

        var renderer = EnemyPrefab.GetComponent<SpriteRenderer>();
        lowerBound = PlayerController.Instance.MinHight + renderer.bounds.size.y / 2;
        HigherBound = PlayerController.Instance.MaxHight - renderer.bounds.size.y / 2;

        distance = HigherBound - lowerBound;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDelay<0)
        {
            SummonWave();
            currentDelay = MaxDelay;
            
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
    void SummonWave()
    {
        int rand = Random.Range(2,6);
        List<int> slots = GetRandomNumbers(rand);

        foreach(int slot in slots)
        {
            SpawnEnemy(slot);
        }



    }
    void SpawnEnemy(int number)
    {
        Debug.Log("Spawning enemy numer "+ number.ToString());
        var obj = enemyPoolManager.Pool.Get();
       Enemy enemy =  obj.GetComponent<Enemy>();
        enemy.ReturnDistance = ReturnDistance;
        obj.transform.position = new Vector3(transform.position.x,lowerBound + (distance / 4 * number),0);
        
    }
    public static List<int> GetRandomNumbers(int count)
    {
        List<int> randomNumbers = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int number;

            do number = Random.Range(0,5);
            while (randomNumbers.Contains(number));

            randomNumbers.Add(number);
        }

        return randomNumbers;
    }
}
