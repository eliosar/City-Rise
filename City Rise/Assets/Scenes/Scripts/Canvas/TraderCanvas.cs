using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject Buildings = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject;

        for (int i = 0; i < Buildings.transform.childCount; i++)
        {
            GameObject Child = Buildings.transform.GetChild(i).gameObject;

            foreach (StringandNumber.rowData Array in GetComponentInParent<Trader>().Trades.Arrays)
            {
                if(Child.name == Array.Name)
                {
                    Child.GetComponent<Amount>().Number = Array.Amount;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Destroy(gameObject);

        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setinGame(true);
    }
}
