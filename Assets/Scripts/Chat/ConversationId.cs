using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConversationId
{
    public TeamMateClass TriggerTarget;
    public int TriggerHP;

    public override bool Equals(object value)
    {
        ConversationId obj = value as ConversationId;

        return (obj != null)
            && (TriggerTarget == obj.TriggerTarget)
            && (TriggerHP == obj.TriggerHP);
    }

    public override int GetHashCode()
    {
        int hash = 27;
        hash = (13 * hash) + TriggerTarget.GetHashCode();
        hash = (13 * hash) + TriggerHP.GetHashCode();
        return hash;
    }
}
