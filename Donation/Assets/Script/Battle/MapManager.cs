using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Camera Camera;
    public float width;
    public float height;
    public GameObject tile;
    public Tilemap test;

    // Start is called before the first frame update
    void Awake()
    {
        MapGenerator(Camera.transform.position);
    }

    // Update is called once per frameheight
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.ScreenToWorldPoint(Input.mousePosition);
            MapGenerator(point);
            //test.transform.position = new Vector3(point.x, point.y, transform.position.z);
            Tilemap test2 = Tilemap.Instantiate(test);
            test2.transform.position = new Vector3(point.x, point.y, transform.position.z);
        }
    }

    void MapGenerator(Vector3 vec)
    {
        Debug.Log(vec);
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++) 
            {
                GameObject initTile = Instantiate(tile);
                initTile.transform.position = new Vector3(i, j, 0);
            }
        }
    }
}
