using System;
using System.Collections;
using Base.Scripts.Cards;
using pure_unity_methods;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingPairs.Scripts.GridSystem
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class GridItem : MonoBehaviour
    {
        private bool Initialised { get; set; }
        private bool Active { get; set; }
        private bool Revealed { get; set; }
        private Image display;
        private Animator animator;
        private Action<GridItem> onClick;
        public Card Value { get; private set; }
        private static readonly WaitForSeconds TimeBeforeReset = new (1);

        public void Initialise(Action<GridItem> gridManagerOnClick)
        {
            if (Initialised)
            {
                Debug.LogError($"Do not initialise {nameof(GridItem)} more than once.");
                return;
            }
            AssignFields();
            SubscribeToEvents(gridManagerOnClick);
            Initialised = true;
            Active = true;
        }

        private void AssignFields()
        {
            //Secured by the require component attribute.
            GetComponent<Button>().onClick.AddListener(OnClick);
            display = GetComponent<Image>();
            animator = GetComponent<Animator>();
        }
    
        private void SubscribeToEvents(Action<GridItem> gridManagerOnClick)
        {
            onClick += gridManagerOnClick;
        }
    
        private void OnDisable()
        {
            if (onClick != null)
            {
                onClick -= MatchingPairsGridManager.OnItemClick;
            }
        }
    
        private void OnClick()
        {
            if (Revealed) return;
            if (!Active) return;
            Reveal();
            onClick(this);
        }

        public void SetValue(Card value)
        {
            if (!Initialised)
            {
                Debug.LogError($"{nameof(GridItem)} has not been initialised.");
                return;
            }
            Value = value;
            //SetCardSprite(Value.GetCardSprite()); // Debug tool
        }

        private void Reveal()
        {
            Revealed = !Revealed;
            SetCardSprite(Value.GetCardSprite());
        }

        private void SetCardSprite(Sprite s)
        {
            display.sprite = s;
        }

        public IEnumerator RemoveFromPlay(bool instant = true)
        {
            Active = false;
            if (!instant)
            {
                yield return TimeBeforeReset;
            }
            animator.enabled = true;
            ((MatchingPairsAudioManager)AudioManager.Instance).PlaySuccess();
        }
    
        public IEnumerator ResetCard(bool instant = true)
        {
            if (!instant)
            {
                yield return TimeBeforeReset;
            }
            Revealed = false;
            Active = true;
            display.sprite = DeckOfCards.Instance.cardBack;
        }
    }
}
