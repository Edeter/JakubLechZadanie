using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] tileObjects;
    Renderer[] tileRenderers;
    [SerializeField]
    float scrollSpeed = 1f;
    [SerializeField]
    float PointToSwap;
    // Start is called before the first frame update

    struct bgTile
    {
        public Renderer renderer;
        public GameObject gameObject;
       public bgTile(GameObject obj)
        {
            gameObject = obj;
            renderer = gameObject.GetComponent<Renderer>();
        }
    }
    bgTile[] tiles;
    void Start()
    {
        tiles = new bgTile[tileObjects.Length];
        for(int i=0; i<tiles.Length;i++)
        {
            tiles[i] = new bgTile(tileObjects[i]);
        }

        SetStartPositions();
    }

    void SetStartPositions()
    {
        for (int i = 1; i < tiles.Length; i++)
        {
            tiles[i].gameObject.transform.position = new Vector3(tiles[i-1].renderer.bounds.center.x + tiles[i - 1].renderer.bounds.extents.x*2, 0, 0);
        }
    }
    void MoveFirstToLast()
    {
        tiles[0].gameObject.transform.position = new Vector3(tiles[2].renderer.bounds.center.x + tiles[2].renderer.bounds.extents.x * 2, 0, 0);

        bgTile tempSwap = tiles[0];
        tiles[0] = tiles[1];
        tiles[1] = tiles[2];
        tiles[2] = tempSwap;


    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].gameObject.transform.position += new Vector3(scrollSpeed * Time.deltaTime,0,0);
        }

        if (tiles[0].gameObject.transform.position.x < PointToSwap)
        {
            MoveFirstToLast();
        }
    }
    public void OnDrawGizmos()
    {
        if(tiles!= null)
        {
            foreach(bgTile tile in tiles)
            {
                var bounds = tile.renderer.bounds;
                Gizmos.matrix = Matrix4x4.identity;
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
            }
        }

    }
}
