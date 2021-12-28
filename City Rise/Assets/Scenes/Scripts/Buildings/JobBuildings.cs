using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobBuildings : MonoBehaviour
{
    public int maxPeopleInBuilding = 2;
    private int currentPeopleInBuilding = 0;
    public Text[] MatsTexts;
    public bool transmitter = false;
    public ArrayLayout data;
    private int[,] ProbabilityAmounts;
    private int[,] MatsProbability;
    public int maxSec = 15;
    public int maxTransSec = 10;
    private int[] TransmittetMatsAmount;
    private int[] MatsAmount;
    private int maxProductAmount = 30;
    private bool newmats = true;

    private void Start()
    {
        if (MatsAmount == null && TransmittetMatsAmount == null)
        {
            MatsAmount = new int[MatsTexts.Length];
            TransmittetMatsAmount = new int[MatsTexts.Length];

            for (int i = 0; i < MatsAmount.Length; i++)
            {
                MatsAmount[i] = 0;
            }
        }

        if (transmitter == false)
        {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPeopleInBuilding > 0)
        {
            if (transmitter)
            {
                if(TransmittetMatsAmount.Length > 0)
                {
                    if (newmats)
                    {
                        StartCoroutine(NewMats(maxTransSec));
                    }
                }
            }
            else
            {
                if (newmats)
                {
                    StartCoroutine(NewMats(maxSec));
                }
            }
        }
    }

    private IEnumerator NewMats(int Sec)
    {
        newmats = false;
        float waitingSeconds = Sec / currentPeopleInBuilding;

        yield return new WaitForSeconds(waitingSeconds);
        for (int i = 0; i < MatsTexts.Length; i++)
        {
            if (MatsAmount[i] < maxProductAmount)
            {
                if (transmitter)
                {
                    MatsAmount[i] += TransmittetMatsAmount[i];
                    TransmittetMatsAmount[i] = 0;
                }
                else
                {
                    MatsAmount[i] += random(i);
                }

                if (MatsAmount[i] > maxProductAmount)
                {
                    MatsAmount[i] = maxProductAmount;
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
                if (random <= Probability[i])
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

    public int getmaxPeopleinBuilding()
    {
        return maxPeopleInBuilding;
    }

    public int getMatsAmount(int which)
    {
        return MatsAmount[which];
    }

    public int getTransmittedMatsAmount(int which)
    {
        return TransmittetMatsAmount[which];
    }

    public void setMatsAmounts(int[] Amounts)
    {
        MatsAmount = Amounts;
    }

    public void setTransmittetMatsAmounts(int[] Amounts)
    {
        TransmittetMatsAmount = Amounts;
    }

    public int getMatsPlace(string Name)
    {
        for (int i = 0; i < MatsTexts.Length; i++)
        {
            if (MatsTexts[i].name == Name)
            {
                return i;
            }
        }
        Debug.LogError("wrong Mat Name");
        return 0;
    }

    public int getmaxMatsAmount()
    {
        return maxProductAmount;
    }

    public void takeawayMatsAmount(int which, int takeaway)
    {
        MatsAmount[which] -= takeaway;
    }

    public void setNewMats(bool what)
    {
        newmats = what;
    }

    public void addtransmittetMats(int which, int add)
    {
        TransmittetMatsAmount[which] += add;
    }

    public void addcurrentPeopleInBuilding(int add)
    {
        currentPeopleInBuilding += add;
    }

    public int getcurrentPeopleInBuilding()
    {
        return currentPeopleInBuilding;
    }

    public void setcurrentPeopleInBuilding(int set)
    {
        currentPeopleInBuilding = set;
    }
}