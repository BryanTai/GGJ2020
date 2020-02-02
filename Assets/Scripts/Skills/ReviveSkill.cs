using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveSkill : Skill
{
    public override SkillType SkillType
    {
        get
        {
            return SkillType.Revive;
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
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        return teamMates[targetIndex].Health == 0 && this.RemainingCoolDown == 0;
    }
}
