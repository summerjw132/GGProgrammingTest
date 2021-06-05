using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Note: Monobehaviour was only used to test this object in Unity editor.
 *      The code can be run as a standalone script by running
 *      CalculateCoinTotal()
 */
public class ReelStripGrid : MonoBehaviour
{

    private ReelStripItem[,] reelStripGrid = new ReelStripItem[3,5];
    private int coinValueTotal = 0;

    private void Start()
    {
        CalculateCoinTotal();
    }

    //Checks grid for coins and accumulate the coin values
    private void CalculateCoinTotal()
    {
        ResetCoinTotal(); // Set coin total to 0 before calculation
        bool isAllCoinsOnly = true;
        string debugTable = "";

        //Accumulate the total coin value of our reelStrip table
        for (int x = 0; x<3; x++){
            for(int y = 0; y<5; y++)
            {
                reelStripGrid[x, y] = new ReelStripItem();
                int itemValue = reelStripGrid[x, y].GetValue();
                coinValueTotal += itemValue;

                if(itemValue <= 0)
                {
                    isAllCoinsOnly = false;
                }

                debugTable += itemValue + ", ";

                
            }
            debugTable += "\n";
            
        }

        //view the coin table in console
        Debug.Log(debugTable);

        if (isAllCoinsOnly)
        {
            Debug.Log("Initial coin value = " + coinValueTotal);
            //apply extra multiplier to coin total
            coinValueTotal = coinValueTotal * GetFinalMultiplier();

            
        }

        Debug.Log("Total coin value (after multiplier): " + coinValueTotal);
    }


    // Reset the coin total back to 0
    private void ResetCoinTotal()
    {
        coinValueTotal = 0;
    }

    private int GetFinalMultiplier()
    {
        int weight = UnityEngine.Random.Range(0, 99);


        if (weight < 60)
        {
            return 2;
        }
        else if (weight < 80)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}
