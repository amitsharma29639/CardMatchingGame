using UnityEngine;
public class UiScreenFactory : MonoBehaviour
{
    public static UiScreenFactory Instance { get; private set; }
    [SerializeField] private MainMenuScreen homeScreen;
    [SerializeField] private GameCompletePopup gameCompletePopup;
    [SerializeField] private NoSavedGamePopup noSavedGamePopup;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one exists
            return;
        }

        Instance = this;
    }

    public UIScreen GetUIScreen(int id)
    {
        switch (id)
        {
            case Constants.HOME_SCREEN:
                return homeScreen;
            case Constants.GAME_COMPLETE_POPUP:
                return gameCompletePopup;
            case Constants.NO_SAVED_GAME_POPUP:
                return noSavedGamePopup;
        }

        return null;
    }
}
