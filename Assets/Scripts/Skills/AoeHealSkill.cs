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
        base.CastSkill(teamMates, targetIndex);

        for (int i = 0; i <= teamMates.Count; ++i)
        {

            if ((i == 3 && targetIndex == 0) || (i == 0 && targetIndex == 3))
            {
                if (!teamMates[i].IsAlive) continue;
                teamMates[i].Health += SkillData.HealAmount;
            }
            else if (i == targetIndex || i == (targetIndex - 1) || i == (targetIndex + 1))
            {
                if (!teamMates[i].IsAlive) continue;
                teamMates[i].Health += SkillData.HealAmount;
            }
            else
            {
                if (!teamMates[i].IsAlive) continue;
                teamMates[i].Mood -= SkillData.MoodAmount;
            }
            
        }
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        return teamMates[targetIndex].Health != 0 && this.RemainingCoolDown == 0;
    }
}
