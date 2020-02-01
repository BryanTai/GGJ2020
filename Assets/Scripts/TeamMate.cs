using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMate : Entity
{
    [SerializeField] private int MaxMood;
    public event Action<int> OnMoodChanged;

    private int _mood;
    public int Mood
    {
        get
        {
            return _mood;
        }
        set
        {
            _mood = Mathf.Min(MaxMood, _mood);
            _mood = Mathf.Max(0, _mood);
            OnMoodChanged(_mood);
        }
    }

    private void Awake()
    {
        Mood = MaxMood;
    }

    public bool IsAlive
    {
        get
        {
            return Health != 0 && Mood != 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
