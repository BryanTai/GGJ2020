using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatItem
{
    public string PlayerName { get; private set; }
    public string Message { get; private set; }
    public Color TextColor { get; private set; }

    public ChatItem(string playerName, string message, Color color)
    {
        PlayerName = playerName;
        Message = message;
        TextColor = color;
    }
}
