using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().getInGame())
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }
}
