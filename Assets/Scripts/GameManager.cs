using Poker;
using Statistics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Dealer dealer;
    public DeckOfCards deck;

    private void Awake()
    {
        Instance = this;
        
        //Cleanup after a large start up sequence.
        //Debugging.ClearUnusedAssetsAndCollectGarbage();
    }

    [ContextMenu("Increment Score")]
    public void IncrementScore()
    {
        ScoreTracker.IncrementScore(0);
    }
    
    [ContextMenu("Start Game")]
    public void StartGameTimer()
    {
        TimeTracker.StartTimer(ProjectManager.Instance);
    }

    [ContextMenu("End Game")]
    public void EndGame()
    {
        TimeTracker.StopTimer();
        HighScores.SetHighScore(ScoreTracker.Score);
        ProjectManager.Instance.userInterface.EnableGameOverMenu();
    }
}
