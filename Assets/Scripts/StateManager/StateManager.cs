using System;
using System.Collections.Generic;
using PureFunctions.UnitySpecific;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    [SerializeField] private State[] states;
    private readonly Queue<State> stateQueue = new ();
    private State activeState;

    private void Awake()
    {
        ConvertArrayToQueue();
    }

    private void Start()
    {
        ProgressState();
    }

    private void ConvertArrayToQueue()
    {
        foreach (var state in states)
        {
            stateQueue.Enqueue(state);
        }
    }
    
    public void ProgressState()
    {
        activeState = stateQueue.Dequeue();
        stateQueue.Enqueue(activeState);
        activeState.OnStateEnter(()=>
        {
            if (activeState.progressImmediately)
            {
                ProgressState();
            }
        });
    }
    
    public bool IsDealState()
    {
        return activeState is Deal;
    }

    public bool IsDrawState()
    {
        return activeState is Draw;
    }
    
    public bool IsHoldState()
    {
        return activeState is Hold;
    }
}
