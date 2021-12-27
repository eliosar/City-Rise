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

        for (int i = 0; i < GetComponentInParent<JobBuildings>().getMatsAmount(); i++)
        {
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<JobBuildings>().getcurrentProductAmount(i) + " / " + GetComponentInParent<JobBuildings>().getmaxProductAmount() + " " + GetComponentInParent<JobBuildings>().MatsName[i] + "\n";
        }
    }

    public void Exit()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().ExitBuildingsCanvas(gameObject);
    }

    public void collectAll()
    {
        foreach (string Name in GetComponentInParent<JobBuildings>().MatsName)
        {
            ProductHandOver(Name);
        }
    }

    private void ProductHandOver(string Name)
    {
        GameObject[] Storages = GetComponentInParent<Buildings>().getStorages();
        int currentMatplace = Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsPlace(Name);
        int i = 0;
        int different = 0;
        GameObject MainCameraCanvas = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMainCameraCanvas();

        foreach (Text Mat in MainCameraCanvas.GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts)
        {
            if (Mat.name == GetComponentInParent<JobBuildings>().MatsName[0])
            {
                different = i;
                break;
            }

            i += 1;
        }

        foreach (GameObject currentStorage in Storages)
        {
            if (currentStorage != null)
            {
                int currentProductAmount = GetComponentInParent<JobBuildings>().getcurrentProductAmount(currentMatplace - different);
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
                    GetComponentInParent<JobBuildings>().takeawaycurrentProductamount(currentMatplace - different, Amount);
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