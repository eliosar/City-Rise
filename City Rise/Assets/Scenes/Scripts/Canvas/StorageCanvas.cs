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
            transform.GetChild(0).GetComponent<Text>().text += GetComponentInParent<storedMats>().getMats(i) + " / " + GetComponentInParent<storedMats>().maxMats + " " + GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsNames[i] + "\n";
        }
    }

    public void Exit()
    {
        Destroy(gameObject);

        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setIsBuildingsCanvasOn(false);
    }
}
