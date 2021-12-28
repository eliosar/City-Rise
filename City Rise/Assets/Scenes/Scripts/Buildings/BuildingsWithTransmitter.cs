using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsWithTransmitter : MonoBehaviour
{
    public int maxPeopleInBuilding = 2;
    private int currentPeopleInBuilding = 0;
    public Text[] MatsTexts;
    public GameObject Transmitter;
    public ArrayLayout data;
    private int[,] ProbabilityAmounts;
    private int[,] MatsProbability;
    public int maxSec = 15;
    public int maxMatsAmount = 30;
    private int[] Mats;
    private bool newmats = true;

    private void Start()
    {
        if (Mats == null)
        {
            Mats = new int[MatsTexts.Length];

            for (int i = 0; i < Mats.Length; i++)
            {
                Mats[i] = 0;
            }
        }

        int maxProbabilityA = 0;
        int maxMatsP = 0;

        for (int i = 0; i < MatsTexts.Length; i++)
        {
            if (maxProbabilityA < data.Mats[i].ProbabilityA.Length)
            {
                maxProbabilityA = data.Mats[i].ProbabilityA.Length;
            }

            if (maxMatsP < data.Mats[i].MatsP.Length)
            {
                maxMatsP = data.Mats[i].MatsP.Length;
            }
        }

        ProbabilityAmounts = new int[MatsTexts.Length, maxProbabilityA];
        MatsProbability = new int[MatsTexts.Length, maxMatsP];

        for (int i = 0; i < MatsTexts.Length; i++)
        {
            for (int x = 0; x < data.Mats[i].ProbabilityA.Length; x++)
            {
                ProbabilityAmounts[i, x] = data.Mats[i].ProbabilityA[x];
            }

            for (int x = 0; x < data.Mats[i].MatsP.Length; x++)
            {
                MatsProbability[i, x] = data.Mats[i].MatsP[x];
            }
        }
    }

    void Update()
    {
        if (currentPeopleInBuilding > 0)
        {
            if (newmats)
            {
                StartCoroutine(NewMats());
            }
        }
    }

    private IEnumerator NewMats()
    {
        newmats = false;
        float waitingSeconds = maxSec / currentPeopleInBuilding;

        yield return new WaitForSeconds(waitingSeconds);
        for (int i = 0; i < MatsTexts.Length; i++)
        {
            if (Mats[i] < maxMatsAmount)
            {
                Mats[i] += random(i);

                if (Mats[i] > maxMatsAmount)
                {
                    Mats[i] = maxMatsAmount;
                }
            }
        }
        newmats = true;
    }

    private int random(int whichMat)
    {
        int Amount = 0;
        int[] Probability = new int[data.Mats[whichMat].MatsP.Length];
        Probability[0] = MatsProbability[whichMat, 0];

        for (int i = 1; i < data.Mats[whichMat].MatsP.Length; i++)
        {
            Probability[i] = Probability[i - 1] + MatsProbability[whichMat, i];
        }

        int random = Random.Range(0, 100 + 1);

        for (int i = 0; i < Probability.Length; i++)
        {
            if (i == 0)
            {
                if(random <= Probability[i])
                {
                    Amount = ProbabilityAmounts[whichMat, i];
                    break;
                }
            }
            else
            {
                if (random <= Probability[i] && random > Probability[i - 1])
                {
                    Amount = ProbabilityAmounts[whichMat, i];
                    break;
                }
            }
        }

        return Amount;
    }

    public int getcurrentPeopleInBuilding()
    {
        return currentPeopleInBuilding;
    }

    public void setcurrentPeopleInBuilding(int onWhat)
    {
        currentPeopleInBuilding = onWhat;
    }

    public void addcurrentPeopleInBuilding(int add)
    {
        currentPeopleInBuilding += add;
    }

    public int getMatsAmount(int which)
    {
        return Mats[which];
    }

    public void setMatsAmounts(int[] Amounts)
    {
        Mats = Amounts;
    }

    public void takeawayMatsAmount(int which, int takeaway)
    {
        Mats[which] -= takeaway;
    }

    public int getmaxMatsAmount()
    {
        return maxMatsAmount;
    }
}
