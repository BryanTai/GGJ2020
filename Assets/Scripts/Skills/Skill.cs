using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    [SerializeField] protected SkillData SkillData;
    [HideInInspector] protected float RemainingCoolDown;
    public abstract SkillType SkillType { get; }

    public abstract void CastSkill(List<TeamMate> teamMates, int targetIndex);
    public abstract bool CanCast(List<TeamMate> teamMates, int targetIndex);
}
