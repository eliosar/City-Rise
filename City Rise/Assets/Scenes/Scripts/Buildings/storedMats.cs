using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storedMats : MonoBehaviour
{
    public int maxMats = 120;
    private int[] Mats;

    private void Start()
    {
        if (Mats == null)
        {
            Mats = new int[GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts.Length];

            for (int i = 0; i < Mats.Length; i++)
            {
                Mats[i] = 0;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < Mats.Length; i++)
        {
            if (Mats[i] > maxMats)
            {
                Mats[i] = maxMats;
            }
        }
    }

    public void createMats()
    {
        Mats = new int[GetComponentInParent<getMainCamera>().mainCamera.GetComponent<Main>().MatsTexts.Length];

        for (int i = 0; i < Mats.Length; i++)
        {
            Mats[i] = 0;
        }
    }

    public int getMats(int which)
    {
        return Mats[which];
    }

    public void addMats(int which, int add)
    {
        Mats[which] += add;
    }

    public void setMats(int which, int what)
    {
        Mats[which] = what;
    }
    public void takeMats(int which, int take)
    {
        Mats[which] -= take;
    }
    public int getMaxMats()
    {
        return maxMats;
    }
}
