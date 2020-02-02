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
    public enum ActionState {Idle, Damaged, Attacking, AttackingFinished, Cheering}
    //public enum MoodState { Neutral, Happy}
    public ActionState state;
    public float state_timeMax;
    private float state_time;

    private int _mood;
    public int Mood
    {
        get
        {
            return _mood;
        }
        set
        {
            int oldMood = _mood;
            _mood = Mathf.Min(MaxMood, value);
            _mood = Mathf.Max(0, _mood);

            if(oldMood != _mood)
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
        // play the current state (that isn't idle) for a period of time
        if(state_time > 0)
        {
            state_time -= Time.deltaTime;
        }

        // swpa back to idle state
        if(state_time <= 0)
        {
            state = ActionState.Idle;
            state_time = 0;

            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 0)
                {
                    CharacterStates[i].SetActive(true);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
    }

    public void ChangeState(ActionState new_state)
    {
        state = new_state;
        state_time = state_timeMax;
    }
    /*
    public void SetStateDamaged()
    {
        state_time = state_timeMax;
        state_damageTaken = true;
        state_attacking = false;
        state_cheering = false;

}
    public void SetStateAttacking()
    {
        state_time = state_timeMax;
        state_damageTaken = false;
        state_attacking = true;
        state_cheering = false;
    }
    public void SetStateCheering()
    {
        state_time = state_timeMax;
        state_damageTaken = false;
        state_attacking = false;
        state_cheering = true;
    }*/
}
