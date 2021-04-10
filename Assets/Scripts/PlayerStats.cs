using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;
    public PickupBar pickupBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            TakeDamage();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            PickKey();
            GameObject.Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
            TakeDamage();
    }

    public void TakeDamage()
    {
        healthBar.TakeDamage();

        GetComponent<Animator>().SetBool("hurt", true);
        StartCoroutine(HurtRoutine());

        if (healthBar.hearts == 0)
            SceneManager.LoadScene("GameOver");
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("hurt", false);
    }

    public void PickKey()
    {
        pickupBar.PickKey();
    }

}
