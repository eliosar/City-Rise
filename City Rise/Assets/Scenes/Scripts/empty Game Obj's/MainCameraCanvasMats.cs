using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraCanvasMats : MonoBehaviour
{
    private int[] Mats;

    private void Start()
    {
        Mats = new int[GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsAmount()];
    }

    private void Update()
    {
        int y = 0;
        foreach (int mAmount in Mats)
        {
            Mats[y] = GetComponentInParent<MainCamera>().getMats(y);
            transform.GetChild(y).GetComponent<Text>().text = Mats[y] + " " + GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsNames[y];

            y += 1;
        }
    }
}