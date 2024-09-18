using System.Linq;
using PureFunctions.UnitySpecific;
using UnityEngine;

public class ButtonManager : Singleton<ButtonManager>
{
    public PlayGameButton Play { get; private set; }
    [SerializeField] private GameButton[] buttons;

    public void Awake()
    {
        AssignReferencesFromList();
    }

    private void AssignReferencesFromList()
    {
        foreach (var button in buttons)
        {
            if (button is PlayGameButton gameButton)
            {
                Play = gameButton;
            }
        }
    }

    public void EnableAllButtons(bool state = true)
    {
        foreach (var button in buttons)
        {
            button.Enable(state);
        }
    }
}
