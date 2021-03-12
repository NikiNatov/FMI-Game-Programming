using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 5.0f;

    private bool moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 6.0f)
            moveRight = true;
        if (transform.position.x > 12.0f)
            moveRight = false;

        if(moveRight)
            transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
    }
}
