using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderCanvas : MonoBehaviour
{
    void Start()
    {
        GameObject Buildings = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject;
        RawImage Trade;

        foreach (StringandNumber.rowData Array in GetComponentInParent<Trader>().getTradesArray())
        {
            foreach (RawImage Button in transform.parent.parent.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().MatsButtons)
            {
                if(Button.name == Array.Name)
                {
                    Trade = Instantiate(Button);
                    Trade.transform.SetParent(Buildings.transform);
                    Trade.GetComponent<Amount>().setNumber(Array.Amount);
                    Trade.GetComponent<Amount>().setCosts(Array.Costs);
                    Trade.GetComponent<Button>().onClick.AddListener(Trade.GetComponent<Amount>().buyMats);
                    Trade.GetComponent<Button>().onClick.AddListener(Exit);
                    Trade.name = Button.name;
                    break;
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
