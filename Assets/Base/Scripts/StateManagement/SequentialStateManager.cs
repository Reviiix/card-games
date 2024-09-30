using System.Collections.Generic;
using Base.Scripts.pure_unity_methods;
using Poker.Scripts.StateManager;
using UnityEngine;

namespace Base.Scripts.StateManagement
{
    public class SequentialStateManager : Singleton<SequentialStateManager>
    {
        [SerializeField] private State[] states;
        private readonly Queue<State> stateQueue = new ();
        protected State ActiveState;

        public void Initialise()
        {
            ConvertArrayToQueue();
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
            ActiveState = stateQueue.Dequeue();
            stateQueue.Enqueue(ActiveState);
            ActiveState.OnStateEnter(()=>
            {
                if (ActiveState.progressImmediately)
                {
                    ProgressState();
                }
            });
        }
    
        public bool IsDealState()
        {
            return ActiveState is Deal;
        }

        public bool IsDrawState()
        {
            return ActiveState is Draw;
        }
    
        public bool IsHoldState()
        {
            return ActiveState is Hold;
        }
    }
}
