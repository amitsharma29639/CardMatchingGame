

public class GameConfig
{
    private int rows;
    private int cols;

    private bool loadSavedGame;

    public GameConfig(int rows, int cols, bool loadSavedGame)
    {
        this.rows = rows;
        this.cols = cols;
        this.loadSavedGame = loadSavedGame;

        if (loadSavedGame)
        {
            rows = -1;
            cols = -1;
        }
    }
    
    public bool LoadSavedGame => loadSavedGame;

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
