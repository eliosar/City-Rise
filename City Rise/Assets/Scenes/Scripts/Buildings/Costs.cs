using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costs : MonoBehaviour
{
    public int Rubins;
    private GameObject MainCameraCanvas;
    private int[] storedRubinsAmount;
    private int RubinPlace;

    private void Start()
    {
        MainCameraCanvas = gameObject;
        while(MainCameraCanvas.name != "Main Camera Canvas")
        {
            MainCameraCanvas = MainCameraCanvas.transform.parent.gameObject;
        }

        for (int i = 0; i < MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().MatsNames.Length; i++)
        {
            if (MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().MatsNames[i] == "Rubins")
            {
                RubinPlace = i;
                break;
            }
        }

        storedRubinsAmount = new int[MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().MatsNames.Length];
    }

    private void Update()
    {
        for (int i = 0; i < storedRubinsAmount.Length; i++)
        {
            storedRubinsAmount[i] = MainCameraCanvas.GetComponent<MainCamera>().getMats(i);
        }

        bool enough = true;
        for (int i = 0; i < storedRubinsAmount.Length; i++)
        {
            if(storedRubinsAmount[i] < Rubins)
            {
                enough = false;
            }
        }

        if(enough == false)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void buy()
    {
        GameObject Buildings = MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().Buildingsplaced;
        int noStorages = 0;
        for (int x = 0; x < MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getStoragesplaced(); x++)
        {
            GameObject Storage = Buildings.GetComponent<Buildings>().getStorage(x);

            if (Storage.GetComponent<storedMats>().getMats(RubinPlace) >= Rubins)
            {
                Storage.GetComponent<storedMats>().takeMats(RubinPlace, Rubins);
                Rubins = 0;
                break;
            }
            else
            {
                noStorages += 1;
            }
        }

        if (noStorages == MainCameraCanvas.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getStoragesplaced())
        {
            int Amount = Rubins;

            for (int x = 0; x < 1; x++)
            {
                GameObject Storage = Buildings.GetComponent<Buildings>().getStorage(MainCameraCanvas.GetComponent<MainCamera>().RandomNumber(MainCameraCanvas.GetComponent<Main>().getStoragesplaced()));

                if (Storage.GetComponent<storedMats>().getMats(RubinPlace) > 0)
                {
                    if (Storage.GetComponent<storedMats>().getMats(RubinPlace) <= Amount)
                    {
                        Storage.GetComponent<storedMats>().takeMats(RubinPlace, Storage.GetComponent<storedMats>().getMats(RubinPlace));
                        Amount -= Storage.GetComponent<storedMats>().getMats(RubinPlace);
                        x -= 1;
                    }
                    else
                    {
                        Storage.GetComponent<storedMats>().takeMats(RubinPlace, Amount);
                        Amount = 0;
                    }
                }
                else
                {
                    x -= 1;
                }
            }
        }
    }

    public int getstoredMatsAmount(int which)
    {
        return storedRubinsAmount[which];
    }
}
