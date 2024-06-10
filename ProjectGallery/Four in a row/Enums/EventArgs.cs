using Four_in_a_row.Enums;

namespace Four_in_a_row.Enums;

public class GameEndEventArgs : EventArgs
{
    public GameEndEventArgs(GameResult result)
    {
        GameResult = result;
    }

    public GameResult GameResult { get; set; }
}