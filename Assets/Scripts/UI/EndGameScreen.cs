using System;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    public override void Close()
    {
        if (WindowGroup != null)
        {
            WindowGroup.alpha = HiddenAlpha;
            WindowGroup.interactable = false;
            WindowGroup.blocksRaycasts = false;
        }
    }

    public override void Open()
    {
        if (WindowGroup != null)
        {
            WindowGroup.alpha = VisibleAlpha;
            WindowGroup.interactable = true;
            WindowGroup.blocksRaycasts = true;
        }
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
