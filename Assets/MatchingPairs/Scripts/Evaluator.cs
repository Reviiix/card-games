using Base.Scripts.Cards;
using pure_unity_methods;
using Base.Scripts.StateManagement;
using MatchingPairs.Scripts.GridSystem;
using MatchingPairs.Scripts.StateManagement;

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
            totalMatches = GridManager.Instance.GetTotalItems() / 2;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GridManager.OnItemClick += OnStateEnter;
        }
    
        protected override void OnDisable()
        {
            base.OnDisable();
            GridManager.OnItemClick -= OnStateEnter;
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
