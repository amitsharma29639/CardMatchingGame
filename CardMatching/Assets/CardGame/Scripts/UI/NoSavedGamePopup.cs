using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.UI;

public class NoSavedGamePopup : UIScreen
{
    [SerializeField] private Button homeBtn;

    private void Awake()
    {
     
        homeBtn.onClick.AddListener(OnHomeButtonClicked);
      
    }

    private void OnHomeButtonClicked()
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
        UIManager.Instance.PopScreen();
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
}
