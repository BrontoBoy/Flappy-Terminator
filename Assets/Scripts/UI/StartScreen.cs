using System;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;

    public override void Close()
    {
        HideWindow(); 
    }

    public override void Open()
    {
        ShowWindow(); 
    }

    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}