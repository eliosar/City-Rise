using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountsButtons : MonoBehaviour
{
    void Start()
    {
        if (GetComponentInParent<Amount>().Number > 1)
        {
            GetComponent<Text>().text = GetComponentInParent<Amount>().Number + "";
        }
    }
}