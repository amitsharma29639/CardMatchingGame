
using UnityEngine;
using UnityEngine.UI;

public class GameCompletePopup : UIScreen
{
    [SerializeField] private Button homeBtn;

    private void Awake()
    {
     
        homeBtn.onClick.AddListener(OnHomeButtonClicked);
      
    }

    private void OnHomeButtonClicked()
    {
      
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
