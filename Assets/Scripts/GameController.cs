using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    public Player Healer = null;
    public List<TeamMate> TeamMates = new List<TeamMate>();
    public float MonsterHealthTimer;
    public float MonsterAttackFrequency;
    public float MonsterAttackPowerMin;
    public float MonsterAttackPowerMax;
    private bool gameLose;
    private bool gameWin;
    public AudioSource backgroundMusic;
    public Canvas gameCanvas;
    [HideInInspector] public PlayerControlsUI playerControlsUI;

    [Header("Prefabs")]
    public PlayerControlsUI playerUIprefab;

    [HideInInspector] public Skill SelectedSkill = null;
    [HideInInspector] private ConversationLoader convLoader;

    private void Awake()
    {
        
    }

    void Start()
    {
        //TODO: Initialize all the prefabs here!
        playerControlsUI = Instantiate(playerUIprefab);
        playerControlsUI.gameController = this;
        playerControlsUI.transform.SetParent(gameCanvas.transform, false);

        convLoader = ChatController.Instance.ConvLoader;
    }

    public void setLose()
    {
        gameLose = true;
        playerControlsUI.LoseScreen.SetActive(true);
    }

    public bool isLost()
    {
        return gameLose;
    }

    public void setWin()
    {
        gameWin = true;
        playerControlsUI.WinScreen.SetActive(true);
    }

    public bool isWon()
    {
        return gameWin;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
