
using System.Collections.Generic;
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.UI;

public class GameCompletePopup : UIScreen
{
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


    protected override void OnShow()
    {
        Debug.Log("Game complete popup Opened");
    }

    public override bool OnBackPressed()
    {
        Debug.Log("Game complete back press");
        return false; 
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
