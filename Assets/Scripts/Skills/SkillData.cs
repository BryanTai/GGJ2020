using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
public class SkillData : ScriptableObject
{
    public int CoolDown;
    public int HealAmount;
    public int MoodAmount;
    public AudioClip skillSE;
    public Sprite SkillButtonReleased;
    public Sprite SkillButtonPressed;
}
