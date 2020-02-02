using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChatItem
{
    public TeamMateClass SenderClass;
    public string Message;
    public Color TextColor { get; private set; }

    public ChatItem(TeamMateClass senderClass, string message)
    {
        SenderClass = senderClass;
        Message = message;
        SetChatColor();
    }

    public void SetChatColor()
    {
        TextColor = TeamMate.GetTeamMateColor(SenderClass);
    }
}
