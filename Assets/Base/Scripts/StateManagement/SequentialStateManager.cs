using System.Collections.Generic;
using pure_unity_methods;
using UnityEngine;

namespace Base.Scripts.StateManagement
{
    public abstract class SequentialStateManager : Singleton<SequentialStateManager>
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
    }
}
