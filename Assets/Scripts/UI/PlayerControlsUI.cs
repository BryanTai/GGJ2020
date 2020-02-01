using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControlsUI : MonoBehaviour
{
    public const int TOTAL_SKILLS = 3;
    public const int TOTAL_TEAMMATES = 4;

    [HideInInspector] public List<SkillButton> skillButtons = new List<SkillButton>();
    [HideInInspector] public List<TeamMateButton> teamMateButtons = new List<TeamMateButton>();

    public Transform skillButtonParent;
    public Transform teamMateButtonParent;

    public SkillButton skillButtonPrefab;
    public TeamMateButton teamMateButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int s = 0; s < TOTAL_SKILLS; s++)
        {
            SkillButton skillButton = Instantiate(skillButtonPrefab);
            skillButton.transform.SetParent(skillButtonParent, false);
            skillButton.InitButton(s);

            int index = s;
            skillButton.uiButton.onClick.AddListener(delegate { OnSkillButtonPressed(index); });
        }

        for (int t = 0; t < TOTAL_TEAMMATES; t++)
        {
            TeamMateButton tmButton = Instantiate(teamMateButtonPrefab);
            tmButton.transform.SetParent(teamMateButtonParent, false);
            tmButton.InitButton(t);

            int index = t;
            tmButton.uiButton.onClick.AddListener(delegate { OnTeamMateButtonPressed(index); });
        }
    }

    public void OnSkillButtonPressed(int index)
    {
        Debug.LogFormat("Skill {0} pressed!", index);
    }

    public void OnTeamMateButtonPressed(int index)
    {
        Debug.LogFormat("Teammate {0} pressed!", index);
    }

}
