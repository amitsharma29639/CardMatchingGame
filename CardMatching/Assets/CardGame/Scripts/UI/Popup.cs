using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : UIScreen
{
    protected override void OnShow()
    {
        Debug.Log("Popup Opened");
    }

    public override bool OnBackPressed()
    {
        Debug.Log("On back pressed popup");

        return false; 
    }
}
