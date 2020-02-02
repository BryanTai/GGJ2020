using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    public Player Healer = null;
    public List<TeamMate> TeamMates = new List<TeamMate>();
    private bool gameLose;
    private bool gameWin;
    public bool GameStarted { get; set; }
    public AudioSource backgroundMusic;
    public Canvas gameCanvas;
    [HideInInspector] public PlayerControlsUI playerControlsUI;

    [Header("Prefabs")]
    public PlayerControlsUI playerUIprefab;

    [Header("Starting Values Info")]
    public StartingValues initValues;

    [HideInInspector] public Skill SelectedSkill = null;
    [HideInInspector] private ConversationLoader convLoader;

    private void Awake()
    {
        GameStarted = false;
    }

    void Start()
    {
        //TODO: Initialize all the prefabs here!
        playerControlsUI = Instantiate(playerUIprefab);
        playerControlsUI.gameController = this;
        playerControlsUI.transform.SetParent(gameCanvas.transform, false);

        convLoader = ChatController.Instance.ConvLoader;
        ChatController.Instance.AddConversation(convLoader.StartingConvo);
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
