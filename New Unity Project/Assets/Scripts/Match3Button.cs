using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match3Button : MonoBehaviour
{
    bool facedown = true;
    private Match3Controller controller;
    private Match3Item.Item match3Item;
    

    // Start is called before the first frame update
    void Start()
    {
        //Get the parent
        controller = transform.parent.GetComponent<Match3Controller>();
        
    }

    public Match3Item.Item GetMatch3ItemStatus()
    {
        return match3Item;
    }

    public void SetFacedown( bool facedown)
    {
        this.facedown = facedown;
        if (facedown)
        {
            this.GetComponent<Image>().color = Color.grey;
            //Debug.Log("I'm facedown!");
        }
    }

    public void FlipOver()
    {

        

        //If the button is facedown && we haven't selected 3 or more buttons
        if (facedown && controller.GetIsActive())
        {
            facedown = false;

            controller.IncrementButtonSelectionCount(this);//Add this button to our selection count

            int random = Random.Range(0, 99);

            if(random < 50)
            {
                this.GetComponent<Image>().color = Color.black;
                match3Item = Match3Item.Item.Mini;

            }else if(random > 50 && random < 75)
            {
                this.GetComponent<Image>().color = Color.red;
                match3Item = Match3Item.Item.Minor;
            }
            else if (random > 75 && random < 90)
            {
                this.GetComponent<Image>().color = Color.yellow;
                match3Item = Match3Item.Item.Maxi;
            }
            else if (random > 90 && random < 98)
            {
                this.GetComponent<Image>().color = Color.green;
                match3Item = Match3Item.Item.Major;
            }
            else
            {
                this.GetComponent<Image>().color = Color.cyan;
                match3Item = Match3Item.Item.Grand;
            }

            SetText(match3Item.ToString());

            //If we met our button quota (3 buttons), 
            if (controller.Match3ButtonQuotaMet())
            {
                //Check if we got a match 3
                //Yes, display victory panel
                //No,
                //  display "No Match" panel
                //  reset buttons
                controller.CheckIfMatch3();
                StartCoroutine(DelayGameBoard(1));
                
            }
        }
        
    }

    public void SetText(string text)
    {
        this.GetComponentInChildren<Text>().text = text;
    }

    // Temporarily set the game board to not active so the player cannot interact with gameboard items
    IEnumerator DelayGameBoard( float delayTime)
    {
        
        controller.SetIsActive(false);
        yield return new WaitForSeconds(delayTime);
        controller.ResetAllButtons();
        controller.SetIsActive(true);
        
    }


}
