using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Amount : MonoBehaviour
{
    private int Number = 1;
    private int Costs;
    private GameObject Canvas;
    private GameObject Buildingsplaced;
    private GameObject MainCamera;

    private void Start()
    {
        Canvas = transform.parent.parent.parent.parent.parent.gameObject;

        MainCamera = Canvas;

        while (MainCamera.name != "Buildings placed" && MainCamera.name != "Main Camera Canvas")
        {
            MainCamera = MainCamera.transform.parent.gameObject;
        }

        MainCamera = MainCamera.GetComponent<getMainCamera>().mainCamera;
    }

    public void buyMats()
    {
        GameObject Storage = null;
        Buildingsplaced = Canvas.transform.parent.parent.gameObject;

        for (int i = 0; i < 1; i++)
        {
            Storage = Buildingsplaced.GetComponent<Buildings>().getrandomStorage();
            int Matsplace = MainCamera.GetComponent<Main>().getMatsPlace(gameObject.name);
            int maxMats = Storage.GetComponent<storedMats>().getMats(Matsplace);

            if (maxMats < Costs)
            {
                Storage.GetComponent<storedMats>().takeMats(Matsplace, maxMats);

                Costs -= maxMats;

                i -= 1;
            }
            else
            {
                Storage.GetComponent<storedMats>().takeMats(Matsplace, Costs);
                Costs = 0;
            }
        }

        Storage.GetComponent<storedMats>().addMats(MainCamera.GetComponent<Main>().getMatsPlace("Rubin"), Number);
    }

    public void buyBuildings()
    {
        GameObject Storage = null;
        Buildingsplaced = Canvas.transform.parent.parent.parent.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().Buildingsplaced;

        for (int i = 0; i < 1; i++)
        {
            Storage = Buildingsplaced.GetComponent<Buildings>().getrandomStorage();
            int Matsplace = MainCamera.GetComponent<Main>().getMatsPlace("Rubin");
            int maxMats = Storage.GetComponent<storedMats>().getMats(Matsplace);

            if (maxMats < GetComponent<Costs>().Rubin)
            {
                Storage.GetComponent<storedMats>().takeMats(Matsplace, maxMats);
                GetComponent<Costs>().Rubin -= maxMats;

                i -= 1;
            }
            else
            {
                Storage.GetComponent<storedMats>().takeMats(Matsplace, GetComponent<Costs>().Rubin);
                GetComponent<Costs>().Rubin -= Costs;
            }
        }
    }

    public void choosedBuilding()
    {
        Canvas.transform.parent.parent.parent.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().setchoosedBuilding(gameObject.name);
    }

    public void setCosts(int what)
    {
        Costs = what;
    }

    public int getCosts()
    {
        return Costs;
    }

    public void setNumber(int what)
    {
        Number = what;
    }

    public int getNumber()
    {
        return Number;
    }
}