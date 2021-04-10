using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public float jumpForce = 40.0f;
    public bool canDoubleJump = false;

    [SerializeField]
    private float movementSpeed = 10.0f;

    [SerializeField]
    private bool facingRight = true;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        HandleInput();

        // If the player falls its position is being reset
        if (transform.position.y < -10.0f)
            SceneManager.LoadScene("GameOver");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it is a moving platform make the player move with the platform
        if (collision.gameObject.tag == "Platform")
            transform.parent = collision.gameObject.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the parent transform so that the player no longer moves with the platform
        if (collision.gameObject.name == "Platform")
            transform.parent = null;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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

        float speed = rigidBody.velocity.magnitude / 8.0f;
        GetComponent<Animator>().SetFloat("speed", speed, 0.1f, Time.deltaTime);
    }   

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0.0f, Vector3.down, 0.1f, layerMask);
        return raycastHit2D.collider != null;
    }

    public void Jump()
    {
        if(IsOnGround())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.velocity = new Vector2(0, jumpForce);
            canDoubleJump = true;
        }
        else
        {
            if (canDoubleJump)
            {
                canDoubleJump = false;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
                rigidBody.velocity = new Vector2(0, jumpForce);
            }
        }

        GetComponent<Animator>().SetBool("jump", true);
        StartCoroutine(JumpRoutine());
    }

    IEnumerator JumpRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("jump", false);
    }
}
