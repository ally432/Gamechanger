using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sky;
    public float spawnRate = 120;
    public float timer = 0;
    public float defaultHeight = 3;
    // Start is called before the first frame update
    void Start()
    {
        spawnSky();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnSky();
            timer = 0;
        }
    }

    void spawnSky()
    {

        Instantiate(sky, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
    }
}
