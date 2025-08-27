

using System;
using UnityEngine;

public class StartGame : MonoBehaviour, IHomeScreenEventsListner
{

    [SerializeField] private GameObject gamePlayPrefab;

    private GameObject cardGridObj;

    private GameConfig config;

    private void Awake()
    {
        config = new GameConfig();
    }

    private void Start()
    {
        MainMenuScreen screen = (MainMenuScreen)UiScreenFactory.Instance.GetUIScreen(Constants.HOME_SCREEN);
        screen.Init();
        screen.AddListner(this);
        UIManager.Instance.PushScreen(screen);
    }

    public void OnDropDownValueChanged(int value)
    {
        switch (value)
        {
          case 0:
              config.Rows = 2;
              config.Cols = 2;
              break;
          case 1:
              config.Rows = 2;
              config.Cols = 3;
              break;
          case 2:
              config.Rows = 3;
              config.Cols = 4;
              break;
          case 3:
              config.Rows = 3;
              config.Cols = 6;
              break;
          case 4:
              config.Rows = 4;
              config.Cols = 4;
              break;
          case 5:
              config.Rows = 4;
              config.Cols = 6;
              break;
        }
        
    }
    public void OnPlayButtonClicked()
    {
        cardGridObj = GameObject.Instantiate(gamePlayPrefab , transform);
        CardsGridManager manager = cardGridObj.GetComponent<CardsGridManager>();
        manager.Init(config);
    }

    public void OnToggleValueChanged(bool isOn)
    {
        config.LoadSavedGame = isOn;
    }
}
