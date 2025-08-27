

public class GameConfig
{
    private int rows;
    private int cols;

    private bool loadSavedGame;

    public GameConfig()
    {
        this.rows = 2;
        this.cols = 2;
        this.loadSavedGame = false;
    }

    public bool LoadSavedGame
    {
        get { return loadSavedGame; }
        set
        {
            loadSavedGame = value;
        }
    }

    public int Rows
    {
        get => rows;
        set => rows = value;
    }

    public int Cols
    {
        get => cols;
        set => cols = value;
    }
}
