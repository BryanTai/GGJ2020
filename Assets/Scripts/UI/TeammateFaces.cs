using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TeammateFaces", order = 1)]
public class TeammateFaces : ScriptableObject
{
    [SerializeField]
    public enum FaceType
    {
        ATTACKING,
        HAPPY,
        BAD,
        WORSE,
        NEUTRAL,
    }

    public Sprite[] Faces;

    public Sprite GetFaceForFaceType(FaceType type)
    {
        return Faces[(int)type];
    }
}
