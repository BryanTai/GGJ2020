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

    public List<ChatItem> chatHistory { get; private set; }
    public event Action<ChatItem> OnChatAdded;

    public void AddChat(TeamMateClass senderClass, string message)
    {
        ChatItem newChat = Instantiate(senderClass, message);
        chatHistory.Add(newChat);
        OnChatAdded?.Invoke(newChat);
    }
}
