using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    public StringandNumber.rowData[] TradesArray;

    private void Start()
    {
        TradesArray = GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getTrades();
    }
}
