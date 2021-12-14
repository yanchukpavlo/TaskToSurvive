using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public event Action<GameState> onChangeStateTrigger;
    public void ChangeStateTrigger(GameState state)
    {
        onChangeStateTrigger?.Invoke(state);
    }

    public event Action onEnemyDie;
    public void EnemyDieTrigger()
    {
        onEnemyDie?.Invoke();
    }
}
