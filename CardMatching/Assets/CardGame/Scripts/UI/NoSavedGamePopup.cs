using DG.Tweening;
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.UI;

public class NoSavedGamePopup : UIScreen
{
    [SerializeField] private Button homeBtn;
    private Vector2 originalPos;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = ((RectTransform)transform);
        originalPos = rectTransform.anchoredPosition;
        homeBtn.onClick.AddListener(OnHomeButtonClicked);
    }

    private void OnHomeButtonClicked()
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        UIManager.Instance.PopScreen();
    }
    
    public override void Show()
    {
        base.Show();
        rectTransform.anchoredPosition = new Vector2(originalPos.x, Screen.height);

        // Animate into center
        rectTransform.DOAnchorPos(originalPos, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => Debug.Log(" Popup Arrived at Center"));
    }
    
}
