using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    private GameObject[] Storage = new GameObject[4];
    private int TransmitterPlaced;
    private int Storagesplaced = 0;
    private int totalPeopleforBuildings = 0;
    private int allmaxPeopleInBuildings = 0;
    private GameObject MainCamera;

    private void Update()
    {
        MainCamera = GetComponent<getMainCamera>().mainCamera;
        int currentChilds = transform.childCount;
        int Worker = 0;
        int freeWorker;
        int freeWorkspace;

        for (int i = 0; i < currentChilds; i++)
        {
            foreach (GameObject currentChildName in MainCamera.GetComponent<Main>().JobBuildings)
            {
                if (transform.GetChild(i).gameObject.name == currentChildName.name)
                {
                    Worker += transform.GetChild(i).gameObject.GetComponent<JobBuildings>().getcurrentPeopleInBuilding();
                }
            }

            foreach (GameObject currentChildName in MainCamera.GetComponent<Main>().BuildingsWithTransmitter)
            {
                if (transform.GetChild(i).gameObject.name == currentChildName.name)
                {
                    Worker += transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().getcurrentPeopleInBuilding();
                }
            }
        }

        if (totalPeopleforBuildings >= allmaxPeopleInBuildings)
        {
            for (int i = 0; i < currentChilds; i++)
            {
                foreach (GameObject currentChildName in MainCamera.GetComponent<Main>().JobBuildings)
                {
                    if (transform.GetChild(i).gameObject.name == currentChildName.name)
                    {
                        transform.GetChild(i).gameObject.GetComponent<JobBuildings>().setcurrentPeopleInBuilding(transform.GetChild(i).gameObject.GetComponent<JobBuildings>().maxPeopleInBuilding);
                    }
                }

                foreach (GameObject currentChildName in MainCamera.GetComponent<Main>().BuildingsWithTransmitter)
                {
                    if (transform.GetChild(i).gameObject.name == currentChildName.name)
                    {
                        transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().setcurrentPeopleInBuilding(transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().maxPeopleInBuilding);
                    }
                }
            }
        }
        else
        {
            if (totalPeopleforBuildings > Worker)
            {
                freeWorker = totalPeopleforBuildings - Worker;

                for (int i = 0; i < currentChilds; i++)
                {
                    foreach (GameObject currentChildName in MainCamera.GetComponent<Main>().JobBuildings)
                    {
                        if (transform.GetChild(i).gameObject.name == currentChildName.name)
                        {
                            if (transform.GetChild(i).gameObject.GetComponent<JobBuildings>().getcurrentPeopleInBuilding() < transform.GetChild(i).gameObject.GetComponent<JobBuildings>().maxPeopleInBuilding)
                            {
                                freeWorkspace = transform.GetChild(i).gameObject.GetComponent<JobBuildings>().maxPeopleInBuilding - transform.GetChild(i).gameObject.GetComponent<JobBuildings>().getcurrentPeopleInBuilding();
                                if (freeWorker >= freeWorkspace)
                                {
                                    transform.GetChild(i).gameObject.GetComponent<JobBuildings>().addcurrentPeopleInBuilding(freeWorkspace);
                                    freeWorker -= freeWorkspace;
                                }
                                else
                                {
                                    int difference = freeWorkspace - freeWorker;
                                    transform.GetChild(i).gameObject.GetComponent<JobBuildings>().addcurrentPeopleInBuilding(difference);
                                    freeWorker -= difference;
                                }
                            }
                        }
                    }

                    foreach (GameObject currentChildName in MainCamera.GetComponent<Main>().BuildingsWithTransmitter)
                    {
                        if (transform.GetChild(i).gameObject.name == currentChildName.name)
                        {
                            if (transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().getcurrentPeopleInBuilding() < transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().maxPeopleInBuilding)
                            {
                                freeWorkspace = transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().maxPeopleInBuilding - transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().getcurrentPeopleInBuilding();
                                if (freeWorker >= freeWorkspace)
                                {
                                    transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().addcurrentPeopleInBuilding(freeWorkspace);
                                    freeWorker -= freeWorkspace;
                                }
                                else
                                {
                                    int difference = freeWorkspace - freeWorker;
                                    transform.GetChild(i).gameObject.GetComponent<BuildingsWithTransmitter>().addcurrentPeopleInBuilding(difference);
                                    freeWorker -= difference;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void setStorageplaced(int which, GameObject onWhat)
    {
        Storage[which] = onWhat;
    }

    public GameObject getStorage(int which)
    {
        return Storage[which];
    }
    public GameObject[] getStorages()
    {
        return Storage;
    }

    public GameObject getrandomStorage()
    {
        return Storage[MainCamera.GetComponent<Main>().RandomNumber(Storagesplaced)];
    }

    public GameObject getTransmitter(GameObject Transmitter, int which)
    {
        GameObject[] Transmitt;

        Transmitt = new GameObject[TransmitterPlaced];

        for (int x = 0; x < TransmitterPlaced; x++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name == Transmitter.name)
                {
                    Transmitt[x] = transform.GetChild(i).gameObject;
                }
            }
        }

        return Transmitt[which];
    }
    public GameObject[] getTransmitter(GameObject Transmitter)
    {
        GameObject[] Transmitt;

        Transmitt = new GameObject[TransmitterPlaced];

        for (int x = 0; x < TransmitterPlaced; x++)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.name == Transmitter.name)
                {
                    Transmitt[x] = transform.GetChild(i).gameObject;
                    Debug.Log(x);
                }
            }
        }
        return Transmitt;
    }

    public GameObject getrandomTransmitter(GameObject Transmitter)
    {
        return getTransmitter(Transmitter, GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().RandomNumber(getTransmitterPlaced(Transmitter)));
    }

    public int getTransmitterPlaced(GameObject Transmitter)
    {
        int TransmitterCount = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == Transmitter.name)
            {
                TransmitterCount += 1;
            }
        }
        TransmitterPlaced = TransmitterCount;

        return TransmitterPlaced;
    }

    public void addtotalPeopleforBuildings(int add)
    {
        totalPeopleforBuildings += add;
    }

    public void addallmaxPeopleInBuildings(int add)
    {
        allmaxPeopleInBuildings += add;
    }

    public int getStoragesplaced()
    {
        return Storagesplaced;
    }

    public void addStorageplaced()
    {
        Storagesplaced += 1;
    }
}
