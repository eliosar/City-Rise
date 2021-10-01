using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storedMats : MonoBehaviour
{
    public int maxMats = 120;
    private int[] Mats;
    private int MatsAmount;

    private void Start()
    {
        MatsAmount = GetComponent<getMainCamera>().mainCamera.GetComponent<Main>().getMatsAmount();
        Mats = new int[MatsAmount];

        for (int i = 0; i < MatsAmount; i++)
        {
            Mats[i] = 0;
        }
    }

    private void Update()
    {
        for (int i = 0; i < MatsAmount; i++)
        {
            if (Mats[i] > maxMats)
            {
                Mats[i] = maxMats;
            }
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

    public void takeMats(int which, int take)
    {
        Mats[which] -= take;
    }
}
