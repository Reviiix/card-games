using MatchingPairs.Cards;
using MatchingPairs.GridSystem;
using MatchingPairs.pure_unity_methods;
using MatchingPairs.StateManagement;

namespace MatchingPairs
{
    public class MatchingPairsEvaluator : Singleton<MatchingPairsEvaluator>
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
            if (MatchingPairsStateManager.Instance.IsPickOne())
            {
                selectionOne = item.Value;
            }
            if (MatchingPairsStateManager.Instance.IsPickTwo())
            {
                selectionTwo = item.Value;
            }
        }
    }
}
