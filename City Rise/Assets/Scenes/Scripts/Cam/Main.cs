using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public string[] MatsNames;
    public GameObject[] BuildingsWithBigCanvasWithoutButton;
    public GameObject[] Canvas;
    public GameObject[] JobBuildings;
    public GameObject[] BuildingsWithTransmitter;
    public GameObject[] otherBuildings;
    public RawImage[] costingBuildingsButtons;
    public RawImage[] freeBuildingsButtons;
    public GameObject ChoosedBuildingPlace;
    public GameObject Buildingsplaced;
    private string choosedBuilding;
    private StringandNumber freeBuildings;
    private StringandNumber Trades;
    private int Storagesplaced = 0;
    private GameObject MainCameraCanvas;
    private GameObject JobBuildingsCanvas;
    private GameObject HouseCanvas;
    private GameObject StorageCanvas;
    private GameObject BuildingsWithTransmitterCanvas;
    private GameObject costingBuildingsCanvas;
    private GameObject freeBuildingsCanvas;
    private GameObject[] allBuildings;
    private GameObject[] otherBuildingsCanvas;

    private void Start()
    {
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

        freeBuildings.Arrays = new StringandNumber.rowData[allBuildings.Length];

        for (int i = 0; i < freeBuildings.Arrays.Length; i++)
        {
            freeBuildings.Arrays[i].Name = allBuildings[i].name;
        }

        for (int i = 0; i < freeBuildings.Arrays.Length; i++)
        {
            if (freeBuildings.Arrays[i].Name == "Storage" || freeBuildings.Arrays[i].Name == "House" || freeBuildings.Arrays[i].Name == "Lumberjack")
            {
                freeBuildings.Arrays[i].Amount = 1;
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
                }
            }
        }
    }

    public GameObject getotherBuildingsCanvas(string name)
    {
        GameObject Canvas = null;

        foreach (GameObject otherBuildingCanvas in otherBuildingsCanvas)
        {
            if (otherBuildingCanvas.name == name)
            {
                Canvas = otherBuildingCanvas;
            }
        }

        return Canvas;
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

    public GameObject getHouseCanvas()
    {
        return HouseCanvas;
    }

    public GameObject getStorageCanvas()
    {
        return StorageCanvas;
    }

    public void addStoragesplaced(int add)
    {
        Storagesplaced += add;
    }

    public int getStoragesplaced()
    {
        return Storagesplaced;
    }

    public StringandNumber.rowData getArray(int which)
    {
        return freeBuildings.Arrays[which];
    }

    public string getchoosedBuilding()
    {
        return choosedBuilding;
    }

    public void setchoosedBuilding(string what)
    {
        choosedBuilding = what;
    }

    public StringandNumber.rowData[] getfreeBuildingArrays()
    {
        return freeBuildings.Arrays;
    }

    public GameObject[] getallBuildings()
    {
        return allBuildings;
    }

    public int getMatsAmount()
    {
        return MatsNames.Length;
    }

    public StringandNumber.rowData[] getTrades()
    {
        return Trades.Arrays;
    }
}