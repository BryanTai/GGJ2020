using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControlsUI : MonoBehaviour
{
    public AudioSource basicHealAudioSource;

    public const int TOTAL_SKILLS = 3;
    public const int TOTAL_TEAMMATES = 4;

    [HideInInspector] public List<UIButton> skillButtons = new List<UIButton>();
    [HideInInspector] public List<UIButton> teamMateButtons = new List<UIButton>();

    public Transform skillButtonParent;
    public Transform teamMateButtonParent;

    public SkillButton skillButtonPrefab;
    public TeamMateButton teamMateButtonPrefab;

    [HideInInspector] public GameController gameController;

    [Header("Art References")]
    public List<TeammateFaces> TeammateFacesList;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int s = 0; s < TOTAL_SKILLS; s++)
        {
            SkillButton skillButton = Instantiate(skillButtonPrefab);
            skillButton.transform.SetParent(skillButtonParent, false);
            skillButton.InitButton(s);
            skillButtons.Add(skillButton);

            int index = s; //Need to manually seperate the index
            skillButton.uiButton.onClick.AddListener(delegate { OnSkillButtonPressed(index); });
        }

        for (int t = 0; t < TOTAL_TEAMMATES; t++)
        {
            TeamMateButton tmButton = Instantiate(teamMateButtonPrefab);
            tmButton.transform.SetParent(teamMateButtonParent, false);
            tmButton.faceReferences = TeammateFacesList[t];
            tmButton.InitButton(t);
            teamMateButtons.Add(tmButton);

            int index = t;  //Need to manually seperate the index
            tmButton.uiButton.onClick.AddListener(delegate { OnTeamMateButtonPressed(index); });
        }

    }

    public void OnSkillButtonPressed(int index)
    {
        Debug.LogFormat("Skill {0} pressed!", index);
        gameController.SelectedSkill = gameController.Healer.GetSkillByType((SkillType)index);
        HighlightButton(skillButtons, index);
    }

    public void OnTeamMateButtonPressed(int index)
    {
        Debug.LogFormat("Teammate {0} pressed!", index);
        basicHealAudioSource.PlayOneShot(basicHealAudioSource.clip);
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
