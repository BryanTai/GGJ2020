using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader : MonoBehaviour
{
    [SerializeField] private List<Conversation> Conversations = new List<Conversation>();
    [HideInInspector] public Dictionary<ConversationId, List<Conversation>> ConversationDict = new Dictionary<ConversationId, List<Conversation>>();

    private void Awake()
    {
        foreach(Conversation conv in Conversations)
        {
            conv.SetChatColors();
            ConversationId key = conv.ID;
            if(ConversationDict.ContainsKey(key))
            {
                ConversationDict[key].Add(conv);
            }
            else
            {
                ConversationDict[key] = new List<Conversation> { conv };
            }
        }
    }

    void Start()
    {
        
    }
}
