
using Base.Scripts.StateManagement;

namespace Poker.Scripts.StateManager
{
    
    public class PokerStateManager : SequentialStateManager
    {
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
