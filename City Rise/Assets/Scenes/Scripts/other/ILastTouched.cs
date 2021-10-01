using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILastTouched : MonoBehaviour
{
    public Collider ILastEntered;
    public Collider ILastExited;

    private void OnTriggerEnter(Collider other)
    {
        ILastEntered = other;
    }

    private void OnTriggerExit(Collider other)
    {
        ILastExited = other;
    }
}