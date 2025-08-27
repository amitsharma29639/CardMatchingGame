
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    [SerializeField]
    TMP_Dropdown dropdown;

    [SerializeField] private Button playBtn;
    [SerializeField] private Toggle playSavedGame;

    private List<IHomeScreenEventsListner> listners;

    private void Awake()
    {
        dropdown.onValueChanged.AddListener(OnDropDownValueChange);
        playBtn.onClick.AddListener(OnPlayButtonClicked);
        playSavedGame.onValueChanged.AddListener(OnToggleValueChanged);
        listners = new List<IHomeScreenEventsListner>();
    }

    private void OnDropDownValueChange(int value)
    {
        foreach (var listner in listners)
        {
            listner.OnDropDownValueChanged(value);
        }
    }

    private void OnPlayButtonClicked()
    {
        foreach (var listner in listners)
        {
            listner.OnPlayButtonClicked();
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        foreach (var listner in listners)
        {
            listner.OnToggleValueChanged(isOn);
        }
    }

    protected override void OnShow()
    {
        Debug.Log("Main Menu Opened");
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
}

public interface IHomeScreenEventsListner
{
    void OnDropDownValueChanged(int value);
    void OnPlayButtonClicked();
    void OnToggleValueChanged(bool isOn);
}