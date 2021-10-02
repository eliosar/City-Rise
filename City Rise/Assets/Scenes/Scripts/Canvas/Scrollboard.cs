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
            foreach (StringandNumber.rowData Array in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getfreeBuildingArrays())
            {
                if(Array.Amount > 0)
                {
                    foreach (RawImage Button in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().freeBuildingsButtons)
                    {
                        if(Button.name == Array.Name)
                        {
                            BuildingButton = Instantiate(Button);
                            BuildingButton.transform.SetParent(Buildings.transform);
                            BuildingButton.name = Button.name;
                            BuildingButton.GetComponent<Amount>().Number = Array.Amount;
                            choosing();
                            BuildingButton.GetComponent<Button>().onClick.AddListener(ExitfromfreeBuildingsCanvas);
                        }
                    }
                }
            }
        }
        if (gameObject.name == "costing Buildings Canvas")
        {
            foreach (RawImage Button in MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().costingBuildingsButtons)
            {
                BuildingButton = Instantiate(Button);
                BuildingButton.transform.SetParent(Buildings.transform);
                BuildingButton.name = Button.name;
                choosing();
                BuildingButton.GetComponent<Button>().onClick.AddListener(ExitfromcostingBuildingsCanvas);
                BuildingButton.GetComponent<Button>().onClick.AddListener(BuildingButton.GetComponent<Costs>().buy);
            }
        }
    }

    private void Update()
    {
        if (gameObject.name == "costingBuildingsCanvas")
        {
            if (GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getStoragesplaced() >= 4)
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

    public void ExitfromcostingBuildingsCanvas()
    {
        Destroy(gameObject);

        MainCameraCanvas.GetComponentInParent<MainCamera>().getcostingBuildingsButton().GetComponent<Button>().interactable = true;
        MainCameraCanvas.GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setinGame(true);
    }

    public void ExitfromfreeBuildingsCanvas()
    {
        Destroy(gameObject);

        MainCameraCanvas.GetComponent<MainCamera>().getfreeBuildingsButton().GetComponent<Button>().interactable = true;
        MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setinGame(true);
    }

    private void choosing()
    {
        if(BuildingButton.name == "Lumberjack")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedLumberjack);
        }
        if (BuildingButton.name == "House")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedHouse);
        }
        if (BuildingButton.name == "Storage")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedStorage);
        }
        if (BuildingButton.name == "Mine")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedMine);
        }
        if (BuildingButton.name == "Smelter")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedSmelter);
        }
        if (BuildingButton.name == "Barn")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedBarn);
        }
        if (BuildingButton.name == "Butcher")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedButcher);
        }
        if (BuildingButton.name == "Fisher")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedFisher);
        }
        if (BuildingButton.name == "Farmer")
        {
            BuildingButton.GetComponent<Button>().onClick.AddListener(choosedFarmer);
        }
    }

    public void choosedLumberjack()
    {
        choosedBuilding("Lumberjack");
    }

    public void choosedHouse()
    {
        choosedBuilding("House");
    }

    public void choosedStorage()
    {
        choosedBuilding("Storage");
    }

    public void choosedMine()
    {
        choosedBuilding("Mine");
    }

    public void choosedSmelter()
    {
        choosedBuilding("Smelter");
    }

    public void choosedBarn()
    {
        choosedBuilding("Barn");
    }

    public void choosedButcher()
    {
        choosedBuilding("Butcher");
    }

    public void choosedFisher()
    {
        choosedBuilding("Fisher");
    }

    public void choosedFarmer()
    {
        choosedBuilding("Farmer");
    }

    private void choosedBuilding(string what)
    {
        if (gameObject.name == "free Buildings Canvas")
        {
            GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setisfreeBuilding(true);
        }

        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().setchoosedBuilding(what);
    }
}