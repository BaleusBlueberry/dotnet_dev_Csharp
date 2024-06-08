using Four_in_a_row.Enums;

namespace Tic_Tac_Toe.Controls;

public class GameEndEventArgs : EventArgs
{
    public GameEndEventArgs(GameResult result)
    {
        GameResult = result;

    }

    public GameResult GameResult { get; set; }
}