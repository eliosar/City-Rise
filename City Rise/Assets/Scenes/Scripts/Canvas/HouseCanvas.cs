using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseCanvas : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = "House\n\n" + GetComponentInParent<House>().PeopleInBuilding + " People in Building";
    }

    public void Exit()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().ExitBuildingsCanvas(gameObject);
    }

    public void Move()
    {
        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MoveBuilding(gameObject.transform.parent.gameObject);
        Exit();
    }
}