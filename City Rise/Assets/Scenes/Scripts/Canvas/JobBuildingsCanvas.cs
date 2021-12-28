using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobBuildingsCanvas : MonoBehaviour
{
    private GameObject Buildingsplaced;

    private void Start()
    {
        Buildingsplaced = transform.parent.parent.gameObject;
    }

    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = transform.parent.name + "\n\n" + GetComponentInParent<JobBuildings>().getcurrentPeopleInBuilding() + " / " + GetComponentInParent<JobBuildings>().maxPeopleInBuilding + " People in Building\n";

        for (int i = 0; i < GetComponentInParent<JobBuildings>().MatsTexts.Length; i++)
        {
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<JobBuildings>().getMatsAmount(i) + " / " + GetComponentInParent<JobBuildings>().getmaxMatsAmount() + " " + GetComponentInParent<JobBuildings>().MatsTexts[i].name + "\n";
        }
    }

    public void Exit()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().ExitBuildingsCanvas(gameObject);
    }

    public void collectAll()
    {
        for (int i = 0; i < GetComponentInParent<JobBuildings>().MatsTexts.Length; i++)
        {
            ProductHandOver(GetComponentInParent<JobBuildings>().MatsTexts[i].name, i);
        }
    }

    private void ProductHandOver(string Name, int repeats)
    {
        GameObject[] Storages = GetComponentInParent<Buildings>().getStorages();
        int currentMatplace = Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsPlace(Name);
        GameObject MainCameraCanvas = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMainCameraCanvas();

        foreach (GameObject currentStorage in Storages)
        {
            if (currentStorage != null)
            {
                int currentProductAmount = GetComponentInParent<JobBuildings>().getMatsAmount(repeats);
                int diff = currentStorage.GetComponent<storedMats>().getMaxMats() - currentStorage.GetComponent<storedMats>().getMats(currentMatplace);

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

                    Buildingsplaced.GetComponent<Buildings>().getrandomStorage().GetComponent<storedMats>().addMats(currentMatplace, Amount);
                    GetComponentInParent<JobBuildings>().takeawayMatsAmount(repeats, Amount);
                }
            }
        }
    }

    public void Move()
    {
        Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().MoveBuilding(gameObject.transform.parent.gameObject);
        Exit();
    }
}