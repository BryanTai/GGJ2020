using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync("Main");
    }
    public void Replay()
    {
        SceneManager.LoadSceneAsync("SplashScreen");
    }
}
