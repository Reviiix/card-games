using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base.Scripts;
using Base.Scripts.Cards;
using Base.Scripts.pure_unity_methods;
using Base.Scripts.StateManagement;
using MatchingPairs.Scripts.StateManagement;
using Poker;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MatchingPairs.Scripts.GridSystem
{
    [ExecuteInEditMode]
    public class MatchingPairsGridManager : Singleton<MatchingPairsGridManager>
    {
        public static Action<GridItem> OnItemClick;
        private bool generatingGrid;
        private const string GridTag = "Grid";
        private readonly List<GridItem> gridItems = new ();
        private GameObject gridObject;
        private GridItem selectionOne;
        private GridItem selectionTwo;
        [SerializeField] private Transform gridArea;
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] [Range(1, 4)] public int amountOfRows = 3; //Setting range to preserve readability of cards.
        [SerializeField] [Range(2, 8)] public int amountOfItemsPerRow = 6;

        public IEnumerator Initialise(Action completeCallback)
        {
            yield return new WaitUntil(() => !generatingGrid);
            yield return new WaitUntil(() => DeckOfCards.Instance != null);
            generatingGrid = true;
            ResetGrid(()=>CreateGrid(() =>
            {
                SetGridItemValues();
                gridObject = GameObject.FindWithTag(GridTag);
                generatingGrid = false;
                completeCallback?.Invoke();
            }));
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StateManagement.States.Evaluation.OnEvaluationComplete += OnEvaluationComplete;
        }
    
        protected override void OnDisable()
        {
            base.OnDisable();
            StateManagement.States.Evaluation.OnEvaluationComplete -= OnEvaluationComplete;
        }

        private void OnEvaluationComplete(bool match)
        {
            if (match)
            {
                StartCoroutine(selectionOne.RemoveFromPlay());
                StartCoroutine(selectionTwo.RemoveFromPlay());
            }
            else
            {
                ((MatchingPairsAudioManager)AudioManager.Instance).PlayFailure();
                StartCoroutine(selectionOne.ResetCard(false));
                StartCoroutine(selectionTwo.ResetCard(false));
            }
        }
    

#if UNITY_EDITOR
        private void OnValidate()
        {
            //generatingGrid = false; //debug tool
            if (Application.isPlaying)
            {
                return;
            }
            if (generatingGrid) return;
            generatingGrid = true;
            ResetGrid(()=>CreateGrid(() =>
            {
                generatingGrid = false;
            }));
        }
#endif

        private void CreateGrid(Action completeCallback = null)
        {
            gridObject = Instantiate(gridPrefab, gridArea);
            for (var i = 0; i < amountOfRows; i++)
            {
                var row = Instantiate(rowPrefab, gridObject.transform).GetComponent<GridRow>();
                for (var j = 0; j < amountOfItemsPerRow; j++)
                {
                    if (Application.isPlaying)
                    {
                        var card = Instantiate(cardPrefab, row.transform).GetComponent<GridItem>();
                        gridItems.Add(card);
                        card.Initialise(OnGridItemClick);
                    }
                    else
                    {
                        Instantiate(cardPrefab, row.transform);
                    }
                }
                row.ShuffleChildren();
            }
            if (!MathUtilities.IsEvenNumber(gridItems.Count))
            {
                Debug.LogWarning($"Having an Odd amount of {nameof(gridItems)} means not every card has a match!");
            }
            completeCallback?.Invoke();
        }

        private void ResetGrid(Action completeCallback = null)
        {
            ResetGridItems();
            gridObject = GameObject.FindWithTag(GridTag);
            if (!gridObject)
            {
                completeCallback?.Invoke();
                return;
            }
            if (Application.isPlaying)
            {
                Destroy(gridObject);
                completeCallback?.Invoke();
            }
            else
            {
                UnityEditor.EditorApplication.delayCall+=()=>
                {
                    DestroyImmediate(gridObject);
                    completeCallback?.Invoke();
                };
            }
        }
    
        private void ResetGridItems()
        {
            foreach (var item in gridItems)
            {
                StartCoroutine(item.ResetCard());
            }
            gridItems.Clear();
        }

        [ContextMenu(nameof(SetGridItemValues))]
        public void SetGridItemValues()
        {
            var cardValues = CreateCardList(gridItems.Count);
            var index = 0;
            foreach (var card in gridItems)
            {
                card.SetValue(cardValues[index]);
                index++;
            }
        }

        /// <summary>
        /// Create two matching arrays of cards and combine them together.
        /// This ensures there are always the correct amount of matches
        /// </summary>
        private static List<Card> CreateCardList(int amount)
        {
            var firstHalf = new List<Card>();
            var half = amount / 2;
            for (var i = 0; i < half; i++)
            {
                firstHalf.Add(GetUniqueValue());
            }
            var secondHalf = new List<Card>(firstHalf);
            var fullList = firstHalf.Concat(secondHalf).ToList();
            return Shuffle(fullList);
        }
    
        private static List<T> Shuffle<T>(List<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                var randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
            return list;
        }
    
    
        private static Card GetUniqueValue()
        {
            return DeckOfCards.Instance.TakeRandomCard();
        }

        private void OnGridItemClick(GridItem gridItem)
        {
            //Debug.Log($"{gridItem.Value.GetRank()} of {gridItem.Value.GetSuit()} revealed({gridItem.Revealed})"); //Debug tool
            SetSelections(gridItem);
            OnItemClick?.Invoke(gridItem);
            SequentialStateManager.Instance.ProgressState();
        }

        private void SetSelections(GridItem gridItem)
        {
            if (((MatchingPairsStateManager)SequentialStateManager.Instance).IsPickOne())
            {
                selectionOne = gridItem;
            }
            if (((MatchingPairsStateManager)SequentialStateManager.Instance).IsPickTwo())
            {
                selectionTwo = gridItem;
            }
        }

        public int GetTotalItems()
        {
            return gridItems.Count;
        }
    }
}

