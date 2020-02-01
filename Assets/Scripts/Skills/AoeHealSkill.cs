using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeHealSkill : Skill
{
    public override SkillType SkillType
    {
        get
        {
            return SkillType.AoeHeal;
        }
    }

    public override void CastSkill(List<TeamMate> teamMates, int targetIndex)
    {
        if (!CanCast(teamMates, targetIndex))
        {
            return;
        }

        for (int i = Mathf.Max(0, targetIndex - 1); i <= Mathf.Min(teamMates.Count - 1, targetIndex + 1); ++i)
        {
            if (!teamMates[i].IsAlive) continue;
            teamMates[i].Health += SkillData.HealAmount;
        }

        RemainingCoolDown = SkillData.CoolDown;
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        if(teamMates[targetIndex].Health != 0 && this.RemainingCoolDown == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
