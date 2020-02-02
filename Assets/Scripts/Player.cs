using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Skill> Skills = new List<Skill>();

    public List<GameObject> CharacterStates = new List<GameObject>();
    public enum ActionState { Idle, Dead, Attacking }
    private ActionState state;
    public float state_timeMax;
    private float state_time;

    public void Start()
    {
        for (int i = 0; i < CharacterStates.Count; i++)
        {
            if (i == 0)
            {
                CharacterStates[i].SetActive(true);
            }
            else
            {
                CharacterStates[i].SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (state_time > 0)
        {
            state_time -= Time.deltaTime;
        }

        if (1 == 0) // TODO: check if all other players are dead
        {
            state = ActionState.Dead;
            state_time = 0;

            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 2)
                {
                    CharacterStates[i].SetActive(true);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
        else if (state_time <= 0 && state != ActionState.Idle) // reset state to idle
        {
            state = ActionState.Idle;
            state_time = 0;

            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 0)
                {
                    CharacterStates[i].SetActive(true);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
    }

    public Skill GetSkillByType(SkillType skillType)
    {
        foreach(Skill skill in Skills)
        {
            if (skill.SkillType == skillType) return skill;
        }
        return null;
    }

    public void AttackingState()
    {
        state = ActionState.Attacking;
        state_time = state_timeMax;
        
        for (int i = 0; i < CharacterStates.Count; i++)
        {
            if (i == 1)
            {
                CharacterStates[i].SetActive(true);
            }
            else
            {
                CharacterStates[i].SetActive(false);
            }
        }
    }
}
