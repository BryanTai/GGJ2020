using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Parent class for buttons
public abstract class UIButton : MonoBehaviour
{
    public Image buttonImage;
    public GameObject highlight;
    public List<Sprite> buttonSprites;
    public Button uiButton;

    private void Start()
    {
        ToggleHighlight(false);
    }

    public virtual void InitButton(int index)
    {
        buttonImage.sprite = buttonSprites[index];
    }

    public void ToggleHighlight(bool toggleOn)
    {
        highlight.SetActive(toggleOn);
    }
}
