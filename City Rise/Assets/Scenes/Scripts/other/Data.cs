using UnityEngine;

[System.Serializable]
public class Data
{
    private StringandNumber.rowData[] allfreeBuildings;
    private int[,] allstoredMats;
    private ArrayLayout[] allBuildingsMats;
    private string[] allBuildingsName;
    private float[] allBuildingsRotation;
    private float[,] allBuildingsPos;
    private float[] CamPos;

    public Data(GameObject Buildingsplaced)
    {
        GameObject mainCamera = Buildingsplaced.GetComponent<getMainCamera>().mainCamera.gameObject;

        CamPos = new float[3];
        allBuildingsRotation = new float[Buildingsplaced.transform.childCount];
        allBuildingsPos = new float[allBuildingsRotation.Length, 3];
        allBuildingsName = new string[allBuildingsRotation.Length];
        allBuildingsMats = new ArrayLayout[allBuildingsRotation.Length];
        allstoredMats = new int[allBuildingsRotation.Length, mainCamera.GetComponent<Main>().MatsTexts.Length];
        allfreeBuildings = new StringandNumber.rowData[mainCamera.GetComponent<Main>().getallBuildings().Length];

        CamPos[0] = mainCamera.transform.position.x;
        CamPos[1] = mainCamera.transform.position.y;
        CamPos[2] = mainCamera.transform.position.z;

        for (int i = 0; i < Buildingsplaced.transform.childCount; i++)
        {
            GameObject Child = Buildingsplaced.transform.GetChild(i).gameObject;

            if (Child.name != "Trader") {
                allBuildingsPos[i, 0] = Child.transform.position.x;
                allBuildingsPos[i, 1] = Child.transform.position.y;
                allBuildingsPos[i, 2] = Child.transform.position.z;

                allBuildingsRotation[i] = Child.transform.rotation.x;

                allBuildingsName[i] = Child.name;

                foreach (GameObject currentBuilding in Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().JobBuildings)
                {
                    if(currentBuilding.name == Child.name)
                    {
                        allBuildingsMats[i] = Child.GetComponent<JobBuildings>().data;
                    }
                }

                foreach (GameObject currentBuilding in Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().BuildingsWithTransmitter)
                {
                    if (currentBuilding.name == Child.name)
                    {
                        allBuildingsMats[i] = Child.GetComponent<BuildingsWithTransmitter>().data;
                    }
                }

                if(Child.name == "Storage")
                {
                    for (int x = 0; x < mainCamera.GetComponent<Main>().MatsTexts.Length; x++)
                    {
                        allstoredMats[i, x] = Child.GetComponent<storedMats>().getMats(x);
                    } 
                }
            }
        }

        allfreeBuildings = mainCamera.GetComponent<Main>().getBuildingArrays();
    }

    public StringandNumber.rowData[] getallfreeBuildings()
    {
        return allfreeBuildings;
    }
    public int[,] getallstoredMats()
    {
        return allstoredMats;
    }
    public ArrayLayout[] getallBuildingsMats()
    {
        return allBuildingsMats;
    }
    public string[] getallBuildingsName()
    {
        return allBuildingsName;
    }
    public float[] getallBuildingsRotation()
    {
        return allBuildingsRotation;
    }
    public float[,] getallBuildingsPos()
    {
        return allBuildingsPos;
    }
    public float[] getCamPos()
    {
        return CamPos;
    }
}