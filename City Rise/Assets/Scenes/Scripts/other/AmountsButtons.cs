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
                if (BuildingsCanvas.name != "free Buildings Canvas" || GetComponent<Amount>().Costs != 0)
                {
                    Debug.Log(GetComponent<Amount>().Costs);
                    Child.GetComponent<Text>().text = GetComponent<Amount>().Costs + "";
                }
            }
            if(Child.name == "Amount")
            {
                if (GetComponent<Amount>().Number > 1)
                {
                    Child.GetComponent<Text>().text = GetComponent<Amount>().Number + "";
                }
            }
        }
    }

    public GameObject getBuildingsCanvas()
    {
        return BuildingsCanvas;
    }
}