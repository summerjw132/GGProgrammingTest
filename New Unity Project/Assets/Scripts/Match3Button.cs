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
            //this.GetComponent<Image>().color = Color.grey;
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
                //this.GetComponent<Image>().color = Color.black;
                match3Item = Match3Item.Item.Mini;
                this.GetComponent<Image>().sprite = controller.miniSprite;

            }else if(random > 50 && random < 75)
            {
                //this.GetComponent<Image>().color = Color.red;
                match3Item = Match3Item.Item.Minor;
                this.gameObject.GetComponent<Image>().sprite = controller.minorSprite;
            }
            else if (random > 75 && random < 90)
            {
                //this.GetComponent<Image>().color = Color.yellow;
                match3Item = Match3Item.Item.Maxi;
                this.gameObject.GetComponent<Image>().sprite = controller.maxiSprite;
            }
            else if (random > 90 && random < 98)
            {
                //this.GetComponent<Image>().color = Color.green;
                match3Item = Match3Item.Item.Major;
                this.gameObject.GetComponent<Image>().sprite = controller.majorSprite;
            }
            else
            {
                //this.GetComponent<Image>().color = Color.cyan;
                match3Item = Match3Item.Item.Grand;
                this.gameObject.GetComponent<Image>().sprite = controller.grandSprite;
            }
            controller.IncrementRewardCounterDict(match3Item.ToString());

            SetText(match3Item.ToString());

            
                //Check if we got a match 3
                //Yes, display victory panel
                //No,
                //  display "No Match" panel
                //  reset buttons
                if (controller.CheckIfMatch3(match3Item))
                {

                    StartCoroutine(DelayGameBoard(1, true));
                }
                else
                {

                    //StartCoroutine(DelayGameBoard(1, false));
                }
        }
        
    }

    public void SetText(string text)
    {
        this.GetComponentInChildren<Text>().text = text;
    }

    // Temporarily set the game board to not active so the player cannot interact with gameboard items
    IEnumerator DelayGameBoard( float delayTime, bool victoryPanelActive)
    {
        
        controller.SetIsActive(false);
        yield return new WaitForSeconds(delayTime);
        controller.ToggleVictoryPanel(victoryPanelActive);
        controller.ResetAllButtons();
        controller.SetIsActive(true);
        
    }


}
