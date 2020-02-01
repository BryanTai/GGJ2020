using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : Entity
{
    private GameController gc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
    private float healthMax;
    private float health;
    private float attackFreq;
    private float attackPower;
    private float healthTimer;
    private float healthTimerInc;
    private int alivePartyMembers;
    private int totalPartyMembers;

    private List<GameObject> viableTargets = new List<GameObject>();
    private GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        // Grab Variables
        healthMax = gc.MonsterHealthMax;
        health = healthMax;
        attackFreq = gc.MonsterAttackFrequency;
        attackPower = gc.MonsterAttackPower;
        healthTimer = gc.MonsterHealthTimer;
        healthTimerInc = healthMax / healthTimer;
        alivePartyMembers = gc.TeamMates.Count;
        totalPartyMembers = gc.TeamMates.Count;

        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // check alive party members
        alivePartyMembers = gc.TeamMates.Count;

        // reduce health over time, based on number of alive party members
        health -= Time.deltaTime * healthTimerInc * (alivePartyMembers/totalPartyMembers);
        // find target to attack
        if(alivePartyMembers > 0)
        {
            SelectTarget();

            // deal damage to target
        }
        

        if(health <= 0)
        {
            Dead();
        }
    }

    private void SelectTarget()
    {
        // figure out which targets are alive
        viableTargets.Clear(); // resets viable targets
        for (int i = 0; totalPartyMembers <= 0; i++)
        {
            if (gc.TeamMates[i].isAlive)
            {
                // add party member to viable targets list
                viableTargets[viableTargets.Count] = gc.TeamMates[i].gameObject;
            }
        }

        // choose a random target among those alive
        currentTarget = viableTargets[Random.Range(0, viableTargets.Count - 1)];
    }

    // when the monster dies
    private void Dead()
    {
        // success!
    }
}
