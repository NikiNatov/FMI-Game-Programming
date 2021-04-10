using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupBar : MonoBehaviour
{
    public int numPickups = 0;

    public Sprite keyEnabled;
    public Sprite keyDisabled;

    public void PickKey()
    {
        Image key = transform.GetChild(numPickups).GetComponent<Image>();
        key.sprite = keyEnabled;
        numPickups++;
    }
}
