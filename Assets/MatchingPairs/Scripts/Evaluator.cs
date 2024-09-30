using Base.Scripts;
using Base.Scripts.Cards;
using Base.Scripts.pure_unity_methods;
using Base.Scripts.StateManagement;
using MatchingPairs.Scripts.GridSystem;
using MatchingPairs.Scripts.StateManagement;
using Poker;

namespace MatchingPairs.Scripts
{
    public class Evaluator : Singleton<Evaluator>
    {
        private int matches;
        private int totalMatches;
        private Card selectionOne;
        private Card selectionTwo;

        public void Initialise()
        {
            matches = 0;
            totalMatches = MatchingPairsGridManager.Instance.GetTotalItems() / 2;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            MatchingPairsGridManager.OnItemClick += OnStateEnter;
        }
    
        protected override void OnDisable()
        {
            base.OnDisable();
            MatchingPairsGridManager.OnItemClick -= OnStateEnter;
        }

        public bool IsMatch()
        {
            var isMatch = selectionOne.DeckIndex == selectionTwo.DeckIndex;
            if (isMatch)
            {
                matches++;
            }
            return isMatch;
        }

        public bool IsWon()
        {
            return matches == totalMatches;
        }

        private void OnStateEnter(GridItem item)
        {
            if (((MatchingPairsStateManager)SequentialStateManager.Instance).IsPickOne())
            {
                selectionOne = item.Value;
            }
            if (((MatchingPairsStateManager)SequentialStateManager.Instance).IsPickTwo())
            {
                selectionTwo = item.Value;
            }
        }
    }
}
