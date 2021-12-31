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
    private GameObject RubinsText;

    private void Start()
    {
        mainCamera = GetComponent<getMainCamera>().mainCamera;
        
        Mats = new int[mainCamera.GetComponent<Main>().MatsTexts.Length];

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Child = transform.GetChild(i).gameObject;

            if (Child.name == "Buttons")
            {
                ButtonsFolder = Child;
                for (int x = 0; x < Child.transform.childCount; x++)
                {
                    GameObject Button = Child.transform.GetChild(x).gameObject;
                    if (Button.name == "costing Buildings")
                    {
                        costingBuildingsButton = Button;
                    }

                    if (Button.name == "free Buildings")
                    {
                        freeBuildingsButton = Button;
                    }
                }
            }
        }

        costingBuildingsButton.GetComponent<Button>().onClick.AddListener(costingBuildings);
        freeBuildingsButton.GetComponent<Button>().onClick.AddListener(freeBuildings);
    }

    private void Update()
    {
        mainCamera = GetComponent<getMainCamera>().mainCamera;
        GameObject Buildingsplaced = mainCamera.GetComponent<Main>().Buildingsplaced;

        if (Buildingsplaced.GetComponent<Buildings>().getStorages().Length > 0)
        {
            for (int i = 0; i < mainCamera.GetComponent<Main>().MatsTexts.Length; i++)
            {
                Mats[i] = 0;
            }

            for (int i = 0; i < Buildingsplaced.GetComponent<Buildings>().getStoragesplaced(); i++)
            {
                for (int x = 0; x < mainCamera.GetComponent<Main>().MatsTexts.Length; x++)
                {
                    Mats[x] += Buildingsplaced.GetComponent<Buildings>().getStorage(i).GetComponent<storedMats>().getMats(x);
                }
            }
        }
    }

    public void costingBuildings()
    {
        InstantiateCanvas(mainCamera.GetComponent<Main>().getcostingBuildingsCanvas());
    }

    public void freeBuildings()
    {
        InstantiateCanvas(mainCamera.GetComponent<Main>().getfreeBuildingsCanvas());
    }

    private void InstantiateCanvas(GameObject whichCanvas)
    {
        GameObject whichButton = null;
        for (int i = 0; i < ButtonsFolder.transform.childCount; i++)
        {
            GameObject Child = ButtonsFolder.transform.GetChild(i).gameObject;
            Child.GetComponent<Button>().interactable = false;
            if (Child.transform.childCount > 0)
            {
                Destroy(ButtonsFolder.transform.GetChild(i).GetChild(0));
            }
            if(Child.name + " Canvas" == whichCanvas.name)
            {
                whichButton = Child;
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

        GameObject Canvas = Instantiate(whichCanvas);
        Canvas.transform.position = transform.position;
        Canvas.transform.SetParent(whichButton.transform);
        Canvas.name = whichCanvas.name;
    }

    public GameObject getfreeBuildingsButton()
    {
        return freeBuildingsButton;
    }

    public GameObject getcostingBuildingsButton()
    {
        return costingBuildingsButton;
    }

    public int getMats(int which)
    {
        return Mats[which];
    }

    public int[] getMats()
    {
        return Mats;
    }

    public GameObject getButtonsFolder()
    {
        return ButtonsFolder;
    }
}