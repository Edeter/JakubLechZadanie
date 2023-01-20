using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet :  ReturnToPool
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
        if (transform.position.x > ReturnDistance) Return();
        transform.position += Vector3.right * Speed * Time.deltaTime;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            Return();
        }
    }
    public override void Return()
    {
        //gameObject.transform.position = 
        base.Return();
    }
}
