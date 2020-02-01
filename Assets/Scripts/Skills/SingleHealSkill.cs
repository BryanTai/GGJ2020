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
        if(!CanCast(teamMates, targetIndex))
        {
            return;
        }



        RemainingCoolDown = SkillData.CoolDown;
    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        if (teamMates[targetIndex].Health != 0 && this.RemainingCoolDown == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
