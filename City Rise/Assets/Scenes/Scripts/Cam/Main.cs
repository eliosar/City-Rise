using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Main : MonoBehaviour
{
    public Text[] MatsTexts;
    public RawImage[] MatsButtons;
    public GameObject[] BuildingsWithBigCanvasWithoutButton;
    public GameObject[] Canvas;
    public GameObject[] JobBuildings;
    public GameObject[] BuildingsWithTransmitter;
    public GameObject[] otherBuildings;
    public RawImage[] BuildingsButtons;
    public GameObject ChoosedBuildingPlace;
    public GameObject Buildingsplaced;
    public GameObject Terrain;
    private string choosedBuilding;
    private StringandNumber Buildings;
    public StringandNumber Trades;
    private GameObject MainCameraCanvas;
    private GameObject JobBuildingsCanvas;
    private GameObject BuildingsWithTransmitterCanvas;
    private GameObject costingBuildingsCanvas;
    private GameObject freeBuildingsCanvas;
    private GameObject[] allBuildings;
    private GameObject[] otherBuildingsCanvas;

    private void Start()
    {
        Terrain.GetComponent<MeshRenderer>().material.color = Color.black;
        otherBuildingsCanvas = new GameObject[otherBuildings.Length];
        allBuildings = new GameObject[JobBuildings.Length + BuildingsWithTransmitter.Length + otherBuildings.Length];

        for (int x = 0; x < JobBuildings.Length; x++)
        {
            allBuildings[x] = JobBuildings[x];
        }

        for (int x = 0; x < BuildingsWithTransmitter.Length; x++)
        {
            allBuildings[x + JobBuildings.Length] = BuildingsWithTransmitter[x];
        }

        for (int x = 0; x < otherBuildings.Length; x++)
        {
            allBuildings[x + JobBuildings.Length + BuildingsWithTransmitter.Length] = otherBuildings[x];
        }

        Buildings.Arrays = new StringandNumber.rowData[allBuildings.Length];

        for (int i = 0; i < Buildings.Arrays.Length; i++)
        {
            Buildings.Arrays[i].Name = allBuildings[i].name;
        }

        for (int i = 0; i < Buildings.Arrays.Length; i++)
        {
            if (Buildings.Arrays[i].Name == "Storage" || Buildings.Arrays[i].Name == "House" || Buildings.Arrays[i].Name == "Lumberjack")
            {
                Buildings.Arrays[i].Amount = 1;
            }
        }

        int currentotherBuildingCanvas = 0;
        foreach (GameObject Canvas in Canvas)
        {
            if (Canvas.name == "Main Camera Canvas")
            {
                MainCameraCanvas = Canvas;
            }
            if (Canvas.name == "Job Buildings Canvas")
            {
                JobBuildingsCanvas = Canvas;
            }
            if (Canvas.name == "Buildings With Transmitter Canvas")
            {
                BuildingsWithTransmitterCanvas = Canvas;
            }
            if (Canvas.name == "costing Buildings Canvas")
            {
                costingBuildingsCanvas = Canvas;
            }
            if (Canvas.name == "free Buildings Canvas")
            {
                freeBuildingsCanvas = Canvas;
            }

            foreach (GameObject otherBuilding in otherBuildings)
            {
                if(Canvas.name == otherBuilding.name + " Canvas")
                {
                    otherBuildingsCanvas[currentotherBuildingCanvas] = Canvas;
                    currentotherBuildingCanvas += 1;
                }
            }
        }

        string path = Application.persistentDataPath + "/Data.save";
        if (File.Exists(path))
        {
            Data data = SceneLoader.Load();
            Buildings.Arrays = data.getallfreeBuildings();
            transform.position = new Vector3(data.getCamPos()[0], data.getCamPos()[1], data.getCamPos()[2]);

            for (int i = 0; i < data.getallBuildingsRotation().Length; i++)
            {
                if (data.getallBuildingsName()[i] != null)
                {
                    GameObject currentBuilding = Instantiate(getBuilding(data.getallBuildingsName()[i]), Buildingsplaced.transform);
                    currentBuilding.name = data.getallBuildingsName()[i];
                    currentBuilding.transform.position = new Vector3(data.getallBuildingsPos()[i, 0], data.getallBuildingsPos()[i, 1], data.getallBuildingsPos()[i, 2]);
                    currentBuilding.transform.eulerAngles = new Vector3(data.getallBuildingsRotation()[i], 0, 0);
                    currentBuilding.transform.localScale = new Vector3(2, 2, 2);

                    if (currentBuilding.name == "Storage")
                    {
                        currentBuilding.GetComponent<storedMats>().createMats();

                        for (int x = 0; x < MatsTexts.Length; x++)
                        {
                            currentBuilding.GetComponent<storedMats>().setMats(x, data.getallstoredMats()[i, x]);
                        }

                        for (int x = 0; x < Buildingsplaced.GetComponent<Buildings>().getStorages().Length; x++)
                        {
                            if (Buildingsplaced.GetComponent<Buildings>().getStorage(x) == null)
                            {
                                Buildingsplaced.GetComponent<Buildings>().setStorageplaced(x, currentBuilding);
                                Buildingsplaced.GetComponent<Buildings>().addStorageplaced();
                                break;
                            }
                        }
                    }

                    foreach (GameObject Building in Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().JobBuildings)
                    {
                        if (Building.name == currentBuilding.name)
                        {
                            currentBuilding.GetComponent<JobBuildings>().data = data.getallBuildingsMats()[i];
                        }
                    }

                    foreach (GameObject Building in Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().BuildingsWithTransmitter)
                    {
                        if (Building.name == currentBuilding.name)
                        {
                            currentBuilding.GetComponent<BuildingsWithTransmitter>().data = data.getallBuildingsMats()[i];
                        }
                    }
                }
            }
        }
    }

    public GameObject getotherBuildingsCanvas(string name)
    {
        GameObject Canvas = null;

        foreach (GameObject otherBuildingCanvas in otherBuildingsCanvas)
        {
            if (name == otherBuildingCanvas.name)
            {
                Canvas = otherBuildingCanvas;
                break;
            }
        }

        return Canvas;
    }

    public Text[] getMatsTexts()
    {
        return MatsTexts;
    }

    public int RandomNumber(int maxAmount)
    {
        int rand = 0;

        if (maxAmount > 1)
        {
            rand = Random.Range(0, maxAmount);
        }

        return rand;
    }

    public int getMatsPlace(string which)
    {
        int Place = 0;

        foreach (Text Mat in MatsTexts)
        {
            if(Mat.name == which)
            {
                break;
            }
            else
            {
                Place += 1;
            }
        }

        return Place;
    }

    public GameObject getfreeBuildingsCanvas()
    {
        return freeBuildingsCanvas;
    }

    public GameObject getcostingBuildingsCanvas()
    {
        return costingBuildingsCanvas;
    }

    public GameObject getBuildingsWithTransmitterCanvas()
    {
        return BuildingsWithTransmitterCanvas;
    }

    public GameObject getMainCameraCanvas()
    {
        return MainCameraCanvas;
    }

    public GameObject getJobBuildingsCanvas()
    {
        return JobBuildingsCanvas;
    }

    public string getchoosedBuilding()
    {
        return choosedBuilding;
    }

    public void setchoosedBuilding(string what)
    {
        choosedBuilding = what;
    }

    public StringandNumber.rowData[] getBuildingArrays()
    {
        return Buildings.Arrays;
    }

    public GameObject[] getallBuildings()
    {
        return allBuildings;
    }

    public void Exitnormal(GameObject destroyingObj)
    {
        Destroy(destroyingObj);

        GetComponent<choosedObj>().setinGame(true);
    }

    public void ExitBuildingsCanvas(GameObject destroyingObj)
    {
        Destroy(destroyingObj);

        GetComponent<choosedObj>().setIsBuildingsCanvasOn(false);
    }

    public void MoveBuilding(GameObject Building)
    {
        Building.transform.SetParent(ChoosedBuildingPlace.transform);
        choosedBuilding = Building.name;
        GetComponent<choosedObj>().setBuildingInstanced(true);
    }

    public GameObject getBuilding(string what)
    {
        foreach (GameObject currentBuilding in allBuildings)
        {
            if (currentBuilding.name == what)
            {
                return currentBuilding;
            }
        }
        Debug.LogError("searched Building can't be found: " + what);
        return null;
    }
}