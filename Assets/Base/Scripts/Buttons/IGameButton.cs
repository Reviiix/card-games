namespace Base.Scripts.Buttons
{
    public interface IGameButton
    {
        public void GetButton();
    
        public void AddListenerToButton();
    
        public void OnClick();
    
        public void Enable(bool state);
    
    }
}
