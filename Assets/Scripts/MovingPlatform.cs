using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 5.0f;

    private bool moveRight = false;
    private float maxDistance = 6.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, startPos) >= maxDistance && moveRight)
            moveRight = false;
        else if (Vector3.Distance(transform.position, startPos) >= maxDistance && !moveRight)
            moveRight = true;

        if(moveRight)
            transform.position = new Vector3(transform.position.x + movementSpeed * Time.fixedDeltaTime, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x - movementSpeed * Time.fixedDeltaTime, transform.position.y);
    }
}
