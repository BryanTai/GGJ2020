using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Parent class for buttons
public class UIButton : MonoBehaviour
{
    public Image buttonImage;
    public GameObject highlight;
    public List<Sprite> buttonSprites;

    private void Start()
    {
        ToggleHighlight(false);
    }

    public void InitButton(int index)
    {
        buttonImage.sprite = buttonSprites[index];
    }

    public void ToggleHighlight(bool toggleOn)
    {
        highlight.SetActive(toggleOn);
    }
}
