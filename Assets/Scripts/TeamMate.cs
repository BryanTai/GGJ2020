using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TeamMate : Entity
{
    [HideInInspector] public int BiggestMood;
    [SerializeField] private TeamMateClass Class;
    [SerializeField] private ParticleSystem HealingParticles;
    public event Action<int> OnMoodChanged;
    public event Action OnRageQuit;

    public List<GameObject> CharacterStates = new List<GameObject>();
    public enum ActionState {Idle, Damaged, Dead, Attacking, AttackingFinished, Cheering, Offline}
    //public enum MoodState { Neutral, Happy}
    private ActionState state;
    public float state_timeMax;
    private float state_time;
    public float attack_timeMax;
    public float attack_timeMin;
    private float attack_time;
    private bool attack_prepare;

    private int _mood;
    private int _moodFrameCounter;

    private int halfHealth;

    public int Mood
    {
        get
        {
            return _mood;
        }
        set
        {
            int oldMood = _mood;
            _mood = Mathf.Min(BiggestMood, value);
            _mood = Mathf.Max(0, _mood);

            if(oldMood != _mood)
                OnMoodChanged?.Invoke(_mood);

            if (_mood == 0)
                OnRageQuit?.Invoke();
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
            return Health != 0;
        }
    }

    public bool IsOnline
    {
        get
        {
            return Mood != 0;
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
        Health = MaxHP = gc.initValues.TeamMateMaxHP;
        halfHealth = MaxHP / 2;
        Mood = BiggestMood = gc.initValues.TeamMateMaxMood;
        attack_prepare = false;

        //initialize character states per character
        for (int i = 0; i < CharacterStates.Count; i++)
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
        if (!gc.GameStarted)
            return;

        if (!IsOnline && state != ActionState.Offline) // if player's offline
        {
            state = ActionState.Offline;
            state_time = 0;
            attack_time = 0;
            attack_prepare = false;

            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 2)
                {
                    CharacterStates[i].SetActive(true);
                    ChangeFace(TeamMateMood.WORSE);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
        else if (!IsAlive && state != ActionState.Dead) // if player's dead
        {
            state = ActionState.Dead;
            state_time = 0;
            attack_time = 0;
            attack_prepare = false;

            for (int i = 0; i < CharacterStates.Count; i++)
            {
                if (i == 2)
                {
                    CharacterStates[i].SetActive(true);
                    ChangeFace(TeamMateMood.WORSE);
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
                    attack_time = 0;
                    attack_prepare = false;

                    for (int i = 0; i < CharacterStates.Count; i++)
                    {
                        if (i == 4)
                        {
                            CharacterStates[i].SetActive(true);
                            TeamMateMood idleMood = (Health > halfHealth) ? TeamMateMood.NEUTRAL : TeamMateMood.BAD;
                            ChangeFace(idleMood);
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
                    attack_time = 0;
                    attack_prepare = false;

                    for (int i = 0; i < CharacterStates.Count; i++)
                    {
                        if (i == 0)
                        {
                            CharacterStates[i].SetActive(true);
                            TeamMateMood idleMood = (Health > halfHealth) ? TeamMateMood.NEUTRAL : TeamMateMood.BAD;
                            ChangeFace(idleMood);
                        }
                        else
                        {
                            CharacterStates[i].SetActive(false);
                        }
                    }
                }
            }

            

            // party members attack
            if (state == ActionState.Idle && !attack_prepare && !gc.isWon())
            {
                attack_time = Random.Range(attack_timeMin, attack_timeMax);
                attack_prepare = true;
            }
            else if (state == ActionState.Idle && attack_prepare)
            {
                attack_time -= Time.deltaTime;
                if(attack_time <= 0)
                {
                    attack_time = 0;
                    attack_prepare = false;
                    ChangeState(ActionState.Attacking);
                    ChangeFace(TeamMateMood.ATTACKING);
                }
            }
        }

        if (state == ActionState.Dead)
        {
            _moodFrameCounter += 1;
            if (_moodFrameCounter == 30)
            {
                Mood -= 3;
                _moodFrameCounter = 0;
            }
        }
    }

    public void ChangeFace(TeamMateMood newMood)
    {
        gc.playerControlsUI.ChangeTeamMateButtonFace(this, newMood);
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
                    ChangeFace(TeamMateMood.WORSE);
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
                    ChangeFace(TeamMateMood.ATTACKING);
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
                    ChangeFace(TeamMateMood.HAPPY);
                }
                else
                {
                    CharacterStates[i].SetActive(false);
                }
            }
        }
    }

    public static Color GetTeamMateColor(TeamMateClass tmclass)
    {
        switch (tmclass)
        {
            case TeamMateClass.Rogue:
                return Color.red;
            case TeamMateClass.Paladin:
                return Color.black;
            case TeamMateClass.Wizard:
                return Color.blue;
            case TeamMateClass.Warrior:
                return new Color32(67, 9, 137, 255);
            default:
                return Color.white;
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
