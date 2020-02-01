using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    private GameController gc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
    private float healthMax;
    private float health;
    private float attackFreq;
    private float attackPower;
    // Start is called before the first frame update
    void Start()
    {
        healthMax = gc.MonsterHealthMax;
        health = healthMax;
        attackFreq = gc.MonsterAttackFrequency;
        attackPower = gc.MonsterAttackPower;
}

    // Update is called once per frame
    void Update()
    {
        // reduce health over time 

        // find target to attack
    }
}
