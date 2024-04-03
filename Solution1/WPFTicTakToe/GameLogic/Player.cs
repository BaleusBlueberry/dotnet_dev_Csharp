namespace WPFTicTakToe.GameLogic;

internal abstract class Player
{
    public char Symbol { get; set; }
    public string Name { get; set; }

    public Player(string name, char symbol)
    {
        Symbol = symbol;
        Name = name;
    }

    public abstract void MakeMove();
}
internal class HumanPlayer : Player
{

    public HumanPlayer(string name, char symbol) : base(name, symbol)
    {

    }
    public override void MakeMove()
    {
        throw new NotImplementedException();
    }

}
internal class ComputerPlayer : Player
{
    public ComputerPlayer(char symbol) :
        base("Computer", symbol)
    {

    }
    public override void MakeMove()
    {
        throw new NotImplementedException();
    }
}

