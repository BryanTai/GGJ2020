using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMateButton : UIButton
{
    public Slider healthBar;
    public TeammateFaces faceReferences;
    public TeamMateClass teamMateClass;
    [HideInInspector] public TeamMate teamMate;

    public Image moodIconImage;
    public List<Sprite> moodIcons;

    private int extaticMoodThreshold;
    private int happyMoodThreshold;
    private int neutralMoodThreshold;
    private int angryMoodThreshold;

    public override void InitButton(int index)
    {
        SetHealth(teamMate.MaxHP);

        teamMateClass = (TeamMateClass) index;
        SetButtonImageFromMood(TeamMateMood.NEUTRAL);
        teamMate.OnHealthChanged += SetHealth;
        teamMate.OnMoodChanged += SetMoodIconFromMoodValue;

        extaticMoodThreshold = (int)(teamMate.BiggestMood);
        happyMoodThreshold = (int) (teamMate.BiggestMood * 0.70f);
        neutralMoodThreshold = (int) (teamMate.BiggestMood * 0.50f);
        angryMoodThreshold = (int)(teamMate.BiggestMood * 0.30f);
        SetMoodIconFromMoodValue(teamMate.BiggestMood);
    }

    public void SetHealth(int newHealth)
    {
        healthBar.value = newHealth / (float) teamMate.MaxHP;
    }

    public void SetButtonImageFromMood(TeamMateMood mood)
    {
        uiButton.image.sprite = faceReferences.GetFaceForMood(mood);
    }

    public void SetMoodIconFromMoodValue(int mood)
    {
        if (moodIconImage == null) return;
        int maxMood = teamMate.BiggestMood;

        if(mood == extaticMoodThreshold)
        {
            moodIconImage.sprite = moodIcons[0];
        }
        else if(mood > happyMoodThreshold)
        {
            moodIconImage.sprite = moodIcons[1];
        }
        else if(mood > neutralMoodThreshold)
        {
            moodIconImage.sprite = moodIcons[2];
        }
        else if(mood > angryMoodThreshold)
        {
            moodIconImage.sprite = moodIcons[3];
        }
        else
        {
            moodIconImage.sprite = moodIcons[4];
        }
    }
}
