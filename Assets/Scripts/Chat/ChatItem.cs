using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatItem
{
    public TeamMateClass SenderClass { get; private set; }
    public string Message { get; private set; }
    public Color TextColor { get; private set; }

    public ChatItem(TeamMateClass senderClass, string message)
    {
        SenderClass = senderClass;
        Message = message;

        switch(senderClass)
        {
            case TeamMateClass.Rogue:
                TextColor = Color.yellow;
                break;
            case TeamMateClass.Paladin:
                TextColor = new Color32(232, 0, 254, 1);
                break;
            case TeamMateClass.Wizard:
                TextColor = Color.blue;
                break;
            case TeamMateClass.Warrior:
                TextColor = new Color32(139, 69, 19, 1);
                break;
        }
    }
}
