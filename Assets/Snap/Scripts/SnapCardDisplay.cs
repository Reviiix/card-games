using Base.Scripts.Cards;
using UnityEngine;

namespace Snap.Scripts
{
    public class SnapCardDisplay : CardDisplay
    {
        protected override Sprite GetSprite(Card card)
        {
            return card.IsJoker() ? SnapDealer.Instance.GetCardBack() : card.GetCardSprite();
        }
    }
}
