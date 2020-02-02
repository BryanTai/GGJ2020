using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMate : Entity
{
    [SerializeField] private int MaxMood;
    [SerializeField] private TeamMateClass Class;
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
            _mood = Mathf.Min(MaxMood, value);
            _mood = Mathf.Max(0, value);
            OnMoodChanged?.Invoke(_mood);
        }
    }

    private void Awake()
    {
       
    }

    public bool IsAlive
    {
        get
        {
            return Health != 0 && Mood != 0;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Mood = MaxMood;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
