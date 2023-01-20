using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float MinHight, MaxHight;

    [SerializeField]
    public int Lives = 3;

    [SerializeField]
    [Tooltip("in secconds")]
    float ShootCooldownMax;
    float ShootCooldown;

    [SerializeField]
    float MoveSpeed = 1;

    [SerializeField]
    float BulletSpeed = 1;
    // Start is called before the first frame update

    [SerializeField] GameObject BulletPrefab;
    BulletPoolManager BulletManager;
    SpriteRenderer renderer;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        BulletManager = new BulletPoolManager(BulletPrefab, transform, BulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical")!= 0)
        {
            float newPositionY = (transform.position.y + Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);
            newPositionY = Mathf.Clamp(newPositionY, MinHight+ renderer.bounds.size.y / 2, MaxHight - renderer.bounds.size.y / 2);
            transform.position = new Vector3(transform.position.x,newPositionY,transform.position.z);
        }

        if (ShootCooldown < 0 && Input.GetKeyDown(KeyCode.Space))
        {

            Shoot();

        }
        ShootCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        Debug.Log("Pew");
        ShootCooldown = ShootCooldownMax;
        GameObject bullet = BulletManager.Pool.Get();
        bullet.transform.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Lives--;
            if (Lives <= 0) GameManager.Instance.onDeath();
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 top, bottom,cube;
        top = new Vector3(gameObject.transform.position.x, MaxHight,0);
        bottom = new Vector3(gameObject.transform.position.x, MinHight,0);
        cube = new Vector3(10, 0.2f, 1);

        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(top, cube);
        Gizmos.DrawCube(bottom, cube);
    }
}
