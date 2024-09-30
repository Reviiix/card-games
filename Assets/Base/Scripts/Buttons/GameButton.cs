using System;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Scripts.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class GameButton : MonoBehaviour, IGameButton
    {
        private Button button;
    
        protected virtual void Awake()
        {
            GetButton();
            AddListenerToButton();
        }

        public void GetButton()
        {
            button = GetComponent<Button>();
        }

        public void AddListenerToButton()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Enable(bool state)
        {
            button.interactable = state;
        }

        public virtual void OnClick()
        {
            throw new NotImplementedException();
        }
    }
}