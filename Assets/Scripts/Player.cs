using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Skill> Skills = new List<Skill>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Skill GetSkillByType(SkillType skillType)
    {
        foreach(Skill skill in Skills)
        {
            if (skill.SkillType == skillType) return skill;
        }
        return null;
    }
}
