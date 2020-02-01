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

    }

    public override bool CanCast(List<TeamMate> teamMates, int targetIndex)
    {
        return true;
    }
}
