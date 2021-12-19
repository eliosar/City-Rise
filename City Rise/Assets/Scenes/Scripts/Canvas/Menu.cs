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
        Debug.Log("Closed");
        Exit();



        Application.Quit();
    }
}
