using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBuildings : MonoBehaviour
{
    public int maxPeopleInBuilding = 2;
    private int currentPeopleInBuilding = 0;
    public string[] MatsName;
    public bool transmitter = false;
    public string originPlaceName;
    public ArrayLayout data;
    private int[,] ProbabilityAmounts;
    private int[,] MatsProbability;
    public int maxSec = 15;
    public int maxTransSec = 10;
    private int[] transmittetMats;
    private int[] currentProductAmount;
    private int maxProductAmount = 30;
    private bool newmats = true;
    private int MatsAmount = 0;

    private void Start()
    {
        foreach (string Mat in MatsName)
        {
            MatsAmount += 1;
        }

        currentProductAmount = new int[MatsAmount];
        transmittetMats = new int[MatsAmount];

        for (int i = 0; i < currentProductAmount.Length; i++)
        {
            currentProductAmount[i] = 0;
        }

        if (transmitter is false)
        {
            int maxProbabilityA = 0;
            int maxMatsP = 0;

            for (int i = 0; i < MatsAmount; i++)
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

            ProbabilityAmounts = new int[MatsAmount, maxProbabilityA];
            MatsProbability = new int[MatsAmount, maxMatsP];

            for (int i = 0; i < MatsAmount; i++)
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
                int transmitMats = 0;

                foreach (int Mat in transmittetMats)
                {
                    transmitMats += Mat;
                }

                if(transmitMats > 0)
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
        for (int i = 0; i < MatsAmount; i++)
        {
            if (currentProductAmount[i] < maxProductAmount)
            {
                if (transmitter)
                {
                    currentProductAmount[i] += transmittetMats[i];
                    transmittetMats[i] = 0;
                }
                else
                {
                    currentProductAmount[i] += random(i);
                }

                if (currentProductAmount[i] > maxProductAmount)
                {
                    currentProductAmount[i] = maxProductAmount;
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

    public int getMatsAmount()
    {
        return MatsAmount;
    }

    public int getmaxPeopleinBuilding()
    {
        return maxPeopleInBuilding;
    }

    public int getcurrentProductAmount(int which)
    {
        return currentProductAmount[which];
    }

    public int getmaxProductAmount()
    {
        return maxProductAmount;
    }

    public void takeawaycurrentProductamount(int which, int takeaway)
    {
        currentProductAmount[which] -= takeaway;
    }

    public void setNewMats(bool what)
    {
        newmats = what;
    }

    public void addtransmittetMats(int which, int add)
    {
        transmittetMats[which] += add;
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