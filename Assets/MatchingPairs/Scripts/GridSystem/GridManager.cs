using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base.Scripts.Cards;
using pure_unity_methods;
using Base.Scripts.StateManagement;
using MatchingPairs.Scripts.StateManagement;
using UnityEngine;

namespace MatchingPairs.Scripts.GridSystem
{
    [ExecuteInEditMode]
    public class GridManager : Singleton<GridManager>
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
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying) return;
            if (generatingGrid) return;
            GenerateGrid();
        }
        #endif
        
        public IEnumerator Initialise(Action completeCallback)
        {
            yield return new WaitUntil(() => !generatingGrid);
            GenerateGrid(()=>
            {
                SetGridItemValues();
                completeCallback();
            });
        }

        #region Generate Grid
                private void GenerateGrid(Action completeCallback = null)
        {
            generatingGrid = true;
            ResetGrid(()=>CreateGrid(() =>
            {
                gridObject = GameObject.FindWithTag(GridTag);
                generatingGrid = false;
                completeCallback?.Invoke();
            }));
        }

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
        #endregion Generate Grid

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
        
        private void SetGridItemValues()
        {
            var amountOfItems = gridItems.Count;
            var cardValues = Dealer.Instance.TakeCardsFromDeck(amountOfItems);
            for (var i = 0; i < amountOfItems; i++)
            {
                gridItems[i].SetValue(cardValues[i]);
            }
        }

        public int GetTotalItems()
        {
            return gridItems.Count;
        }
    }
}

