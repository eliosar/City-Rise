using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public string[] MatsNames;
    public RawImage[] MatsButtons;
    public Text[] MatsTexts;
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

        foreach (string MatName in MatsNames)
        {
            if(MatName == which)
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

    public StringandNumber.rowData getArray(int which)
    {
        return Buildings.Arrays[which];
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

    public int getMatsAmount()
    {
        return MatsNames.Length;
    }

    public StringandNumber.rowData[] getTrades()
    {
        return Trades.Arrays;
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
}