using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillButton : UIButton
{
    public Text cooldownTimer;
    public AudioSource skillSe;

    [HideInInspector] public Skill skill;
    [HideInInspector] public int maxCooldownTime;

    private bool isOnCooldown;
    private float time;

    public override void InitButton(int index)
    {
        SpriteState skillSprites = new SpriteState
        {
            pressedSprite = skill.SkillData.SkillButtonPressed,
            disabledSprite = skill.SkillData.SkillButtonPressed,
            selectedSprite = skill.SkillData.SkillButtonReleased,
        };
        uiButton.image.sprite = skill.SkillData.SkillButtonReleased;
        uiButton.spriteState = skillSprites;
        skillSe.clip = skill.SkillData.skillSE;
        maxCooldownTime = skill.SkillData.CoolDown;
        skill.OnCoolDownStarted += StartSkillCooldown;
        ToggleCooldownUI(false);
    }

    public void StartSkillCooldown()
    {
        if (isOnCooldown) return;
        skillSe.PlayOneShot(skillSe.clip);
        isOnCooldown = true;
        time = maxCooldownTime;

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
       // Debug.Log("START COOLDOWN");
        ToggleCooldownUI(true);
        while(time > 0)
        {
            cooldownTimer.text = ((int)time + 1).ToString();
            time -= Time.deltaTime;
            //Debug.Log("Time " + time.ToString());
            yield return null;
        }
        ToggleCooldownUI(false);
        isOnCooldown = false;
        //Debug.Log("COOLDOWN COMPLETE");
    }

    public void ToggleCooldownUI(bool isStarting)
    {
        uiButton.interactable = !isStarting;
        cooldownTimer.gameObject.SetActive(isStarting);
    }
}
