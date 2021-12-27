using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private StringandNumber.rowData[] TradesArray;

    private void Start()
    {
        TradesArray = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().Trades.Arrays;
    }

    public StringandNumber.rowData[] getTradesArray()
    {
        return TradesArray;
    }
}
