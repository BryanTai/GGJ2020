using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHealSkill : Skill
{
    public int HealAmount { get; private set; }

    public override SkillType SkillType
    {
        get
        {
            return SkillType.SingleHeal;
        }
    }

    public override void CastSkill(List<TeamMate> teamMates, int targetIndex)
    {
        base.CastSkill(teamMates, targetIndex);

        teamMates[targetIndex].Health += SkillData.HealAmount;
        teamMates[targetIndex].Mood += SkillData.MoodAmount;
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        return teamMates[targetIndex].Health != 0 && this.RemainingCoolDown == 0;
    }
}
