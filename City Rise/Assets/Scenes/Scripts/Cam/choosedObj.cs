using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choosedObj : MonoBehaviour
{
    private Vector3 posHit;
    public int range = 300;
    private RaycastHit hit;
    private GameObject hittedObj;
    private Collider hittedCol;
    private GameObject BuildingsCanvas;
    private GameObject currentchoosedBuilding;
    private GameObject lasthittetObj;
    private GameObject Buildingsplaced;
    private GameObject ChoosedBuildingPlace;
    private bool IsBuildingsCanvasOn = false;
    private bool BuildingInstancend = false;
    private bool NotInObj = true;
    private bool InGame = true;
    private bool isfreeBuilding = false;
    private int currentJobBuildings = 0;
    private int currentBuildingsWithTransmitter = 0;

    private void Start()
    {
        Buildingsplaced = GetComponent<Main>().Buildingsplaced;
        ChoosedBuildingPlace = GetComponent<Main>().ChoosedBuildingPlace;
    }

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
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        if (currentchoosedBuilding != null)
                        {
                            Vector3 rotation = currentchoosedBuilding.transform.eulerAngles;
                            currentchoosedBuilding.transform.eulerAngles = new Vector3(rotation.x, rotation.y + 90, rotation.z);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        InGame = false;
                        GameObject Menu = null;

                        foreach (GameObject Canvas in GetComponent<Main>().Canvas)
                        {
                            if (Canvas.name == "Menu")
                            {
                                Menu = Canvas;
                                break;
                            }
                        }

                        GameObject Menuobj = Instantiate(Menu, GetComponent<Main>().getMainCameraCanvas().transform);
                        Menuobj.name = Menu.name;
                        Menuobj.transform.localPosition = new Vector2(0, 0);
                    }

                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (BuildingInstancend)
                        {
                            if (NotInObj)
                            {
                                ChoosedBuildingPlace.transform.GetChild(0).GetComponent<BuildingsScript>().setIsPlaced(true);

                                foreach (GameObject JobBuilding in GetComponent<Main>().JobBuildings)
                                {
                                    if (GetComponent<Main>().getchoosedBuilding() == JobBuilding.name)
                                    {
                                        currentJobBuildings += 1;
                                        ChoosedBuildingPlace.transform.GetChild(0).GetComponent<JobBuildings>().enabled = true;
                                        Buildingsplaced.GetComponent<Buildings>().addallmaxPeopleInBuildings(ChoosedBuildingPlace.transform.GetChild(0).GetComponent<JobBuildings>().maxPeopleInBuilding);
                                        break;
                                    }
                                }

                                foreach (GameObject Building in GetComponent<Main>().BuildingsWithTransmitter)
                                {
                                    if (GetComponent<Main>().getchoosedBuilding() == Building.name)
                                    {
                                        currentBuildingsWithTransmitter += 1;
                                        ChoosedBuildingPlace.transform.GetChild(0).GetComponent<BuildingsWithTransmitter>().enabled = true;

                                        Buildingsplaced.GetComponent<Buildings>().addallmaxPeopleInBuildings(ChoosedBuildingPlace.transform.GetChild(0).GetComponent<BuildingsWithTransmitter>().maxPeopleInBuilding);
                                        break;
                                    }
                                }

                                if (GetComponent<Main>().getchoosedBuilding() == "House")
                                {
                                    ChoosedBuildingPlace.transform.GetChild(0).GetComponent<House>().enabled = true;
                                    Buildingsplaced.GetComponent<Buildings>().addtotalPeopleforBuildings(ChoosedBuildingPlace.transform.GetChild(0).GetComponent<House>().PeopleInBuilding);
                                }

                                if (GetComponent<Main>().getchoosedBuilding() == "Storage")
                                {
                                    int currentBuildings = GetComponent<Main>().Buildingsplaced.transform.childCount;
                                    Buildingsplaced.GetComponent<Buildings>().addStoragesplaced(1);
                                    Buildingsplaced.GetComponent<Buildings>().setStorageplaced(Buildingsplaced.GetComponent<Buildings>().getStoragesplaced() - 1, ChoosedBuildingPlace.transform.GetChild(0).gameObject);
                                }

                                GetComponent<Main>().setchoosedBuilding(null);
                                BuildingInstancend = false;

                                if (isfreeBuilding)
                                {
                                    StringandNumber.rowData[] Array = GetComponent<Main>().getBuildingArrays();

                                    for (int i = 0; i < Array.Length; i++)
                                    {
                                        if(Array[i].Name == ChoosedBuildingPlace.transform.GetChild(0).name)
                                        {
                                            Array[i].Amount -= 1;
                                        }
                                    }
                                    isfreeBuilding = false;
                                }
                                ChoosedBuildingPlace.transform.GetChild(0).gameObject.transform.SetParent(GetComponent<Main>().Buildingsplaced.transform);
                            }
                        }
                        else
                        {
                            if (hittedObj.name != GetComponent<Main>().Terrain.name)
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
                                    IsBuildingsCanvasOn = true;

                                    CanvasInstantiation();
                                }
                                lasthittetObj = hittedObj;
                            }
                        }
                    }

                    if (GetComponent<Main>().getchoosedBuilding() != null)
                    {
                        if (hittedObj.name == GetComponent<Main>().Terrain.name)
                        {
                            ChoosedBuildingPlace.transform.position = posHit;
                            ChoosedBuildingPlace.transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, 0);
                        }
                        /*if (hittedCol is SphereCollider)
                        {
                            Debug.Log("Sphere");
                            while (ChoosedBuildingPlace.GetComponent<choosedBuildingPlace>().Terrainhitted == false)
                            {
                                Debug.Log("false");
                                ChoosedBuildingPlace.transform.rotation = transform.rotation;
                                ChoosedBuildingPlace.transform.position += new Vector3(0, 0.5f, 0);
                            }
                            ChoosedBuildingPlace.GetComponent<choosedBuildingPlace>().Terrainhitted = true;
                            ChoosedBuildingPlace.transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, 0);
                        }*/

                        if (!BuildingInstancend)
                        {
                            foreach (GameObject otherBuilding in GetComponent<Main>().otherBuildings)
                            {
                                if (GetComponent<Main>().getchoosedBuilding() == otherBuilding.name)
                                {
                                    currentchoosedBuilding = Instantiate(otherBuilding, ChoosedBuildingPlace.transform.position, otherBuilding.transform.rotation);
                                    currentchoosedBuilding.name = otherBuilding.name;
                                    break;
                                }
                            }

                            foreach (GameObject JobBuilding in GetComponent<Main>().JobBuildings)
                            {
                                if (GetComponent<Main>().getchoosedBuilding() == JobBuilding.name)
                                {
                                    currentchoosedBuilding = Instantiate(JobBuilding, ChoosedBuildingPlace.transform.position, JobBuilding.transform.rotation);
                                    currentchoosedBuilding.name = JobBuilding.name;
                                    currentchoosedBuilding.GetComponent<JobBuildings>().enabled = false;
                                    break;
                                }
                            }

                            foreach (GameObject Building in GetComponent<Main>().BuildingsWithTransmitter)
                            {
                                if (GetComponent<Main>().getchoosedBuilding() == Building.name)
                                {
                                    currentchoosedBuilding = Instantiate(Building, ChoosedBuildingPlace.transform.position, Building.transform.rotation);
                                    currentchoosedBuilding.name = Building.name;
                                    currentchoosedBuilding.GetComponent<BuildingsWithTransmitter>().enabled = false;
                                    break;
                                }
                            }
                            currentchoosedBuilding.transform.localScale = new Vector3(2, 2, 2);
                            currentchoosedBuilding.transform.SetParent(ChoosedBuildingPlace.transform);
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
                BuildingsCanvas = Instantiate(GetComponent<Main>().getJobBuildingsCanvas(), hittedObj.transform);
                BuildingsCanvas.name = GetComponent<Main>().getJobBuildingsCanvas().name;
                break;
            }
        }

        foreach (GameObject Building in GetComponent<Main>().BuildingsWithTransmitter)
        {
            if (hittedObj.name == Building.name)
            {
                BuildingsCanvas = Instantiate(GetComponent<Main>().getBuildingsWithTransmitterCanvas(), hittedObj.transform);
                BuildingsCanvas.name = GetComponent<Main>().getBuildingsWithTransmitterCanvas().name;
                break;
            }
        }

        foreach (GameObject otherBuilding in GetComponent<Main>().otherBuildings)
        {
            if (hittedObj.name == otherBuilding.name)
            {
                BuildingsCanvas = Instantiate(GetComponent<Main>().getotherBuildingsCanvas(hittedObj.name + " Canvas"), hittedObj.transform);
                BuildingsCanvas.name = hittedObj.name + " Canvas";

                foreach (GameObject Building in GetComponent<Main>().BuildingsWithBigCanvasWithoutButton)
                {
                    if (Building.name == otherBuilding.name)
                    {
                        InGame = false;
                        IsBuildingsCanvasOn = false;
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