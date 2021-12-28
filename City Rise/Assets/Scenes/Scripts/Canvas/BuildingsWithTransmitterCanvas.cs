using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsWithTransmitterCanvas : MonoBehaviour
{
    private GameObject Buildingsplaced;

    private void Start()
    {
        Buildingsplaced = transform.parent.parent.gameObject;
    }
    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = transform.parent.name + "\n\n" + GetComponentInParent<BuildingsWithTransmitter>().getcurrentPeopleInBuilding() + " / " + GetComponentInParent<BuildingsWithTransmitter>().maxPeopleInBuilding + " People in Building\n";

        for (int i = 0; i < GetComponentInParent<BuildingsWithTransmitter>().MatsTexts.Length; i++)
        {
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<BuildingsWithTransmitter>().getMatsAmount(i) + " / " + GetComponentInParent<BuildingsWithTransmitter>().getmaxMatsAmount() + " " + GetComponentInParent<BuildingsWithTransmitter>().MatsTexts[i].name + "\n";
        }
    }

    public void TransmittAll()
    {
        for (int i = 0; i < GetComponentInParent<BuildingsWithTransmitter>().MatsTexts.Length; i++)
        {
            ProductHandOver(GetComponentInParent<BuildingsWithTransmitter>().MatsTexts[i].name, i);
        }
    }

    private void ProductHandOver(string Name, int repeats)
    {
        GameObject gameObjTransmitter = GetComponentInParent<BuildingsWithTransmitter>().Transmitter;
        int TransmitterPlaced = Buildingsplaced.GetComponent<Buildings>().getTransmitterPlaced(gameObjTransmitter);
        GameObject[] Transmitter = Buildingsplaced.GetComponent<Buildings>().getTransmitter(gameObjTransmitter, TransmitterPlaced);
        GameObject MainCameraCanvas = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMainCameraCanvas();

        foreach (GameObject currentTransmitter in Transmitter)
        {
            int currentMatplace = currentTransmitter.GetComponent<JobBuildings>().getMatsPlace(Name);
            int currentProductAmount = GetComponentInParent<BuildingsWithTransmitter>().getMatsAmount(repeats);
            int diff = currentTransmitter.GetComponent<JobBuildings>().getmaxMatsAmount() - currentTransmitter.GetComponent<JobBuildings>().getMatsAmount(currentMatplace);

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

                Buildingsplaced.GetComponent<Buildings>().getrandomTransmitter(GetComponentInParent<BuildingsWithTransmitter>().Transmitter).GetComponent<JobBuildings>().addtransmittetMats(currentMatplace, Amount);
                GetComponentInParent<BuildingsWithTransmitter>().takeawayMatsAmount(repeats, Amount);
            }
        }
    }

    public void Exit()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().ExitBuildingsCanvas(gameObject);
    }

    public void Move()
    {
        Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().MoveBuilding(gameObject.transform.parent.gameObject);
        Exit();
    }
}