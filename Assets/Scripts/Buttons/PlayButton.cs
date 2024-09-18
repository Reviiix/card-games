using TMPro;
using UnityEngine;

public class PlayGameButton :  GameButton
{
    private const string DealString = "DEAL";
    private const string DrawString = "DRAW";
    [SerializeField] private TMP_Text text; 
    
    public override void OnClick()
    {
        StateManager.Instance.ProgressState();
    }
    
    public void SetDealText()
    {
        text.text = DealString;
    }
    
    public void SetDrawText()
    {
        text.text = DrawString;
    }
}
