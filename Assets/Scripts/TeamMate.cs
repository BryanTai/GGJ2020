using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMate : Entity
{
    [SerializeField] private int MaxMood;
    [SerializeField] private TeamMateClass Class;
    [SerializeField] private ParticleSystem HealingParticles;
    public event Action<int> OnMoodChanged;
    public event Action OnRageQuit;
    
    public List<GameObject> CharacterStates = new List<GameObject>();
    public enum ActionState {Idle, Damaged, Dead, Attacking, AttackingFinished, Cheering}
    //public enum MoodState { Neutral, Happy}
    private ActionState state;
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

            if (_mood == 0)
                OnRageQuit?.Invoke();
        }
    }

    public int BiggestMood { get { return MaxMood; } }

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

    public void ShowHealingParticles()
    {
        if (HealingParticles.isPlaying)
            HealingParticles.Stop();
        HealingParticles.Play();
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
        if (!IsAlive && state != ActionState.Dead) // if player's dead
        {
            state = ActionState.Dead;
            state_time = 0;

            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 2)
                {
                    CharacterStates[i].SetActive(true);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
        else if(IsAlive)
        {
            // play the current state (that isn't idle) for a period of time
            if (state_time > 0)
            {
                state_time -= Time.deltaTime;
            }

            // swap back to idle state / second attack state
            if (state_time <= 0 && state != ActionState.Idle)
            {
                // if this character is a paladin or warrior, and the are in the attacking state
                if ((Class == TeamMateClass.Paladin || Class == TeamMateClass.Warrior) && state == ActionState.Attacking)
                {
                    state = ActionState.AttackingFinished;
                    state_time = state_timeMax;

                    for (int i = 0; i < CharacterStates.Count; i++)
                    {
                        if (i == 4)
                        {
                            CharacterStates[i].SetActive(true);
                        }
                        else
                        {
                            CharacterStates[i].SetActive(false);
                        }
                    }


                }
                else // reset state to idle
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
        }
        
    }

    public void ChangeState(ActionState new_state)
    {
        state = new_state;
        state_time = state_timeMax;

        if (state == ActionState.Damaged)
        {
            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 1)
                {
                    CharacterStates[i].SetActive(true);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
        else if (state == ActionState.Attacking) // attacking finished states are handled manually
        {
            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 3)
                {
                    CharacterStates[i].SetActive(true);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
        else if (state == ActionState.Cheering) // only warrior and paladin have cheers
        {
            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 5)
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
