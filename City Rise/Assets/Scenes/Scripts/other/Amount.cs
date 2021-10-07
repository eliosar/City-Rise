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
    private GameObject MainCameraCanvas;

    private void Start()
    {
        Canvas = transform.parent.parent.parent.parent.parent.gameObject;
        Buildingsplaced = Canvas.transform.parent.gameObject;

        MainCamera = Canvas;

        while (MainCamera.name != "Buildings placed" || MainCamera.name != "Main Camera Canvas")
        {
            MainCamera = MainCamera.transform.parent.gameObject;
            Debug.Log("Main Camera: " + MainCamera.name);
        }

        MainCamera = MainCamera.GetComponent<getMainCamera>().mainCamera;
        
        MainCameraCanvas = MainCamera.GetComponent<Main>().getMainCameraCanvas();
    }

    public void buyMats()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject Storage = Buildingsplaced.GetComponent<Buildings>().getStorage(MainCamera.GetComponent<Main>().RandomNumber(MainCamera.GetComponent<Main>().getStoragesplaced()));
            int maxMats = Storage.GetComponent<storedMats>().getMats(MainCamera.GetComponent<Main>().getMatsPlace(Canvas.name));

            if (maxMats < Costs)
            {
                Storage.GetComponent<storedMats>().takeMats(MainCamera.GetComponent<Main>().getMatsPlace(Canvas.name), maxMats);

                Costs -= maxMats;

                i -= 1;
            }
            else
            {
                Storage.GetComponent<storedMats>().takeMats(MainCamera.GetComponent<Main>().getMatsPlace(Canvas.name), Costs);
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