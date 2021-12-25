using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choosedBuildingPlace : MonoBehaviour
{
    private bool Terrainhitted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain (1)")
        {
            Terrainhitted = true;
        }
        else
        {
            Terrainhitted = false;
        }
    }
}