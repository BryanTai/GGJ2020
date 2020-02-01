using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int TeamSize;
    public Player Healer;
    public List<TeamMate> TeamMates = new List<TeamMate>();

    private void Awake()
    {
        
    }

    void Start()
    {
        //TODO: Initialize all the prefabs here!
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
