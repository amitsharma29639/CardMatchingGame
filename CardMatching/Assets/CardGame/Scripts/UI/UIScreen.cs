using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    public bool IsVisible => gameObject.activeSelf;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnShow();
    }

    public virtual void Hide()
    {
        OnHide();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when the screen is shown
    /// </summary>
    protected virtual void OnShow() { }

    /// <summary>
    /// Called when the screen is hidden
    /// </summary>
    protected virtual void OnHide() { }

    /// <summary>
    /// Called when back button (or escape key) is pressed
    /// Returns true if the screen handled the back press
    /// </summary>
    public virtual bool OnBackPressed()
    {
        return false; // Default: not handled
    }
}