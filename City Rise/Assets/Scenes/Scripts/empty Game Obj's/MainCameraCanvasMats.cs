using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraCanvasMats : MonoBehaviour
{
    private Text[] MatsTexts;
    public Text Rubins;
    int RubinPlace;

    private void Start()
    {
        MatsTexts = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts;
        RubinPlace = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsPlace(Rubins.name);
    }

    private void Update()
    {
        int[] Mats = GetComponentInParent<MainCamera>().getMats();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = Mats[i] + " " + MatsTexts[i].name;
        }
        
        Rubins.GetComponent<Text>().text = Mats[RubinPlace] + " " + MatsTexts[RubinPlace].name;
    }
}