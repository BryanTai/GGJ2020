using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillButton : UIButton
{
    public Text cooldownTimer;
    [HideInInspector] public int maxCooldownTime;

    private bool isOnCooldown;
    private float time;

    public override void InitButton(int index)
    {
        base.InitButton(index);

        ToggleCooldownUI(false);
    }

    public void StartSkillCooldown()
    {
        if (isOnCooldown) return;

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
            cooldownTimer.text = ((int)time).ToString();
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
