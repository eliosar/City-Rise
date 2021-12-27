using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraCanvasMats : MonoBehaviour
{
    private Text[] MatsTexts;

    private void Start()
    {
        MatsTexts = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsTexts();
    }

    private void Update()
    {
        int[] Mats = GetComponentInParent<MainCamera>().getMats();

        for (int i = 0; i < MatsTexts.Length; i++)
        {
            MatsTexts[i].GetComponent<Text>().text = Mats[i] + " " + MatsTexts[i].name;
        }
    }
}