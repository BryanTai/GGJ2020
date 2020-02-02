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
    public TeamMate teamMate;

    public override void InitButton(int index)
    {
        base.InitButton(index);
        SetHealth(teamMate.MaxHealth);
        teamMateClass = (TeamMateClass) index;
        SetFaceFromMood(TeamMateMood.NEUTRAL);
        teamMate.OnHealthChanged += SetHealth;
    }

    public void SetHealth(int newHealth)
    {
        healthBar.value = newHealth / (float) teamMate.MaxHealth;
    }

    public void SetFaceFromMood(TeamMateMood mood)
    {
        uiButton.image.sprite = faceReferences.GetFaceForMood(mood);
    }

    
}
