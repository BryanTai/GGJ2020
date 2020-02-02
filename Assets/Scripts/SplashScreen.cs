using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public GameObject blackBackground;
    public GameObject hideo;
    public GameObject team;
    public GameObject menu;
    private float fadeAlpha;
    public float fadeLerp;
    private bool fadeDirection;
    
    private bool shown_hideo;
    private bool shown_team;
    private bool menu_loaded;
    // Start is called before the first frame update
    void Start()
    {
        shown_hideo = false;
        shown_team = false;
        menu_loaded = false;
        fadeAlpha = 0;
        fadeDirection = true;
    }

    // Update is called once per frame
    void Update()
    {
        // change direction of fade, either up or down
        if (fadeDirection && !menu_loaded)
        {
            fadeAlpha = Mathf.Lerp(fadeAlpha, 1, fadeLerp* Time.deltaTime);
            if (1-fadeAlpha < 0.0001)
            {
                fadeAlpha = 1;
                fadeDirection = false;
                
            }
        }
        else if (!fadeDirection && !menu_loaded)
        {
            fadeAlpha = Mathf.Lerp(fadeAlpha, 0, fadeLerp*Time.deltaTime);
            if (fadeAlpha < 0.0001)
            {
                fadeAlpha = 0;
                fadeDirection = true;
            }
        }

        // toggle states of intro
        if (!shown_team)
        {
            blackBackground.SetActive(true);
            team.SetActive(true);
            team.GetComponent<CanvasGroup>().alpha = fadeAlpha;

            if (fadeAlpha == 0)
            {
                team.SetActive(false);
                shown_team = true;
            }
        }
        else if (!shown_hideo)
        {
            hideo.SetActive(true);
            hideo.GetComponent<CanvasGroup>().alpha = fadeAlpha;

            if (fadeAlpha == 0)
            {
                hideo.SetActive(false);
                shown_hideo = true;
            }
        }
        else if (!menu_loaded)
        {
            menu.GetComponent<CanvasGroup>().alpha = fadeAlpha;
            menu.SetActive(true);

            if (fadeAlpha == 1)
            {
                menu_loaded = true;
            }
        }
    }
}
