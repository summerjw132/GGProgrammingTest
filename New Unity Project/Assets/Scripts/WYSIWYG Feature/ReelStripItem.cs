using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelStripItem
{

    private enum ReelStripValues
    {
        A = 0,
        B = 0, 
        C = 0,
        D = 0,
        Coin = 1
    }

    [SerializeField]
    private List<ReelStripValues> reelStrip = new List<ReelStripValues>() { ReelStripValues.C, ReelStripValues.A, ReelStripValues.Coin,
        ReelStripValues.D, ReelStripValues.B, ReelStripValues.A, ReelStripValues.B, ReelStripValues.C, ReelStripValues.Coin, ReelStripValues.A,
        ReelStripValues.D, ReelStripValues.C, ReelStripValues.B, ReelStripValues.A, ReelStripValues.Coin, ReelStripValues.C,
        ReelStripValues.D, ReelStripValues.A};

    ReelStripValues value;

   

    // Start is called before the first frame update
    public ReelStripItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, reelStrip.Count);
        value = reelStrip[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal int GetValue()
    {
        return (int) value * GetRandomCoinValue(); 
    }

    private int GetRandomCoinValue()
    {
        int coinWeight = UnityEngine.Random.Range(0, 99);

        if (coinWeight < 60)
        {
            return 50;
        }else if (coinWeight < 75)
        {
            return 75;
        }
        else if (coinWeight < 85)
        {
            return 125;
        }
        else if (coinWeight < 95)
        {
            return 275;
        }
        else if (coinWeight < 98)
        {
            return 500;
        }
        else 
        {
            return 1000;
        }
    }
}
