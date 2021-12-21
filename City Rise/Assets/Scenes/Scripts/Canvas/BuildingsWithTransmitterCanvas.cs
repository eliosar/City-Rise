using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsWithTransmitterCanvas : MonoBehaviour
{
    private GameObject MainCameraCanvas;
    private GameObject Buildingsplaced;

    private void Start()
    {
        Buildingsplaced = transform.parent.parent.gameObject;
        MainCameraCanvas = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMainCameraCanvas();
    }
    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = transform.parent.name + "\n\n" + GetComponentInParent<BuildingsWithTransmitter>().getcurrentPeopleInBuilding() + " / " + GetComponentInParent<BuildingsWithTransmitter>().maxPeopleInBuilding + " People in Building\n";

        for (int i = 0; i < GetComponentInParent<BuildingsWithTransmitter>().getMatsAmount(); i++)
        {
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<BuildingsWithTransmitter>().getMat(i) + " / " + GetComponentInParent<BuildingsWithTransmitter>().getmaxMatsAmount() + " " + GetComponentInParent<BuildingsWithTransmitter>().MatsName[i] + "\n";
        }
    }

    public void TransmittAll()
    {
        foreach (string Name in GetComponentInParent<BuildingsWithTransmitter>().MatsName)
        {
            ProductHandOver(Name);
        }
    }

    private void ProductHandOver(string Name1)
    {
        GameObject[] Transmitter = GetComponentInParent<Buildings>().getTransmitter(GetComponentInParent<BuildingsWithTransmitter>().Transmitter);
        Debug.Log("choosed Transmitter name: " + GetComponentInParent<BuildingsWithTransmitter>().Transmitter.name);
        Debug.Log("all choosed Transmitter: " + GetComponentInParent<Buildings>().getTransmitter(GetComponentInParent<BuildingsWithTransmitter>().Transmitter));
        Debug.Log("first choosed Transmitter: " + GetComponentInParent<Buildings>().getTransmitter(GetComponentInParent<BuildingsWithTransmitter>().Transmitter, 0));
        Debug.Log("own Transmitter: " + Transmitter);
        int i = 0;
        int y = 0;
        int different = 0;
        GameObject MainCameraCanvas = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMainCameraCanvas();
        int currentMatplace = Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsPlace(Name1);

        foreach (Text Mat in MainCameraCanvas.GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts)
        {
            if (Mat.name == Name1)
            {
                y = i;
            }

            if (Mat.name == GetComponentInParent<BuildingsWithTransmitter>().MatsName[0])
            {
                different = i;
            }

            
            i += 1;
        }

        int ymindiff = y - different;

        foreach (GameObject currentTransmitter in Transmitter)
        {
            if (currentTransmitter != null)
            {
                int currentProductAmount = GetComponentInParent<BuildingsWithTransmitter>().getMat(ymindiff);
                Debug.Log("Mat amount: " + GetComponentInParent<BuildingsWithTransmitter>().getMat(ymindiff));
                int diff = currentTransmitter.GetComponent<storedMats>().getMaxMats() - currentTransmitter.GetComponent<storedMats>().getMats(currentMatplace);

                if (diff > 0)
                {
                    int Amount = 0;
                    if (diff > currentProductAmount)
                    {
                        Amount = currentProductAmount;
                    }
                    else
                    {
                        Amount = diff;
                    }

                    Buildingsplaced.GetComponent<Buildings>().getrandomTransmitter(GetComponentInParent<BuildingsWithTransmitter>().Transmitter).GetComponent<JobBuildings>().addtransmittetMats(y, Amount);
                    Debug.Log("Take: " + Amount);
                    GetComponentInParent<BuildingsWithTransmitter>().takeawayMatsamount(ymindiff, Amount);
                }
            }
        }
    }

    public void Exit()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().ExitBuildingsCanvas(gameObject);
    }
}