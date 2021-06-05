using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match3Controller : MonoBehaviour
{

    private int selectionCount = 0;
    private List<Match3Button> match3Buttons;
    private List<Match3Button> selectedMatch3Buttons;
    private bool isActive = true;
    [SerializeField]
    private Dictionary<string, string> rewardDictionary;

    void Start()
    {
        match3Buttons = new List<Match3Button>();
        selectedMatch3Buttons = new List<Match3Button>();


        //Get all buttons in the page
        for (int i=0; i<transform.childCount; i++)
        {
            

            match3Buttons.Add(transform.GetChild(i).GetComponent<Match3Button>());

        }


        ResetAllButtons();
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive( bool isActive)
    {
        this.isActive = isActive;
    }

    //Flips all buttons back to facedown position
    public void ResetAllButtons()
    {
        if(match3Buttons.Count > 0)
        {
            for (int i = 0; i < 15; i++)
            {

                match3Buttons[i].SetFacedown(true);
                match3Buttons[i].SetText("");


            }
            
        }

        selectedMatch3Buttons.Clear(); //Clear out our selected buttons list

    }

    //Used to see if 3 items were selected
    public bool Match3ButtonQuotaMet()
    {
        
        
        return selectedMatch3Buttons.Count >= 3;
    }

    //Used to add a button to our list
    public void IncrementButtonSelectionCount(Match3Button button)
    {
        selectedMatch3Buttons.Add(button);
    }

    public bool CheckIfMatch3()
    {
        Match3Item.Item match = selectedMatch3Buttons[0].GetMatch3ItemStatus();
        for(int i = 1; i< selectedMatch3Buttons.Count; i++)
        {
            if (!selectedMatch3Buttons[i].GetMatch3ItemStatus().Equals(match))
            {
                Debug.Log("No Match!");
                return false;
            }
        }


        Debug.Log("We have a match 3 of " + match.ToString() + "!");
        return true;
    }
}
