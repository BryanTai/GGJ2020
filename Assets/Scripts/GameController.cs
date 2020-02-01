using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    public Player Healer = null;
    public List<TeamMate> TeamMates = new List<TeamMate>();
    public float MonsterHealthTimer;
    public float MonsterAttackFrequency;
    public float MonsterAttackPower;

    public Canvas gameCanvas;
    [HideInInspector] public PlayerControlsUI playerControlsUI;

    [Header("Prefabs")]
    public PlayerControlsUI playerUIprefab;

    [HideInInspector] public Skill SelectedSkill = null;

    private void Awake()
    {
        
    }

    void Start()
    {
        //TODO: Initialize all the prefabs here!
        playerControlsUI = Instantiate(playerUIprefab);
        playerControlsUI.gameController = this;
        playerControlsUI.transform.SetParent(gameCanvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
