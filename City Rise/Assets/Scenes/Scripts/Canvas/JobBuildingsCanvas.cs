using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobBuildingsCanvas : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = transform.parent.name + "\n\n" + GetComponentInParent<JobBuildings>().getcurrentPeopleInBuilding() + " / " + GetComponentInParent<JobBuildings>().maxPeopleInBuilding + " Peoples in Building\n";

        for (int i = 0; i < GetComponentInParent<JobBuildings>().getMatsAmount(); i++)
        {
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<JobBuildings>().getcurrentProductAmount(i) + " / " + GetComponentInParent<JobBuildings>().getmaxProductAmount() + " " + GetComponentInParent<JobBuildings>().MatsName[i] + "\n";
        }
    }

    public void Exit()
    {
        Destroy(gameObject);

        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setIsBuildingsCanvasOn(false);
    }

    public void collectAll()
    {
        foreach (string Name in GetComponentInParent<JobBuildings>().MatsName)
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
        GameObject MainCameraCanvas = GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMainCameraCanvas();

        foreach (string Name in MainCameraCanvas.GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsNames)
        {
            if (Name == Name1)
            {
                y = i;
            }

            if (Name == GetComponentInParent<JobBuildings>().MatsName[0])
            {
                different = i;
            }

            i += 1;
        }

        ymindiff = y - different;
        currentProductAmount = GetComponentInParent<JobBuildings>().getcurrentProductAmount(ymindiff);
        transform.parent.parent.GetComponent<Buildings>().getrandomStorage().GetComponent<storedMats>().addMats(y, currentProductAmount);
        GetComponentInParent<JobBuildings>().takeawaycurrentProductamount(ymindiff, currentProductAmount);
    }
}