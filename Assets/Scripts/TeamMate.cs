using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMate : Entity
{
    [SerializeField] private int MaxMood;
    [SerializeField] private TeamMateClass Class;
    public event Action<int> OnMoodChanged;
    public List<GameObject> CharacterStates = new List<GameObject>();
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

    public TeamMateClass TMClass { get { return Class; } }

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

        //initialize character states per character
        for(int i = 0; i < CharacterStates.Count; i++)
        {
            if(i == 0)
            {
                CharacterStates[i].SetActive(true);
            }
            else
            {
                CharacterStates[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
