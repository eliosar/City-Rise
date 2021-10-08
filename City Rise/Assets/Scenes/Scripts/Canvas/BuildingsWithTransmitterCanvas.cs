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
        transform.GetChild(0).GetComponent<Text>().text = transform.parent.name + "\n\n" + GetComponentInParent<BuildingsWithTransmitter>().getcurrentPeopleInBuilding() + " / " + GetComponentInParent<BuildingsWithTransmitter>().maxPeopleInBuilding + " Peoples in Building\n";

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
        int y = 0;
        int i = 0;
        int different = 0;
        int ymindiff = 0;
        int currentProductAmount = 0;

        foreach (string Name in GetComponentInParent<BuildingsWithTransmitter>().Transmitter.GetComponent<JobBuildings>().MatsName)
        {
            if (Name == Name1)
            {
                y = i;
            }

            if (Name == GetComponentInParent<BuildingsWithTransmitter>().MatsName[0])
            {
                different = i;
            }

            i += 1;
        }

        ymindiff = y - different;
        currentProductAmount = GetComponentInParent<BuildingsWithTransmitter>().getMat(ymindiff);
        Buildingsplaced.GetComponent<Buildings>().getrandomTransmitter(GetComponentInParent<BuildingsWithTransmitter>().Transmitter).GetComponent<JobBuildings>().addtransmittetMats(y, currentProductAmount);
        GetComponentInParent<BuildingsWithTransmitter>().takeawayMatsamount(ymindiff, currentProductAmount);
    }

    public void Exit()
    {
        Destroy(gameObject);

        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setIsBuildingsCanvasOn(false);
    }
}