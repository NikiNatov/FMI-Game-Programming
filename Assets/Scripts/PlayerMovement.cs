using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private GameObject camera;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float jumpForce = 40.0f;

    [SerializeField]
    private float movementSpeed = 10.0f;

    [SerializeField]
    private bool facingRight = true;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        HandleInput();

        // Make the camera and the background sprite follow the player
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
        background.transform.position = new Vector3(transform.position.x, transform.position.y, 1.0f);

        // If the player falls its position is being reset
        if (transform.position.y < -30.0f)
            transform.position = new Vector3(0.0f, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it is a moving platform make the player move with the platform
        if(collision.gameObject.name == "MovingPlatform")
            transform.parent = collision.gameObject.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the parent transform so that the player no longer moves with the platform
        if (collision.gameObject.name == "MovingPlatform")
            transform.parent = null;
    }

    private void HandleInput()
    {
        if (IsOnGround() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.velocity = Vector3.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);
            if (facingRight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                facingRight = false;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
            if (!facingRight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                facingRight = true;
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0.0f, Vector3.down, 0.1f, layerMask);
        return raycastHit2D.collider != null;
    }
}
