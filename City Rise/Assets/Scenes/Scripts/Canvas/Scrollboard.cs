using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollboard : MonoBehaviour
{
    private GameObject Buildings;
    private GameObject MainCameraCanvas;
    private RawImage BuildingButton;

    private void Start()
    {
        MainCameraCanvas = transform.parent.parent.parent.gameObject;
        Buildings = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject;

        if(gameObject.name == "free Buildings Canvas")
        {
            foreach (StringandNumber.rowData Array in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getBuildingArrays())
            {
                if(Array.Amount > 0)
                {
                    foreach (RawImage Button in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().BuildingsButtons)
                    {
                        if(Button.name == Array.Name)
                        {
                            BuildingButton = Instantiate(Button);
                            BuildingButton.transform.SetParent(Buildings.transform);
                            BuildingButton.name = Button.name;
                            BuildingButton.GetComponent<Amount>().setNumber(Array.Amount);
                            BuildingButton.GetComponent<Button>().onClick.AddListener(BuildingButton.GetComponent<Amount>().choosedBuilding);
                            BuildingButton.GetComponent<Button>().onClick.AddListener(Exit);
                        }
                    }
                }
            }
        }
        if (gameObject.name == "costing Buildings Canvas")
        {
            foreach (StringandNumber.rowData Array in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getBuildingArrays())
            {
                foreach (RawImage Button in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().BuildingsButtons)
                {
                    if (Button.name == Array.Name)
                    {
                        BuildingButton = Instantiate(Button);
                        BuildingButton.transform.SetParent(Buildings.transform);
                        BuildingButton.name = Button.name;
                        BuildingButton.GetComponent<Amount>().setCosts(Array.Costs);
                        BuildingButton.GetComponent<Button>().onClick.AddListener(BuildingButton.GetComponent<Amount>().choosedBuilding);
                        BuildingButton.GetComponent<Button>().onClick.AddListener(Exit);
                        BuildingButton.GetComponent<Button>().onClick.AddListener(BuildingButton.GetComponent<Amount>().buyBuildings);

                        if(MainCameraCanvas.GetComponent<MainCamera>().getMats(MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsPlace("Rubins")) < BuildingButton.GetComponent<Costs>().Rubin)
                        {
                            BuildingButton.GetComponent<Button>().interactable = false;
                        }
                    }
                }
            }
            if (GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().getStoragesplaced() >= 4)
            {
                for (int i = 0; i < Buildings.transform.childCount; i++)
                {
                    if (Buildings.transform.GetChild(i).name == "Storage")
                    {
                        Buildings.transform.GetChild(1).GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
    }

    public void Exit()
    {
        if (gameObject.name == "free Buildings Canvas")
        {
            GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setisfreeBuilding(true);
        }

        MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().Exitnormal(gameObject);
    }
}