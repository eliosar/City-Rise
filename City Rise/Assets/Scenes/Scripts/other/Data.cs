using UnityEngine;

[System.Serializable]
public class Data
{
    private StringandNumber.rowData[] allfreeBuildings;
    private int[,] allstoredMats;
    private int[,] allBuildingsMats;
    private int[,] JobBuildingstransmittetMats;
    private int[] allBuildingsMatsLastPlace;
    private string[] allBuildingsName;
    private float[] allBuildingsRotation;
    private float[,] allBuildingsPos;
    private float[] CamPos;
    private int MatsTextsLength;

    public Data(GameObject Buildingsplaced)
    {
        GameObject mainCamera = Buildingsplaced.GetComponent<getMainCamera>().mainCamera.gameObject;

        MatsTextsLength = mainCamera.GetComponent<Main>().MatsTexts.Length;
        CamPos = new float[3];
        allBuildingsRotation = new float[Buildingsplaced.transform.childCount];
        allBuildingsPos = new float[allBuildingsRotation.Length, 3];
        allBuildingsName = new string[allBuildingsRotation.Length];
        allBuildingsMats = new int[allBuildingsRotation.Length, MatsTextsLength];
        JobBuildingstransmittetMats = new int[allBuildingsRotation.Length, MatsTextsLength];
        allBuildingsMatsLastPlace = new int[allBuildingsRotation.Length];
        allstoredMats = new int[allBuildingsRotation.Length, MatsTextsLength];
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
                        int lastPlace = 0;
                        for (int x = 0; x < Child.GetComponent<JobBuildings>().MatsTexts.Length; x++)
                        {
                            JobBuildingstransmittetMats[i, x] = Child.GetComponent<JobBuildings>().getTransmittedMatsAmount(x);

                            allBuildingsMats[i, x] = Child.GetComponent<JobBuildings>().getMatsAmount(x);
                            lastPlace = x;
                        }
                        allBuildingsMatsLastPlace[i] = lastPlace;
                    }
                }

                foreach (GameObject currentBuilding in Buildingsplaced.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().BuildingsWithTransmitter)
                {
                    if (currentBuilding.name == Child.name)
                    {
                        int lastPlace = 0;
                        for (int x = 0; x < Child.GetComponent<BuildingsWithTransmitter>().MatsTexts.Length; x++)
                        {
                            allBuildingsMats[i, x] = Child.GetComponent<BuildingsWithTransmitter>().getMatsAmount(x);
                        }
                        allBuildingsMatsLastPlace[i] = lastPlace;
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
    public int[] getallBuildingsMats(int index)
    {
        int[] Mats = new int[MatsTextsLength];

        for (int i = 0; i < Mats.Length; i++)
        {
            if(i > allBuildingsMatsLastPlace[index])
            {
                break;
            }

            Mats[i] = allBuildingsMats[index, i];
        }
        return Mats;
    }

    public int[] getJobBuildingstransmittetMats(int index)
    {
        int[] Mats = new int[MatsTextsLength];

        for (int i = 0; i < Mats.Length; i++)
        {
            if (i > allBuildingsMatsLastPlace[index])
            {
                break;
            }

            Mats[i] = JobBuildingstransmittetMats[index, i];
        }
        return Mats;
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