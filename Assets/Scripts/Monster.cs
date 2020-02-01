using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : Entity
{
    private GameController gc;
    private float health;
    private float attackFreq;
    private float attackFreqTime;
    private float attackPower;
    private float healthTimer;
    private float healthTimerInc;
    private int alivePartyMembers;
    private int totalPartyMembers;
    private bool isDead;

    private List<GameObject> viableTargets = new List<GameObject>();
    private GameObject currentTarget;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Grab Variables
        gc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        health = MaxHP;
        attackFreq = gc.MonsterAttackFrequency;
        attackFreqTime = 0;
        attackPower = gc.MonsterAttackPower;
        healthTimer = gc.MonsterHealthTimer;
        healthTimerInc = MaxHP / healthTimer;
        alivePartyMembers = gc.TeamMates.Count;
        totalPartyMembers = gc.TeamMates.Count;
        isDead = false;

        //SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            // check alive party members
            CheckAlivePartyMembers();

            // reduce health over time, based on number of alive party members
            health -= Time.deltaTime * healthTimerInc * (alivePartyMembers / totalPartyMembers);
            // find target to attack and attack
            attackFreqTime += Time.deltaTime;

            if (attackFreqTime > attackFreq) // attack
            {
                if (alivePartyMembers > 0) // attack a player if alive
                {
                    SelectTarget();

                    // deal damage to target
                    Debug.Log("Dealt " + attackPower.ToString() + " damage to: " + currentTarget.ToString() + "!");
                    Debug.Log("Monster's Health: " + health.ToString());
                }
                attackFreqTime = 0f; //reset attack timer
            }


            if(health <= 0 && !isDead)
            {
                Dead();
                health = 0;
            }
        }
    }

    private void SelectTarget()
    {
        // figure out which targets are alive
        viableTargets.Clear(); // resets viable targets
        for (int i = 0; i < totalPartyMembers; i++)
        {
            
            if (gc.TeamMates[i].IsAlive)
            {
                // add party member to viable targets list
                viableTargets.Add(gc.TeamMates[i].gameObject);
                //viableTargets[viableTargets.Count-1] = gc.TeamMates[i].gameObject;
            }
        }

        // choose a random target among those alive
        //currentTarget = viableTargets[Random.Range(0, viableTargets.Count - 1)];
        currentTarget = viableTargets[0];
        Debug.Log("Targeted Party Member: " + currentTarget.ToString());
    }

    private void CheckAlivePartyMembers()
    {
        alivePartyMembers = 0;
        for (int i = 0; i < totalPartyMembers; i++)
        {
            if (gc.TeamMates[i].IsAlive)
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
    }

    public bool IsDead()
    {
        if (isDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
