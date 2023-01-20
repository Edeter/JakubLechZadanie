using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ReturnToPool
{
    public float Speed;
    public float ReturnDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < ReturnDistance) Return();
        transform.position -= Vector3.right * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.AddPoint();
        Return();
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //}
}
