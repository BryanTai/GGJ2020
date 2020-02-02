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

    public Conversation GetRandomConvoById(ConversationId id)
    {
        List<Conversation> convoList = ConversationDict[id];

        if (convoList == null)
            return null;

        Conversation res;

        do
        {
            int index = Random.Range(0, convoList.Count - 1);
            res = convoList[index];
        }
        while (res.IsUsed == true);

        return res;
    }
}
