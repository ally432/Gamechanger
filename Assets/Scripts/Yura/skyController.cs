using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float deadLine = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        if (transform.position.x > deadLine)
        {
            Destroy(gameObject);
        }
    }
}