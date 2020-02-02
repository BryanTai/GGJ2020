using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int MaxHP;
    public event Action<int> OnHealthChanged;

    private int _health;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Min(MaxHP, value);
            _health = Mathf.Max(value, 0);
            OnHealthChanged?.Invoke(_health);
        }
    }

    public int MaxHealth
    {
        get { return MaxHP; }
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Health = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
