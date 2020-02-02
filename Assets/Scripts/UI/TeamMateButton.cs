using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMateButton : UIButton
{
    public Slider healthBar;
    public TeammateFaces faceReferences;
    public TeamMateClass teamMateClass;

    public override void InitButton(int index)
    {
        base.InitButton(index);
        SetHealthPercentage(1f);
        teamMateClass = (TeamMateClass) index;
        SetFaceFromType(TeammateFaces.FaceType.NEUTRAL);
    }

    public void SetHealthPercentage(float newHealth)
    {
        healthBar.value = newHealth;
    }

    public void SetFaceFromType(TeammateFaces.FaceType type)
    {
        uiButton.image.sprite = faceReferences.GetFaceForFaceType(type);
    }
}
