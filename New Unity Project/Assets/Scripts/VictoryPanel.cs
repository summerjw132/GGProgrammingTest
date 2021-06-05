using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{

    Text rewardText;

    private void Awake()
    {
        
    }

    public void Init()
    {
        rewardText = this.transform.Find("RewardText").gameObject.GetComponent<Text>();
        if (!rewardText)
        {
            Debug.LogError("Cannot find 'RewardText' Text object under " + this.gameObject.name);
        }
    }

    public void SetRewardText(string text)
    {
        rewardText.text = text;
    }
}
