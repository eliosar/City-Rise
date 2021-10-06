using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountsButtons : MonoBehaviour
{
    private GameObject BuildingsCanvas;

    void Start()
    {
        BuildingsCanvas = transform.parent.parent.parent.parent.parent.gameObject;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject Child = transform.GetChild(i).gameObject;

            if (Child.name == "Cost")
            {
                if (BuildingsCanvas.name != "free Buildings Canvas" && GetComponent<Amount>().getCosts() > 0)
                {
                    Child.GetComponent<Text>().text = GetComponent<Amount>().getCosts() + "";
                }
            }
            if(Child.name == "Amount")
            {
                if (GetComponent<Amount>().getNumber() > 1)
                {
                    Child.GetComponent<Text>().text = GetComponent<Amount>().getNumber() + "";
                }
            }
        }
    }

    public GameObject getBuildingsCanvas()
    {
        return BuildingsCanvas;
    }
}