using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    public float smoothSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed * Time.fixedDeltaTime);
    }
}
