using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    private int[] Mats;
    private GameObject mainCamera;
    private GameObject ButtonsFolder;
    private GameObject costingBuildingsButton;
    private GameObject freeBuildingsButton;
    private GameObject Canvas;

    private void Start()
    {
        mainCamera = GetComponent<getMainCamera>().mainCamera;
        
        Mats = new int[mainCamera.GetComponent<Main>().getMatsAmount()];

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Child = transform.GetChild(i).gameObject;
            
            if(Child.name == "Buttons")
            {
                ButtonsFolder = Child;
                for (int x = 0; x < Child.transform.childCount; x++)
                {
                    GameObject ButtonChild = Child.transform.GetChild(x).gameObject;
                    if (ButtonChild.name == "costing Buildings")
                    {
                        costingBuildingsButton = ButtonChild;
                    }

                    if(ButtonChild.name == "free Buildings")
                    {
                        freeBuildingsButton = ButtonChild;
                    }
                }
            }
        }
    }

    private void Update()
    {
        mainCamera = GetComponent<getMainCamera>().mainCamera;
        int currentBuildings = mainCamera.GetComponent<Main>().Buildingsplaced.transform.childCount;
        int currentStoragesplaced = 0;

        for (int i = 0; i < currentBuildings; i++)
        {
            string Buildings = mainCamera.GetComponent<Main>().Buildingsplaced.transform.GetChild(i).name;

            if(Buildings == "Storage")
            {
                currentStoragesplaced += 1;
            }
        }

        if (mainCamera.GetComponent<Main>().getStoragesplaced() > 0)
        {
            GameObject Buildings = mainCamera.GetComponent<Main>().Buildingsplaced;

            for (int i = 0; i < mainCamera.GetComponent<Main>().getMatsAmount(); i++)
            {
                Mats[i] = 0;
            }

            for (int i = 0; i < mainCamera.GetComponent<Main>().getStoragesplaced(); i++)
            {
                for (int x = 0; x < mainCamera.GetComponent<Main>().getMatsAmount(); x++)
                {
                    Mats[x] += Buildings.GetComponent<Buildings>().getStorage(i).GetComponent<storedMats>().getMats(x);
                }
            }
        }
    }

    public void costingBuildings()
    {
        InstantiateCanvas(mainCamera.GetComponent<Main>().getcostingBuildingsCanvas(), costingBuildingsButton);
    }

    public void freeBuildings()
    {
        InstantiateCanvas(mainCamera.GetComponent<Main>().getfreeBuildingsCanvas(), freeBuildingsButton);
    }

    private void InstantiateCanvas(GameObject whichCanvas, GameObject whichButton)
    {
        for (int i = 0; i < ButtonsFolder.transform.childCount; i++)
        {
            GameObject Button = ButtonsFolder.transform.GetChild(i).gameObject;

            if(Button.GetComponent<Button>().interactable == false && Button != whichButton)
            {
                Button.GetComponent<Button>().interactable = true;
            }

            for (int x = 0; x < ButtonsFolder.transform.childCount; x++)
            {
                if (ButtonsFolder.transform.GetChild(x).childCount > 0)
                {
                    Destroy(ButtonsFolder.transform.GetChild(x).GetChild(0));
                }
            }
        }

        if (mainCamera.GetComponent<choosedObj>().getBuildingInstanced())
        {
            Destroy(mainCamera.GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).gameObject);
            mainCamera.GetComponent<choosedObj>().setBuildingInstanced(false);
        }
        mainCamera.GetComponent<choosedObj>().setinGame(false);
        mainCamera.GetComponent<Main>().setchoosedBuilding(null);
        mainCamera.GetComponent<choosedObj>().setisfreeBuilding(false);

        Canvas = Instantiate(whichCanvas);
        Canvas.transform.position = transform.position;
        Canvas.transform.SetParent(whichButton.transform);
        Canvas.name = whichCanvas.name;
        whichButton.GetComponent<Button>().interactable = false;
    }

    public GameObject getfreeBuildingsButton()
    {
        return freeBuildingsButton;
    }

    public GameObject getcostingBuildingsButton()
    {
        return costingBuildingsButton;
    }

    public GameObject getrandomStorage()
    {
        return mainCamera.GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().getStorage(RandomNumber(mainCamera.GetComponent<Main>().getStoragesplaced()));
    }

    public GameObject getrandomTransmitter(GameObject Transmitter)
    {
        return mainCamera.GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().getTransmitter(Transmitter, RandomNumber(mainCamera.GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().getTransmitterPlaced(Transmitter)));
    }

    public int RandomNumber(int AmountofBuilding)
    {
        int rand = 0;

        if (AmountofBuilding > 1)
        {
            rand = Random.Range(0, AmountofBuilding);
        }

        return rand;
    }

    public int getMats(int which)
    {
        return Mats[which];
    }

    public int[] getMats()
    {
        return Mats;
    }
}