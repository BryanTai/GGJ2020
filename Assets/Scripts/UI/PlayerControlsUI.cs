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

    [Header("Endgame Elements")]
    public GameObject WinScreen;
    public GameObject LoseScreen;

    [Header("Prefab References")]

    public SkillButton skillButtonPrefab;
    public TeamMateButton teamMateButtonPrefab;

    public ChatItemWidget chatItemWidgetPrefab;

    [Header("Art References")]
    public List<TeammateFaces> TeammateFacesList;

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

        ChatController.Instance.OnChatAdded += CreateChat;

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
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
        //Debug.LogFormat("Skill {0} pressed!", index);
        gameController.SelectedSkill = gameController.Healer.GetSkillByType((SkillType)index);
        HighlightButton(skillButtons, index);
        skillSelectSE.PlayOneShot(skillSelectSE.clip);
    }

    public void OnTeamMateButtonPressed(int index)
    {
        if (gameController.SelectedSkill == null) return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AttackingState(); // set attacking state
        gameController.SelectedSkill?.CastSkill(gameController.TeamMates, index);
        HighlightButton(teamMateButtons, index);
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
