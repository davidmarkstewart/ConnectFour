namespace ConnectFour.Domain
{
    public enum PlayerType
    {
        Human = 1,
        Computer = 2,
        NotAssigned = 3
    }

    public enum GameStatus
    {
        HumanWon = 1,
        ComputerWon = 2,
        NoWinner = 3,
        NoWinnerGridFull = 4
    }
}
