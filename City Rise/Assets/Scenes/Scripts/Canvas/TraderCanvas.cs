using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderCanvas : MonoBehaviour
{
    void Start()
    {
        GameObject Buildings = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject;

        for (int i = 0; i < Buildings.transform.childCount; i++)
        {
            GameObject Child = Buildings.transform.GetChild(i).gameObject;

            foreach (StringandNumber.rowData Array in GetComponentInParent<Trader>().getTradesArray())
            {
                if(Child.name == Array.Name)
                {
                    Child.GetComponent<Amount>().Number = Array.Amount;
                }
            }
        }
    }

    public void Exit()
    {
        Destroy(gameObject);

        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setinGame(true);
    }
}
