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
        if (!CanCast(teamMates, targetIndex))
        {
            return;
        }
        base.CastSkill(teamMates, targetIndex);

        teamMates[targetIndex].Health += SkillData.HealAmount;
        teamMates[targetIndex].Mood += SkillData.MoodAmount;
        teamMates[targetIndex].ShowHealingParticles();

        for (int i = 0; i <= teamMates.Count; i++)
        {
            if (i != targetIndex)
            {
                teamMates[i].Mood -= 5;
            }
        }
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        return teamMates[targetIndex].IsAlive && this.RemainingCoolDown == 0 && teamMates[targetIndex].IsOnline;
    }
}
