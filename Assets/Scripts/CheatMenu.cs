using UnityEngine;
using UnityEngine.UI;

namespace Poker
{
    public class CheatMenu : MonoBehaviour
    {
        private static bool CheatsEnabled
        {
            get
            {
                #if !UNITY_EDITOR
                return false;
                #endif
                return true;
            }
        }

        [SerializeField] private GameObject cheatCardPrefab;
        [SerializeField] private GridLayoutGroup deckLayoutGroup;
        [SerializeField] private HorizontalLayoutGroup handLayoutGroup;
        [SerializeField] private Canvas cheatScreen;
        [SerializeField] private Button cheatButton;
        private CardSlot[] _cheatCards;
        public static bool CheatCardsAvailable => _cheatCardInHandIndex > 0;
        #region CheatCardInHandIndex
        private static int _cheatCardInHandIndex;
        private int CheatCardInHandIndex
        {
            get
            {
                var temporary = _cheatCardInHandIndex;
                _cheatCardInHandIndex++;
                if (_cheatCardInHandIndex == _cheatCards.Length)
                {
                    EnableCheatScreen(false);
                }
                return temporary;
            }
            set => _cheatCardInHandIndex = value;
        }
        #endregion CheatCardInHandIndex

        private void Start()
        {
            Initialise();
        }

        private void Initialise()
        {
            SetCheatState();
            AddButtonEvents();
        }
        
        private void SetCheatState()
        {
            if (!CheatsEnabled)
            {
                RemoveCheats();
                return;
            }
            CreateDynamicCheatScreen();
        }

        private void AddButtonEvents()
        {
            cheatButton.onClick.AddListener(()=>EnableCheatScreen());
        }

        private void HandCardSelected(int handIndex)
        {
            
        }

        private void DeckCardSelected(int deckIndex)
        {
            _cheatCards[CheatCardInHandIndex].SetNewCard(Dealer.Instance.TakeSpecificCardFromDeck(deckIndex));
        }

        #region Create dynamic cheat menu based of deck and hand classes
        [ContextMenu("Create Cheat Menu")]
        private void CreateDynamicCheatScreen()
        {
            CreateDeck();
            CreateHand();
        }
        
        private void CreateDeck()
        {
            var amountOfCardsInDeck = Dealer.Instance.GetAmountOfCardsInDeck();
            for (var i = 0; i < amountOfCardsInDeck; i++)
            {
                CreateDeckCard(i);
            }
        }
        
        private void CreateDeckCard(int deckIndex)
        {
            var cheatCard = Instantiate(cheatCardPrefab, deckLayoutGroup.transform);
            
            cheatCard.GetComponent<Image>().sprite = Dealer.Instance.TakeSpecificCardFromDeck(deckIndex).GetCardSprite();
            cheatCard.GetComponent<Button>().onClick.AddListener(()=>DeckCardSelected(deckIndex));
        }
        
        private void CreateHand()
        {
            _cheatCards = new CardSlot[Dealer.Instance.GetAmountOfCardsInHand(0)];
            for (var i = 0; i < _cheatCards.Length; i++)
            {
                CreateHandCard(i);
            }
        }

        private void CreateHandCard(int handIndex)
        {
            var cheatCard = Instantiate(cheatCardPrefab, handLayoutGroup.transform);
            var cheatCardImage = cheatCard.GetComponent<Image>();

            //_cheatCards[handIndex].cardImage = cheatCardImage;
            cheatCardImage.sprite = Dealer.Instance.GetCardBack();
            
            cheatCard.GetComponent<Button>().onClick.AddListener(() => HandCardSelected(handIndex));

        }
        #endregion Create cheat menu based of deck and hand classes
        
        private void EnableCheatScreen(bool state = true)
        {
            cheatScreen.enabled = state;
        }

        private void RemoveCheats()
        {
            Destroy(cheatScreen.gameObject);
            Destroy(cheatButton.gameObject);
            
            Destroy(gameObject);
        }
    }
}




