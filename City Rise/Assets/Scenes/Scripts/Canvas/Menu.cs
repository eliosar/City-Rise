using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Exit()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().Exitnormal(gameObject);
    }

    public void Close()
    {
        SceneLoader.Save(GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().Buildingsplaced);

        Exit();

        Application.Quit();
    }

    public void Delete()
    {
        SceneLoader.Delete();
        Exit();
    }
}
