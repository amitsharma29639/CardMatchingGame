
using System;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    
    private GameConfig gameConfig;
    
    private CardsGridManager cardsGridManager;
    private void Awake()
    {
        cardsGridManager = GetComponent<CardsGridManager>();
        gameConfig = new GameConfig(2 , 2, true);
    }

    private void Start()
    {
        cardsGridManager.Init(gameConfig);
    }
}
