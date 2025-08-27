
using System;
using System.Collections.Generic;
using DG.Tweening;
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.UI;

public class GameCompletePopup : UIScreen
{
    private Vector2 originalPos;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = ((RectTransform)transform);
        originalPos = rectTransform.anchoredPosition;
    }

    [SerializeField] private Button homeBtn;
    private List<IGameCompletePopupEventListner> listners;
    
    public void Init()
    {
        homeBtn.onClick.AddListener(OnHomeButtonClicked);
        listners = new List<IGameCompletePopupEventListner>();
    }

    public void AddListener(IGameCompletePopupEventListner listener)
    {
        listners.Add(listener);
    }

    private void OnHomeButtonClicked()
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        UIManager.Instance.PopScreen();
        UIManager.Instance.PushScreen(UiScreenFactory.Instance.GetUIScreen(Constants.HOME_SCREEN));
        foreach (IGameCompletePopupEventListner listner in listners)
        {
            listner.OnClickHomeButton();
        }
    }

    public override void Show()
    {
        base.Show();
        rectTransform.anchoredPosition = new Vector2(originalPos.x, Screen.height);

        // Animate into center
        rectTransform.DOAnchorPos(originalPos, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => Debug.Log("✅ Popup Arrived at Center"));
        
    }

    private void OnDestroy()
    {
        homeBtn.onClick.RemoveAllListeners();
        listners.Clear();
    }
}

public interface IGameCompletePopupEventListner
{
    void OnClickHomeButton();
}
