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

    private int happyMoodThreshold;
    private int neutralMoodThreshold;

    public Text debugMoodText; //TODO REMOVE THIS

    public override void InitButton(int index)
    {
        SetHealth(teamMate.MaxHealth);
        SetMoodIconFromMoodValue(teamMate.BiggestMood);

        teamMateClass = (TeamMateClass) index;
        SetButtonImageFromMood(TeamMateMood.NEUTRAL);
        teamMate.OnHealthChanged += SetHealth;
        teamMate.OnMoodChanged += SetMoodIconFromMoodValue;

        happyMoodThreshold = (int) (teamMate.BiggestMood * 0.66f);
        neutralMoodThreshold = (int) (teamMate.BiggestMood * 0.33f);
    }

    public void SetHealth(int newHealth)
    {
        healthBar.value = newHealth / (float) teamMate.MaxHealth;
    }

    public void SetButtonImageFromMood(TeamMateMood mood)
    {
        uiButton.image.sprite = faceReferences.GetFaceForMood(mood);
    }

    public void SetMoodIconFromMoodValue(int mood)
    {
        if (moodIconImage == null) return;
        int maxMood = teamMate.BiggestMood;

        debugMoodText.text = mood.ToString();

        if(mood > happyMoodThreshold)
        {
            moodIconImage.sprite = moodIcons[0];
        }
        else if(mood > neutralMoodThreshold)
        {
            moodIconImage.sprite = moodIcons[1];
        }
        else
        {
            moodIconImage.sprite = moodIcons[2];
        }
    }
}
