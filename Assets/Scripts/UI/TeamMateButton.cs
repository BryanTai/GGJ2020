using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMateButton : UIButton
{
    public Slider healthBar;

    public override void InitButton(int index)
    {
        base.InitButton(index);
        SetHealthPercentage(1f);
    }

    public void SetHealthPercentage(float newHealth)
    {
        healthBar.value = newHealth;
    }
}
