using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public int MaxHP { get; protected set; }
    public event Action<int> OnHealthChanged;
    public event Action OnDead;

    private GameController _gc;
    protected GameController gc
    {
        get
        {
            if(_gc == null)
            {
                _gc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
            }
            return _gc;
        }
    }

    private int _health;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            int oldHealth = _health;
            _health = Mathf.Min(MaxHP, value);
            _health = Mathf.Max(_health, 0);

            if (oldHealth != _health)
                OnHealthChanged?.Invoke(_health);

            if (_health == 0)
                OnDead?.Invoke();
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _gc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
    }
}
