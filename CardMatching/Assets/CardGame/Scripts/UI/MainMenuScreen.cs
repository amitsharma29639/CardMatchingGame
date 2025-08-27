
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField] private Button playBtn;
    [SerializeField] private Toggle playSavedGame;
    [SerializeField] private Image fadeImage;

    private List<IHomeScreenEventsListner> listners;
    public void Init()
    {
        dropdown.onValueChanged.AddListener(OnDropDownValueChange);
        playBtn.onClick.AddListener(OnPlayButtonClicked);
        playSavedGame.onValueChanged.AddListener(OnToggleValueChanged);
        listners = new List<IHomeScreenEventsListner>();
    }

    private void OnDropDownValueChange(int value)
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        foreach (var listner in listners)
        {
            listner.OnDropDownValueChanged(value);
        }
    }

    private void OnPlayButtonClicked()
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);

        if (playSavedGame.isOn)
        {
            if (!CheckSavedGame())
            {
                UIManager.Instance.PushScreen(UiScreenFactory.Instance.GetUIScreen(Constants.NO_SAVED_GAME_POPUP));
                playSavedGame.isOn = false;
                return;
            }
        }
        
        fadeImage.gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();
       
        seq.Append(fadeImage.DOFade(1f, 0.5f).OnComplete(() =>
        {
            foreach (var listner in listners)
            {
                listner.OnPlayButtonClicked();
            }

            fadeImage.color = new Color(0, 0, 0, 0);
            fadeImage.gameObject.SetActive(false);
            UIManager.Instance.PopScreen();
        }));
    }

    private void OnToggleValueChanged(bool isOn)
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        

        foreach (var listner in listners)
        {
            listner.OnToggleValueChanged(isOn);
        }
    }

    public void AddListner(IHomeScreenEventsListner listner)
    {
        listners.Add(listner);
    }

    protected override void OnShow()
    {
    }

    public override bool OnBackPressed()
    {
        Debug.Log("Main menu on back pressed");
        return false; 
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveAllListeners();
        playBtn.onClick.RemoveAllListeners();
        playSavedGame.onValueChanged.RemoveAllListeners();
        listners.Clear();
    }

    private bool CheckSavedGame()
    {
        return File.Exists(Application.persistentDataPath + "/SaveGameData.json");
    }

}

public interface IHomeScreenEventsListner
{
    void OnDropDownValueChanged(int value);
    void OnPlayButtonClicked();
    void OnToggleValueChanged(bool isOn);
}