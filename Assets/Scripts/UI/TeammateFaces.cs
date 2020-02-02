using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TeammateFaces", order = 1)]
public partial class TeammateFaces : ScriptableObject
{
    public Sprite[] Faces;
    public Sprite GetFaceForMood(TeamMateMood type)
    {
        return Faces[(int)type];
    }
}
