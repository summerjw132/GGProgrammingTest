using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match3Controller : MonoBehaviour
{

    private List<Match3Button> match3Buttons;
    private List<Match3Button> selectedMatch3Buttons;
    private bool isActive = true;                       // the board cannot be interacted with if set to false
    [SerializeField]
    private VictoryPanel victoryPanel;

    [SerializeField]
    public Sprite miniSprite;
    [SerializeField]
    public Sprite minorSprite;
    [SerializeField]
    public Sprite majorSprite;
    [SerializeField]
    public Sprite maxiSprite;
    [SerializeField]
    public Sprite grandSprite;

    private Sprite defaultSprite;

    private Match3Item.Item match;

    //Used to keep track of revealed rewards on the board
    private Dictionary<string, int> rewardCountDictionary = new Dictionary<string, int>();




    void Start()
    {
        match3Buttons = new List<Match3Button>();
        selectedMatch3Buttons = new List<Match3Button>();
        isActive = true;

        //set up the dictionary
        rewardCountDictionary.Add("Mini", 0);
        rewardCountDictionary.Add("Minor", 0);
        rewardCountDictionary.Add("Maxi", 0);
        rewardCountDictionary.Add("Major", 0);
        rewardCountDictionary.Add("Grand", 0);

        //Get & deactivate the victory panel
        //victoryPanel = this.transform.Find("VictoryPanel").gameObject.GetComponent<VictoryPanel>();

        if (victoryPanel)
        {
            victoryPanel.Init();
            victoryPanel.gameObject.SetActive(false);
        }
            
        //Get all buttons in the page
        for (int i=0; i<transform.childCount; i++)
        {
            

            match3Buttons.Add(transform.GetChild(i).GetComponent<Match3Button>());

        }

        defaultSprite = match3Buttons[0].gameObject.GetComponent<Image>().sprite;
        ResetAllButtons();
    }

    public void IncrementRewardCounterDict(string key)
    {
        if (rewardCountDictionary.ContainsKey(key))
        {
            rewardCountDictionary[key]++;
            Debug.Log(key + " count: " + rewardCountDictionary[key]);
            
            
        }
        else
        {
            throw new KeyNotFoundException("The key '" + key + "' was not found");
        }
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
                match3Buttons[i].gameObject.GetComponent<Image>().sprite = defaultSprite;

            }
            
        }

        ClearRewardCountDictionary(); //Clear out our selected buttons list

    }

    private void ClearRewardCountDictionary()
    {
        foreach (string key in new List<string>(rewardCountDictionary.Keys))
        {
            rewardCountDictionary[key] = 0;
        }
    }

    //Used to see if 3 of the same reward item were selected
    public bool CheckIfMatch3(Match3Item.Item match3Item)
    {
        
        if (rewardCountDictionary.ContainsKey(match3Item.ToString()))
        {

            if(rewardCountDictionary[match3Item.ToString()] >= 3)  //IF a match is found, store the match and return true
            {

                match = match3Item;
                return true;
            }
        }
        else
        {
            throw new KeyNotFoundException("The key '" + match3Item.ToString() + "' was not found");
        }
        return false;
    }

    //Used to add a button to our list
    public void IncrementButtonSelectionCount(Match3Button button)
    {
        selectedMatch3Buttons.Add(button);
    }

    //public bool CheckIfMatch3()
    //{
    //    //match = selectedMatch3Buttons[0].GetMatch3ItemStatus();
    //    //for(int i = 1; i< selectedMatch3Buttons.Count; i++)
    //    //{
    //    //    if (!selectedMatch3Buttons[i].GetMatch3ItemStatus().Equals(match))
    //    //    {
    //    //        //Debug.Log("No Match!");
    //    //        return false;
    //    //    }
    //    //}


        

        

    //    return true;
    //}

    public void ToggleVictoryPanel(bool toggleValue)
    {
        //Reactivate and properly label the reward text
        victoryPanel.gameObject.SetActive(toggleValue);

        if (toggleValue)
        {
            victoryPanel.SetRewardText(match.ToString());
        }
    }

    public void ReplayGame()
    {
        ResetAllButtons();
        if (victoryPanel.gameObject.activeSelf)
        {

            ToggleVictoryPanel(false);
        }
    }

    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}
