using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageCanvas : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = "Storage\n\n";

        for(int i = 0; i < transform.parent.parent.GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsAmount(); i++)
        {
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<storedMats>().getMats(i) + " / " + GetComponentInParent<storedMats>().maxMats + " " + GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts[i].name + "\n";
        }
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