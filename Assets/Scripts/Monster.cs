using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : Entity
{
    [SerializeField]
    private ParticleSystem FlameAttackParticles;

    private float monsterHealth;
    private float attackFreq;
    private float attackFreqTime;
    private float attackPower;
    private float healthTimer;
    private float healthTimerInc;
    private float alivePartyMembers;
    private float totalPartyMembers;
    private bool isDead;
    private int _monsterDamageFrameCount;

    private List<TeamMate> viableTargets = new List<TeamMate>();
    private TeamMate currentTarget;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Grab Variables
        Health = MaxHP;
        monsterHealth = MaxHP = gc.initValues.MonsterMaxHP;
        attackFreq = gc.initValues.MonsterAttackFrequency;
        attackFreqTime = 0;
        attackPower = gc.initValues.MonsterAttackPowerMin;
        healthTimer = gc.initValues.MonsterHealthTimer;
        healthTimerInc = MaxHP / healthTimer;
        alivePartyMembers = gc.TeamMates.Count;
        totalPartyMembers = gc.TeamMates.Count;
        isDead = false;
        _monsterDamageFrameCount = 0;

        //SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.GameStarted)
            return;

        if(!isDead)
        {
            // check alive party members
            CheckAlivePartyMembers();
            // scales monster's attack with it's % HP remaining
            attackPower = Mathf.Lerp(gc.initValues.MonsterAttackPowerMax, gc.initValues.MonsterAttackPowerMin, monsterHealth / MaxHP);

            // reduce health over time, based on number of alive party members
            
            _monsterDamageFrameCount += 1;
            if (_monsterDamageFrameCount == 60)
            {
                monsterHealth -= Time.deltaTime * healthTimerInc * (alivePartyMembers / totalPartyMembers) * Random.Range(30.0f, 150.0f);
                _monsterDamageFrameCount = 0;
            }
            gc.playerControlsUI.SetBossHealthSlider(monsterHealth / MaxHP);
            // find target to attack and attack
            attackFreqTime += Time.deltaTime;

            if (attackFreqTime > attackFreq) // attack
            {
                if (alivePartyMembers > 0) // attack a player if alive
                {
                    SelectTarget();

                    // deal damage to target
                    currentTarget.Health -= (int) attackPower;
                    currentTarget.ChangeState(TeamMate.ActionState.Damaged);

                    //TODO: This is an example of the ChatController adding chat stuff
                    //ChatController.Instance.AddChat(currentTarget.TMClass, //"OOF");
                    //    string.Format("OOF I HAVE TAKEN {0} DAMAGE!!! I NEED HEALING!!!", (int)attackPower));

                    ChatController.Instance.AddConvoByCondition(currentTarget.TMClass, currentTarget.Health);

                    Debug.Log("Dealt " + attackPower.ToString() + " damage to: " + currentTarget.ToString() + "!");
                    Debug.LogFormat("{0}'s Health {1} MaxHP {2}", currentTarget.ToString(), currentTarget.Health, currentTarget.MaxHP);
                    //Debug.Log("Monster's Health: " + health.ToString());

                    gameObject.transform.LookAt(currentTarget.transform);

                    if (FlameAttackParticles.isPlaying)
                        FlameAttackParticles.Stop();
                    FlameAttackParticles.Play();
                }
                else if(alivePartyMembers == 0)
                {
                    // you lose!
                    gc.setLose();
                }
                attackFreqTime = 0f; //reset attack timer
            }


            if(monsterHealth <= 0 && !isDead)
            {
                Dead();
                monsterHealth = 0;
            }
        }
    }

    private void SelectTarget()
    {
        // figure out which targets are alive
        viableTargets.Clear(); // resets viable targets
        for (int i = 0; i < totalPartyMembers; i++)
        {
            if (gc.TeamMates[i].IsAlive && gc.TeamMates[i].IsOnline)
            {
                // add party member to viable targets list
                viableTargets.Add(gc.TeamMates[i]);
                //viableTargets[viableTargets.Count-1] = gc.TeamMates[i].gameObject;
            }
        }

        //choose a random target among those alive
        currentTarget = viableTargets[Random.Range(0,viableTargets.Count)];
        Debug.Log("Targeted Party Member: " + currentTarget.ToString());
    }

    private void CheckAlivePartyMembers()
    {
        alivePartyMembers = 0;
        for (int i = 0; i < totalPartyMembers; i++)
        {
            if (gc.TeamMates[i].IsAlive && gc.TeamMates[i].IsOnline)
            {
                alivePartyMembers++;
            }
        }
    }

    // when the monster dies
    private void Dead()
    {
        // success!
        isDead = true;
        Debug.Log("Monster is dead!");
        gc.setWin();
    }

    public bool IsDead()
    {
        return isDead;
    }
}
