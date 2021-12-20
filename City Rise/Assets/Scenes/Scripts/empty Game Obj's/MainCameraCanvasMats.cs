using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraCanvasMats : MonoBehaviour
{
    private int[] Mats;
    private Text[] MatsTexts;

    private void Start()
    {
        Mats = new int[GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsAmount()];
        MatsTexts = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsTexts();
    }

    private void Update()
    {
        int y = 0;
        foreach (int mAmount in Mats)
        {
            Mats[y] = GetComponentInParent<MainCamera>().getMats(y);
            MatsTexts[y].GetComponent<Text>().text = Mats[y] + " " + GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts[y].name;

            y += 1;
        }
    }
}