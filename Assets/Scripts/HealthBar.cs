using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int hearts = 3;

    public Sprite heartDisabled;
    public Sprite heartEnabled;

    public void TakeDamage()
    {
        hearts--;
        Image heart = transform.GetChild(hearts).GetComponent<Image>();
        heart.sprite = heartDisabled;
    }
}
