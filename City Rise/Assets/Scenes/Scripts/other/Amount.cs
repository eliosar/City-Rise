using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amount : MonoBehaviour
{
    private int Number = 1;
    private int Costs;
    private GameObject Canvas;

    private void Start()
    {
        Canvas = transform.parent.parent.parent.parent.parent.gameObject;
    }

    public void buyMats()
    {
        
    }

    public void choosedBuilding()
    {
        Canvas.transform.parent.parent.parent.GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().setchoosedBuilding(gameObject.name);
    }

    public void setCosts(int what)
    {
        Costs = what;
    }

    public int getCosts()
    {
        return Costs;
    }

    public void setNumber(int what)
    {
        Number = what;
    }

    public int getNumber()
    {
        return Number;
    }
}