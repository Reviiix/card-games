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
                
                return ProjectSettings.CheatsEnabled;
            }
        }

        [SerializeField] private GameObject cheatCardPrefab;
        [SerializeField] private GridLayoutGroup deckLayoutGroup;
        [SerializeField] private HorizontalLayoutGroup handLayoutGroup;
        [SerializeField] private Canvas cheatScreen;
        [SerializeField] private Button cheatButton;
        private CardInHand[] _cheatCards;
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
            _cheatCards[CheatCardInHandIndex].SetNew(GameManager.Instance.deck.TakeSpecificCard(deckIndex));
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
            for (var i = 0; i < GameManager.Instance.deck.AmountOfCards; i++)
            {
                CreateDeckCard(i);
            }
        }
        
        private void CreateDeckCard(int deckIndex)
        {
            var cheatCard = Instantiate(cheatCardPrefab, deckLayoutGroup.transform);
            
            cheatCard.GetComponent<Image>().sprite = GameManager.Instance.deck.TakeSpecificCard(deckIndex).ReturnCardSprite();
            cheatCard.GetComponent<Button>().onClick.AddListener(()=>DeckCardSelected(deckIndex));
        }
        
        private void CreateHand()
        {
            _cheatCards = new CardInHand[GameManager.Instance.dealer.AmountOfCardsInHand(0)];
            for (var i = 0; i < _cheatCards.Length; i++)
            {
                CreateHandCard(i);
            }
        }

        private void CreateHandCard(int handIndex)
        {
            var cheatCard = Instantiate(cheatCardPrefab, handLayoutGroup.transform);
            var cheatCardImage = cheatCard.GetComponent<Image>();

            _cheatCards[handIndex].cardImage = cheatCardImage;
            cheatCardImage.sprite = GameManager.Instance.deck.cardBack;
            
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




