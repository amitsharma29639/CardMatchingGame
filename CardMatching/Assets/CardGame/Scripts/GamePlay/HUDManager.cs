using System;
using System.Collections;
using System.Collections.Generic;
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private Button saveGameBtn;
    [SerializeField] private Button homeBtn;

    private List<IHUDEventsListner> listners;

    private void Awake()
    {
        saveGameBtn.onClick.AddListener(OnClickSaveBtn);
        homeBtn.onClick.AddListener(OnClickHomeBtn);
        listners = new List<IHUDEventsListner>();
    }

    private void OnClickHomeBtn()
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        foreach (var listener in listners)
        {
            listener.OnHomeBtnClicked();
        }
    }

    private void OnClickSaveBtn()
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        foreach (var listener in listners)
        {
            listener.OnSaveBtnClicked();
        }
    }

    public void OnScoreUpdate(int score)
    {
        scoreText.text = "Score:" + score;
    }

    public void OnTurnsUpdate(int turns)
    {
        turnsText.text = "Turns:"+turns;
    }
    
    public void AddListener(IHUDEventsListner listener)
    {
        listners.Add(listener);
    }

    public void RemoveListener(IHUDEventsListner listener)
    {
        listners.Remove(listener);
    }

    private void ClearListeners()
    {
        listners.Clear();
    }
    
    private void OnDestroy()
    {
        ClearListeners();
    }
    
}

public interface IHUDEventsListner
{
    void OnHomeBtnClicked();
    void OnSaveBtnClicked();
}
