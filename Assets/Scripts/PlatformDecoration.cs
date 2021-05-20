using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDecoration : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject spawnLeft;
    public GameObject spawnRight;

    // Start is called before the first frame update
    void Start()
    {
        spawnLeft.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
        spawnRight.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        spawnLeft.transform.localScale = spawnLeft.transform.localScale;
        spawnRight.transform.localScale = spawnRight.transform.localScale;
    }
}
