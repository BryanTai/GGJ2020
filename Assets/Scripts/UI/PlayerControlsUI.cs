using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControlsUI : MonoBehaviour
{
    public AudioSource skillSelectSE;

    [HideInInspector] public GameController gameController;

    public Slider bossHealthSlider;
    public TeamMateButton PlayerHealerButton;

    [Header("Skill and Teammate Buttons")]
    public const int TOTAL_SKILLS = 3;
    public const int TOTAL_TEAMMATES = 4;

    [HideInInspector] public List<UIButton> skillButtons = new List<UIButton>();
    [HideInInspector] public List<UIButton> teamMateButtons = new List<UIButton>();

    public Transform skillButtonParent;
    public Transform teamMateButtonParent;

    [Header("Chat Elements")]
    private const int MAX_CHAT_ITEMS = 15;
    public Transform chatItemParent;
    private int totalChatItems = 0;
    private Queue<ChatItemWidget> chatHistory = new Queue<ChatItemWidget>();
    private Queue<ChatItem> chatQueue = new Queue<ChatItem>();
    private float chatCooldown;
    private float timeSinceLastCooldown = 0;
    private const float MinChatCooldown = 1.0f;
    private const float MaxChatCooldown = 2.5f;

    [Header("Endgame Elements")]
    public GameObject WinScreen;
    public GameObject LoseScreen;

    [Header("Prefab References")]

    public SkillButton skillButtonPrefab;
    public TeamMateButton teamMateButtonPrefab;

    public ChatItemWidget chatItemWidgetPrefab;

    [Header("Art References")]
    public List<TeammateFaces> TeammateFacesList;

    private void Awake()
    {
        //ChatController.Instance.OnChatAdded += CreateChat;
        ChatController.Instance.OnConversationAdded += AddConvoToQueue;
        chatCooldown = Random.Range(MinChatCooldown, MaxChatCooldown);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetBossHealthSlider(1f);

        for(int s = 0; s < TOTAL_SKILLS; s++)
        {
            SkillButton skillButton = Instantiate(skillButtonPrefab);
            skillButton.transform.SetParent(skillButtonParent, false);
            skillButton.skill = gameController.Healer.GetSkillByType((SkillType)s);
            skillButton.InitButton(s);
            skillButtons.Add(skillButton);

            int index = s; //Need to manually seperate the index
            skillButton.uiButton.onClick.AddListener(delegate { OnSkillButtonPressed(index); });
        }

        for (int t = 0, count = gameController.TeamMates.Count; t < count; t++)
        {
            TeamMateButton tmButton = Instantiate(teamMateButtonPrefab);
            tmButton.transform.SetParent(teamMateButtonParent, false);
            tmButton.teamMate = gameController.TeamMates[t];
            tmButton.faceReferences = TeammateFacesList[t];
            tmButton.InitButton(t);
            teamMateButtons.Add(tmButton);

            int index = t;  //Need to manually seperate the index
            tmButton.uiButton.onClick.AddListener(delegate { OnTeamMateButtonPressed(index); });
        }

        //Set the player's button image. It won't have any other functionality
        PlayerHealerButton.faceReferences = TeammateFacesList[TeammateFacesList.Count - 1];
        PlayerHealerButton.SetButtonImageFromMood(TeamMateMood.NEUTRAL);

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    private void Update()
    {
        DequeueChatQueue();
    }

    private void DequeueChatQueue()
    {
        timeSinceLastCooldown += Time.deltaTime;

        if (chatQueue.Count == 0)
        {
            if (!gameController.GameStarted)
            {
                gameController.GameStarted = true;
            }
                
            return;
        }

        if (timeSinceLastCooldown < chatCooldown)
            return;

        ChatItem nextItem = chatQueue.Dequeue();
        CreateChat(nextItem);

        timeSinceLastCooldown = 0;
        chatCooldown = Random.Range(MinChatCooldown, MaxChatCooldown);
    }

    public void AddConvoToQueue(Conversation newConvo)
    {
        foreach(ChatItem chatItem in newConvo.ChatItems)
        {
            if (chatItem == null) continue;

            chatItem.SetChatColor();
            chatQueue.Enqueue(chatItem);
        }
    }

    public void CreateChat(ChatItem chatItem)
    {
        ChatItemWidget chatWidget = Instantiate(chatItemWidgetPrefab);
        chatWidget.Init(chatItem);
        chatWidget.transform.SetParent(chatItemParent);
        chatHistory.Enqueue(chatWidget);

        totalChatItems++;
        if(totalChatItems >= MAX_CHAT_ITEMS)
        {
            totalChatItems--;
            ChatItemWidget toDestroy = chatHistory.Dequeue();
            Destroy(toDestroy.gameObject);
        }
    }

    public void SetBossHealthSlider(float healthPercentage)
    {
        bossHealthSlider.value = healthPercentage;
    }

    public void OnSkillButtonPressed(int index)
    {
        if (!gameController.GameStarted)
            return;

        if(!gameController.isWon() && !gameController.isLost())
        {
            //Debug.LogFormat("Skill {0} pressed!", index);
            gameController.SelectedSkill = gameController.Healer.GetSkillByType((SkillType)index);
            HighlightButton(skillButtons, index);
            skillSelectSE.PlayOneShot(skillSelectSE.clip);
        }
    }

    public void OnTeamMateButtonPressed(int index)
    {
        if (!gameController.GameStarted)
            return;

        if (gameController.SelectedSkill == null)
            return;

        if (!gameController.isWon() && !gameController.isLost())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AttackingState(); // set attacking state
            gameController.SelectedSkill?.CastSkill(gameController.TeamMates, index);
            HighlightButton(teamMateButtons, index);
        }
    }

    public void ChangeTeamMateButtonFace(TeamMate teamMate, TeamMateMood mood)
    {
        int index = (int)teamMate.TMClass;
        TeamMateButton tmb = teamMateButtons[index] as TeamMateButton;
        tmb.SetButtonImageFromMood(mood);
    }

    private void HighlightButton(List<UIButton> allButtons, int index)
    {
        for(int i = 0; i < allButtons.Count; i++)
        {
            UIButton button = allButtons[i];
            button.ToggleHighlight(i == index);
        }
    }
}
