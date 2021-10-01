using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private StringandNumber.rowData[] TradesArray;

    private void Start()
    {
        TradesArray = GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getTrades();
    }

    public StringandNumber.rowData[] getTradesArray()
    {
        return TradesArray;
    }
}
