using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged HealthChangedEvent;

    public LayerMask        layerMask;
    public float            jumpForce = 40.0f;
    public bool             canDoubleJump = false;
    public float            movementSpeed = 10.0f;

    public GameObject       bulletPrefab;
    public HealthBar        healthBar;
    public PickupBar        pickupBar;
    public CameraShake      cameraShakeEffect;
    public BoxCollider2D    boxCollider;
    public Rigidbody2D      rigidBody;
    public SpriteRenderer   sprite;
    public GameObject       playerCamera;
    public Controls         controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Shoot.performed += ctx => Shoot();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();    
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        {
            float moveDirection = controls.Gameplay.Move.ReadValue<float>();
            Vector2 dir = new Vector2(moveDirection, 0) * movementSpeed * Time.fixedDeltaTime;
            transform.Translate(dir, Space.World);

            if (moveDirection < 0)
                transform.rotation = Quaternion.Euler(new Vector3(0, 180));
            else
                transform.rotation = Quaternion.Euler(new Vector3(0, 0));

            // If the player falls its position is being reset
            if (transform.position.y < -10.0f)
                SceneManager.LoadScene("GameOver");

            float speed = Mathf.Abs(moveDirection * movementSpeed / 8.0f);
            GetComponent<Animator>().SetFloat("speed", speed, 0.1f, Time.deltaTime);
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it is a moving platform make the player move with the platform
        if (collision.gameObject.tag == "Pickup")
        {
            PickKey();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
            TakeDamage();
    }

    private bool IsOnGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0.0f, Vector3.down, 0.1f, layerMask);
        return raycastHit2D.collider != null;
    }

    public void Jump()
    {

            if (IsOnGround())
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

    
    public void TakeDamage()
    {
        healthBar.TakeDamage();

        GetComponent<Animator>().SetBool("hurt", true);
        StartCoroutine(HurtRoutine());

        if (healthBar.hearts == 0)
            SceneManager.LoadScene("GameOver");

        HealthChangedEvent?.Invoke(healthBar.hearts);
        StartCoroutine(cameraShakeEffect.Shake(0.15f, 0.3f));
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("hurt", false);
    }

    public void PickKey()
    {
        pickupBar.PickKey();

        if (pickupBar.numPickups == 3)
            SceneManager.LoadScene("WinScene");
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
