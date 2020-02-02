using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StartingValues", order = 1)]
public class StartingValues : ScriptableObject
{
    public float MonsterHealthTimer;
    public float MonsterAttackFrequency;
    public float MonsterAttackPowerMin;
    public float MonsterAttackPowerMax;
}
