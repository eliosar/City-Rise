using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costs : MonoBehaviour
{
    public int Rubin;
    private GameObject MainCameraCanvas;
    private GameObject MainCamera;

    private void Start()
    {
        MainCameraCanvas = gameObject;

        while(MainCameraCanvas.name != "Main Camera Canvas")
        {
            MainCameraCanvas = MainCameraCanvas.transform.parent.gameObject;
        }

        MainCamera = MainCameraCanvas.GetComponent<getMainCamera>().mainCamera;
    }
}
