using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choosedObj : MonoBehaviour
{
    private Vector3 posHit;
    private int range = 300;
    private RaycastHit hit;
    private GameObject hittedObj;
    private Collider hittedCol;
    private GameObject BuildingsCanvas;
    private GameObject currentchoosedBuilding;
    private GameObject lasthittetObj;
    private bool IsBuildingsCanvasOn = false;
    private bool BuildingInstancend = false;
    private bool NotInObj = true;
    private bool InGame = true;
    private bool isfreeBuilding = false;
    private int currentJobBuildings = 0;
    private int currentBuildingsWithTransmitter = 0;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform != null)
            {
                posHit = hit.point;
                hittedObj = hit.transform.gameObject;
                hittedCol = hit.collider;

                if (InGame)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (BuildingInstancend)
                        {
                            if (NotInObj)
                            {
                                GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<BuildingsScript>().setIsPlaced(true);

                                foreach (GameObject JobBuilding in GetComponent<Main>().JobBuildings)
                                {
                                    if (GetComponent<Main>().getchoosedBuilding() == JobBuilding.name)
                                    {
                                        currentJobBuildings += 1;
                                        GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<JobBuildings>().enabled = true;
                                        GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().addallmaxPeopleInBuildings(GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<JobBuildings>().maxPeopleInBuilding);
                                        break;
                                    }
                                }

                                foreach (GameObject Building in GetComponent<Main>().BuildingsWithTransmitter)
                                {
                                    if (GetComponent<Main>().getchoosedBuilding() == Building.name)
                                    {
                                        currentBuildingsWithTransmitter += 1;
                                        GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<BuildingsWithTransmitter>().enabled = true;

                                        GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().addallmaxPeopleInBuildings(GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<BuildingsWithTransmitter>().maxPeopleInBuilding);
                                        break;
                                    }
                                }

                                if (GetComponent<Main>().getchoosedBuilding() == "House")
                                {
                                    GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<House>().enabled = true;
                                    GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().addtotalPeopleforBuildings(GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).GetComponent<House>().PeopleInBuilding);
                                }

                                if (GetComponent<Main>().getchoosedBuilding() == "Storage")
                                {
                                    int currentBuildings = GetComponent<Main>().Buildingsplaced.transform.childCount;
                                    GetComponent<Main>().addStoragesplaced(1);
                                    GetComponent<Main>().Buildingsplaced.GetComponent<Buildings>().setStorageplaced(GetComponent<Main>().getStoragesplaced() - 1, GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).gameObject);
                                }

                                GetComponent<Main>().setchoosedBuilding(null);
                                BuildingInstancend = false;

                                if (isfreeBuilding)
                                {
                                    StringandNumber.rowData[] Array = GetComponent<Main>().getBuildingArrays();

                                    for (int i = 0; i < Array.Length; i++)
                                    {
                                        if(Array[i].Name == GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).name)
                                        {
                                            Array[i].Amount -= 1;
                                        }
                                    }
                                    isfreeBuilding = false;
                                }
                                GetComponent<Main>().ChoosedBuildingPlace.transform.GetChild(0).gameObject.transform.SetParent(GetComponent<Main>().Buildingsplaced.transform);
                            }
                        }
                        else
                        {
                            if (hittedObj.name != "Terrain (1)")
                            {
                                if (IsBuildingsCanvasOn)
                                {
                                    int Childs = lasthittetObj.transform.childCount;
                                    int lastChild = Childs - 1;
                                    Destroy(lasthittetObj.transform.GetChild(lastChild).gameObject);

                                    CanvasInstantiation();
                                }
                                else
                                {
                                    CanvasInstantiation();

                                    IsBuildingsCanvasOn = true;
                                }
                                lasthittetObj = hittedObj;
                            }
                        }
                    }

                    if (GetComponent<Main>().getchoosedBuilding() != null)
                    {
                        if (hittedObj.name == "Terrain (1)")
                        {
                            GetComponent<Main>().ChoosedBuildingPlace.transform.position = posHit;
                            GetComponent<Main>().ChoosedBuildingPlace.transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, 0);
                        }
                        /*if (hittedCol is SphereCollider)
                        {
                            Debug.Log("Sphere");
                            while (GetComponent<Main>().ChoosedBuildingPlace.GetComponent<choosedBuildingPlace>().Terrainhitted == false)
                            {
                                Debug.Log("false");
                                GetComponent<Main>().ChoosedBuildingPlace.transform.rotation = transform.rotation;
                                GetComponent<Main>().ChoosedBuildingPlace.transform.position += new Vector3(0, 0.5f, 0);
                            }
                            GetComponent<Main>().ChoosedBuildingPlace.GetComponent<choosedBuildingPlace>().Terrainhitted = true;
                            GetComponent<Main>().ChoosedBuildingPlace.transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, 0);
                        }*/

                        if (!BuildingInstancend)
                        {
                            foreach (GameObject otherBuilding in GetComponent<Main>().otherBuildings)
                            {
                                if (GetComponent<Main>().getchoosedBuilding() == otherBuilding.name)
                                {
                                    currentchoosedBuilding = Instantiate(otherBuilding, GetComponent<Main>().ChoosedBuildingPlace.transform.position, GetComponent<Main>().ChoosedBuildingPlace.transform.rotation);
                                    currentchoosedBuilding.name = otherBuilding.name;
                                    break;
                                }
                            }

                            foreach (GameObject JobBuilding in GetComponent<Main>().JobBuildings)
                            {
                                if (GetComponent<Main>().getchoosedBuilding() == JobBuilding.name)
                                {
                                    currentchoosedBuilding = Instantiate(JobBuilding, GetComponent<Main>().ChoosedBuildingPlace.transform.position, GetComponent<Main>().ChoosedBuildingPlace.transform.rotation);
                                    currentchoosedBuilding.name = JobBuilding.name;
                                    currentchoosedBuilding.GetComponent<JobBuildings>().enabled = false;
                                    break;
                                }
                            }

                            foreach (GameObject Building in GetComponent<Main>().BuildingsWithTransmitter)
                            {
                                if (GetComponent<Main>().getchoosedBuilding() == Building.name)
                                {
                                    currentchoosedBuilding = Instantiate(Building, GetComponent<Main>().ChoosedBuildingPlace.transform.position, GetComponent<Main>().ChoosedBuildingPlace.transform.rotation);
                                    currentchoosedBuilding.name = Building.name;
                                    currentchoosedBuilding.GetComponent<BuildingsWithTransmitter>().enabled = false;
                                    break;
                                }
                            }
                            currentchoosedBuilding.transform.SetParent(GetComponent<Main>().ChoosedBuildingPlace.transform);
                            BuildingInstancend = true;
                        }
                    }
                }
            }
        }
    }

    private void CanvasInstantiation()
    {
        foreach (GameObject JobBuilding in GetComponent<Main>().JobBuildings)
        {
            if (hittedObj.name == JobBuilding.name)
            {
                BuildingsCanvas = Instantiate(GetComponent<Main>().getJobBuildingsCanvas());
                BuildingsCanvas.transform.SetParent(hittedObj.transform);
                BuildingsCanvas.name = GetComponent<Main>().getJobBuildingsCanvas().name;
                break;
            }
        }

        foreach (GameObject Building in GetComponent<Main>().BuildingsWithTransmitter)
        {
            if (hittedObj.name == Building.name)
            {
                BuildingsCanvas = Instantiate(GetComponent<Main>().getBuildingsWithTransmitterCanvas());
                BuildingsCanvas.transform.SetParent(hittedObj.transform);
                BuildingsCanvas.name = GetComponent<Main>().getBuildingsWithTransmitterCanvas().name;
                break;
            }
        }

        foreach (GameObject otherBuilding in GetComponent<Main>().otherBuildings)
        {
            if (hittedObj.name == otherBuilding.name)
            {
                BuildingsCanvas = Instantiate(GetComponent<Main>().getotherBuildingsCanvas(hittedObj.name + " Canvas"));
                BuildingsCanvas.transform.SetParent(hittedObj.transform);
                BuildingsCanvas.name = hittedObj.name + " Canvas";

                foreach (GameObject Building in GetComponent<Main>().BuildingsWithBigCanvasWithoutButton)
                {
                    if (Building.name == otherBuilding.name)
                    {
                        InGame = false;
                        IsBuildingsCanvasOn = false;

                        for (int i = 0; i < GetComponent<Main>().getMainCameraCanvas().GetComponent<MainCamera>().getButtonsFolder().transform.childCount; i++)
                        {
                            GameObject Child = GetComponent<Main>().getMainCameraCanvas().GetComponent<MainCamera>().getButtonsFolder().transform.GetChild(i).gameObject;

                            Child.GetComponent<Button>().interactable = false;
                        }
                        break;
                    }
                }
                break;
            }
        }
        BuildingsCanvas.GetComponentInParent<getMainCamera>().mainCamera = gameObject;
    }

    public void setIsBuildingsCanvasOn(bool set)
    {
        IsBuildingsCanvasOn = set;
    }

    public bool getBuildingInstanced()
    {
        return BuildingInstancend;
    }

    public void setBuildingInstanced(bool set)
    {
        BuildingInstancend = set;
    }

    public void setNotInObj(bool set)
    {
        NotInObj = set;
    }

    public void setinGame(bool set)
    {
        InGame = set;
    }

    public bool getInGame()
    {
        return InGame;
    }

    public int getcurrentJobBuildings()
    {
        return currentJobBuildings;
    }

    public int getcurrentBuidlingsWithTransmitter()
    {
        return currentBuildingsWithTransmitter;
    }

    public void setisfreeBuilding(bool what)
    {
        isfreeBuilding = what;
    }
}