using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private bool unlocked;

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int reward;

    public int Reward
    {
        get { return reward; }
        set { reward = value; }
    }

    private GameObject achievementRef;

    public GameObject AchievementRef
    {
        get { return achievementRef; }
        set { achievementRef = value; }
    }


    public Achievement(string title, string description, int reward, GameObject achievementRef)
    {
        this.title = title;
        this.description = description;
        this.unlocked = false;
        this.reward = reward;
        this.achievementRef = achievementRef;
    }

    public bool EarnAchievement()
    {
        if(!unlocked)
        {
            achievementRef.GetComponent<Image>().sprite = AchievementSystem.Instance.unlokedSprite;
            unlocked = true; 
            return true;
        }
        return false;
    }
}
