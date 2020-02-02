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

        int leftNeighbour = (targetIndex - 1) % teamMates.Count;
        leftNeighbour = leftNeighbour < 0 ? teamMates.Count + leftNeighbour : leftNeighbour;
        int rightNeighbour = (targetIndex + 1) % teamMates.Count;
        rightNeighbour = rightNeighbour < 0 ? teamMates.Count + rightNeighbour : rightNeighbour;

        for (int i = 0; i < teamMates.Count; ++i)
        {
            if((i == leftNeighbour || i == targetIndex || i == rightNeighbour) && teamMates[i].IsAlive)
            {
                teamMates[i].Health += SkillData.HealAmount;
                teamMates[i].ShowHealingParticles();
            }
            else if(teamMates[i].IsAlive)
            {
                teamMates[i].Mood -= SkillData.MoodAmount;
            }
        }
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        return teamMates[targetIndex].IsAlive && this.RemainingCoolDown == 0;
    }
}
