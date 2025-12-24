using System;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    private void Start()
    {
        Close();
    }
    
    public override void Close()
    {
        HideWindow();
        
        if (ActionButton != null)
            ActionButton.interactable = false;
    }

    public override void Open()
    {
        ShowWindow();
        
        if (ActionButton != null)
            ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
