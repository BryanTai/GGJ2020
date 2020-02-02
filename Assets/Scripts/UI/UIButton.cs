using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Parent class for buttons
public abstract class UIButton : MonoBehaviour
{
    public Image buttonImage;
    public GameObject highlight;
    public Button uiButton;

    private void Start()
    {
        ToggleHighlight(false);
    }

    public abstract void InitButton(int index);

    public void ToggleHighlight(bool toggleOn)
    {
        if(highlight != null)
        {
            highlight.SetActive(toggleOn);
        }
    }
}
