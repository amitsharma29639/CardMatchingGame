using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSaveTest : MonoBehaviour
{
    [SerializeField]
    private CardsGridManager cardsGridManager;

    private void Awake()
    {
       Button btn = GetComponent<Button>();
       btn.onClick.AddListener(() =>
       {
           cardsGridManager.SaveGame();
       });
    }
}
