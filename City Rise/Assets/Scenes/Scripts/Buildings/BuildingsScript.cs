using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsScript : MonoBehaviour
{
    private Color defaultColor;
    private bool IsPlaced = false;
    private Collider myColLastHit;
    private Collider myColLastExited;

    private void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void Update()
    {
        if (IsPlaced) {
            GetComponent<MeshRenderer>().material.color = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Terrain (1)" && other.name != "choosedBuildingPlace")
        {
            /*if (gameObject.name == "Fischer")
            {
                Debug.Log("Enter");
                myColLastHit = other.GetComponent<ILastTouched>().ILastEntered;
                Debug.Log(myColLastHit);

                if (myColLastHit is SphereCollider)
                {
                    if(other.name == "WaterBasicDaytime")
                    {
                        Debug.Log("Water");
                        GetComponent<MeshRenderer>().material.color = Color.green;

                        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setNotInObj(true);
                    }
                }
                else if (myColLastHit is BoxCollider)
                {
                    GetComponent<MeshRenderer>().material.color = Color.red;

                    GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setNotInObj(false);
                }
            }
            else
            {*/
                GetComponent<MeshRenderer>().material.color = Color.red;

                GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setNotInObj(false);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Terrain (1)" && other.name != "choosedBuildingPlace")
        {
            /*if (gameObject.name == "Fischer")
            {
                Debug.Log("Exit");
                myColLastExited = other.GetComponent<ILastTouched>().ILastEntered;

                if (myColLastExited is SphereCollider)
                {
                    if (other.name == "WaterBasicDaytime")
                    {
                        GetComponent<MeshRenderer>().material.color = Color.red;

                        GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setNotInObj(false);
                    }
                }
                else if (myColLastHit is BoxCollider)
                {
                    GetComponent<MeshRenderer>().material.color = Color.green;

                    GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setNotInObj(true);
                }
            }
            else
            {*/
                GetComponent<MeshRenderer>().material.color = Color.green;

                GetComponentInParent<getMainCamera>().mainCamera.GetComponent<choosedObj>().setNotInObj(true);
            //}
        }
    }

    public void setIsPlaced(bool set)
    {
        IsPlaced = set;
    }
}