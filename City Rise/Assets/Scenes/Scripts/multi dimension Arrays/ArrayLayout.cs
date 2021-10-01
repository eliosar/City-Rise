using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrayLayout
{
    [System.Serializable]
    public struct rowData
    {
        public int[] ProbabilityA;
        public int[] MatsP;
    }

    public rowData[] Mats;
}