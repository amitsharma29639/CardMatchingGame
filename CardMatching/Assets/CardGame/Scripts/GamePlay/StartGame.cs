using UnityEngine;
public class StartGame : MonoBehaviour, IHomeScreenEventsListner
{

    [SerializeField] private GameObject gamePlayPrefab;

    private GameObject cardGridObj;

    private int rows;
    private int cols;
    private bool isOn;

    private void Start()
    {
        rows = 2;
        cols = 2;
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
              rows = 2;
              cols = 2;
              break;
          case 1:
              rows = 2;
              cols = 3;
              break;
          case 2:
              rows = 3;
              cols = 4;
              break;
          case 3:
              rows = 3;
              cols = 6;
              break;
          case 4:
              rows = 4;
              cols = 4;
              break;
          case 5:
              rows = 4;
              cols = 6;
              break;
        }
    }
    public void OnPlayButtonClicked()
    {
        cardGridObj = GameObject.Instantiate(gamePlayPrefab , transform);
        CardsGridManager manager = cardGridObj.GetComponent<CardsGridManager>();
        GameConfig config = new GameConfig();
        config.Rows = rows;
        config.Cols = cols;
        config.LoadSavedGame = isOn;
        
        manager.Init(config);
    }
    public void OnToggleValueChanged(bool isOn)
    {
        this.isOn = isOn;
    }
}
