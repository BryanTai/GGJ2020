using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Conversation")]
public class Conversation : ScriptableObject
{
    public ConversationId ID;
    public List<ChatItem> ChatItems = new List<ChatItem>();
}
