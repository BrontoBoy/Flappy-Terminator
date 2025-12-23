using System;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;

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
        PlayButtonClicked?.Invoke();
    }
}
