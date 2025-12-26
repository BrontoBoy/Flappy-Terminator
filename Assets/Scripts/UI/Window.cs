using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    protected const float VisibleAlpha = 1f;
    protected const float HiddenAlpha = 0f;
    
    [SerializeField] protected Button ActionButton;
    [SerializeField] private CanvasGroup _windowGroup;

    protected CanvasGroup WindowGroup => _windowGroup;

    private void OnEnable()
    {
        if (ActionButton != null)
            ActionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        if (ActionButton != null)
            ActionButton.onClick.RemoveListener(OnButtonClick);
    }
    
    public abstract void Open();
    
    public abstract void Close();
    
    protected abstract void OnButtonClick();

    protected void ShowWindow()
    {
        if (WindowGroup != null)
        {
            WindowGroup.alpha = VisibleAlpha;
            WindowGroup.interactable = true;
            WindowGroup.blocksRaycasts = true;
        }
    }
    
    protected void HideWindow()
    {
        if (WindowGroup != null)
        {
            WindowGroup.alpha = HiddenAlpha;
            WindowGroup.interactable = false;
            WindowGroup.blocksRaycasts = false;
        }
    }
}