using System;
using System.Collections.Generic;
using MatchingPairs.pure_unity_methods;
using MatchingPairs.StateManagement.States;
using MatchingPairs.StateManagement.States.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MatchingPairs.StateManagement
{
    public class MatchingPairsStateManager : Singleton<MatchingPairsStateManager>
    {
        [SerializeField] private State[] states;
        [SerializeField] private State menuState; //menuState can appear out of order
        private readonly Queue<State> stateQueue = new ();
        private State activeState;
        private State stateCache;
        public static Action<State> OnStateChange;

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
            activeState = stateQueue.Dequeue();
            stateQueue.Enqueue(activeState);
            activeState.OnStateEnter(()=>
            {
                if (activeState.progressImmediately)
                {
                    ProgressState();
                }
                OnStateChange?.Invoke(activeState);
            });
        }

        public void SetMenuState(bool state, Action callBack)
        {
            if (state)
            {
                stateCache = activeState;
                activeState = menuState;
                activeState.OnStateEnter(callBack);
            }
            else
            {
                activeState = stateCache;
                activeState.OnStateEnter(callBack);
            }
        }
    
        public void GameWon()
        {
            MMatchingPairsMenu.Instance.OpenMenu();
            MatchingPairsAudio.PlayGameWon();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
        public bool IsMenuState()
        {
            return activeState is States.Menu;
        }

        public bool IsPickOne()
        {
            return activeState is PickOne;
        }
    
        public bool IsPickTwo()
        {
            return activeState is PickTwo;
        }
    
        public bool IsEvaluation()
        {
            return activeState is Evaluation;
        }
    }
}
