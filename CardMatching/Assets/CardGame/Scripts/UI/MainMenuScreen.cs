using UnityEngine;

public class MainMenuScreen : UIScreen
{
    protected override void OnShow()
    {
        Debug.Log("Main Menu Opened");
    }

    public override bool OnBackPressed()
    {
        Debug.Log("Main menu on back pressed");
        return false; 
    }
}