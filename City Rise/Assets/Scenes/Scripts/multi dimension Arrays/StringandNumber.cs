using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StringandNumber
{
    [System.Serializable]
    public struct rowData
    {
        public string Name;
        public int Costs;
        public int Amount;
    }

    public rowData[] Arrays;
}