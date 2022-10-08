using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOption : MonoBehaviour
{
    Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = Camera.transform.position - transform.position;
        if (Mathf.Abs(distance.x) > 36 || Mathf.Abs(distance.y) > 36)
        {
            Destroy(gameObject);
        }
    }
}
