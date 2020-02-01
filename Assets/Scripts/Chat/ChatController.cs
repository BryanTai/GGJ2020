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

    public void AddChat(string senderName, string message, Color color)
    {
        ChatItem newChat = new ChatItem(senderName, message, color);
        chatHistory.Add(newChat);
        OnChatAdded?.Invoke(newChat);
    }
}
