using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatController
{
    private static readonly ChatController _instance = new ChatController();
    static ChatController() {}
    private ChatController() {}

    public static ChatController Instance
    {
        get
        {
            return _instance;
        }
    }

    private ConversationLoader _convLoader;
    public ConversationLoader ConvLoader
    {
        get
        {
            if(_convLoader == null)
            {
                _convLoader = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ConversationLoader>();
            }
            return _convLoader;
        }
    }
    
    public event Action<ChatItem> OnChatAdded;
    public event Action<Conversation> OnConversationAdded;

    public void AddChat(TeamMateClass senderClass, string message)
    {
        ChatItem newChat = new ChatItem(senderClass, message);
        OnChatAdded?.Invoke(newChat);
    }

    public void AddConversation(Conversation newConvo)
    {
        newConvo.IsUsed = true;
        OnConversationAdded?.Invoke(newConvo);
    }
}
