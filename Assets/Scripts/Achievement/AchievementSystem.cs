using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    public GameObject achievementPrefab;

    public GameObject generalAchievementMenu;
    public GameObject otherAchievementMenu;

    public GameObject visualAchievement;

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlokedSprite;


    public static AchievementSystem instance;

    public static AchievementSystem Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementSystem>();
            }
            return AchievementSystem.instance;
        }
    }                        

    // Use this for initialization
    void Start ()
    {
        CreateAchievement("General", "Press W", "Press W to Unlock", 200);
        CreateAchievement("General", "222", "dddss", 100);
        CreateAchievement("General", "333", "gggs", 250);
        CreateAchievement("General", "444", "kkk", 300);

        CreateAchievement("Other", "555", "uuu", 400);
        CreateAchievement("Other", "666", "uoooo", 300);
        CreateAchievement("Other", "777", "uyyyuu", 2100);
        CreateAchievement("Other", "888", "wwww", 100);
        CreateAchievement("Other", "999", "ummmmmuu", 500);

        otherAchievementMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.W))
        {
            //use same name as achivement title
            EarnAchievement("Press W");
        }
	}

    public void EarnAchievement(string title)
    {
        if(achievements[title].EarnAchievement())
        {
            //Do Somting
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("AchievementsMenu", achievement, title);

            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    public void CreateAchievement(string parent, string title,string description, int reward)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        Achievement newAchievement = new Achievement(title, description, reward, achievement);

        achievements.Add(title, newAchievement);

        SetAchievementInfo(parent, achievement, title);
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);

        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Reward.ToString();
    }



    //Category BTN
    public void OnGeneralAchBTNClick()
    {
        if(generalAchievementMenu.active == false)
        {
            generalAchievementMenu.SetActive(true);
            otherAchievementMenu.SetActive(false);
        }
    }

    public void OnOtherAchBTNClick()
    {
        if (otherAchievementMenu.active == false)
        {
            generalAchievementMenu.SetActive(false);
            otherAchievementMenu.SetActive(true);
        }
    }
}
