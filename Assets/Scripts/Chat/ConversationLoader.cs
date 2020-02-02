using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader : MonoBehaviour
{
    public List<Conversation> Conversations = new List<Conversation>();
    public Conversation StartingConvo;
    [HideInInspector] public Dictionary<ConversationId, List<Conversation>> ConversationDict = new Dictionary<ConversationId, List<Conversation>>();

    private void Awake()
    {
        foreach(Conversation conv in Conversations)
        {
            conv.IsUsed = false;
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
