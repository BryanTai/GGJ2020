using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] public SkillData SkillData;
    [HideInInspector] protected float RemainingCoolDown;
    public abstract SkillType SkillType { get; }
    public event Action OnCoolDownRefreshed;

    public virtual void CastSkill(List<TeamMate> teamMates, int targetIndex)
    {
        RemainingCoolDown = SkillData.CoolDown;
    }
    public abstract bool CanCast(List<TeamMate> teamMates, int targetIndex);

    private void Update()
    {
        if (RemainingCoolDown == 0) return;

        RemainingCoolDown = Mathf.Max(RemainingCoolDown - Time.deltaTime, 0);

        if (RemainingCoolDown == 0)
        {
            OnCoolDownRefreshed?.Invoke();
        }
    }
}
