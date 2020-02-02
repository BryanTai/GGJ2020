using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConversationId
{
    public TeamMateClass TriggerTarget;
    public int TriggerMood;

    public override bool Equals(object value)
    {
        ConversationId obj = value as ConversationId;

        return (obj != null)
            && (TriggerTarget == obj.TriggerTarget)
            && (TriggerMood == obj.TriggerMood);
    }

    public override int GetHashCode()
    {
        int hash = 27;
        hash = (13 * hash) + TriggerTarget.GetHashCode();
        hash = (13 * hash) + TriggerMood.GetHashCode();
        return hash;
    }
}
