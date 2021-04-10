using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector3 initialPosition;

    public float respawnTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Invoke("DropPlatform", 0.1f);
            Invoke("RespawnPlatform", respawnTime);
        }
    }

    private void DropPlatform()
    {
        rigidBody.isKinematic = false;
    }

    private void RespawnPlatform()
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
        transform.position = initialPosition;
    }    
}
