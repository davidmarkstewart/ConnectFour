using System;
using System.Linq;
using ConnectFour.Domain;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Grid grid = new Grid(6, 7);
                GameService gameService = new GameService();

                grid.CounterAdded += gameService.OnCounterAdded;

                while (gameService.GameStatus == GameStatus.NoWinner)
                {
                    if (!grid.IsComputersTurn)
                    {
                        WriteGridToConsole(grid);

                        string column = Console.ReadLine();

                        if (!grid.IsValidUserInput(column))
                        {
                            Console.WriteLine("Please enter a valid column number.");

                            continue;
                        }

                        if (!grid.IsColumnFull(Convert.ToInt32(column)))
                        {
                            grid.AddCounter(new Counter
                            {
                                Column = Convert.ToInt32(column),
                                PlayerType = PlayerType.Human
                            });

                            grid.IsComputersTurn = true;
                        }
                        else
                        {
                            Console.WriteLine("That column is full please select another.");
                        }
                    }
                    else
                    {
                        grid.TakeComputersTurn();

                        grid.IsComputersTurn = false;
                    }
                }

                WriteGridToConsole(grid, true);

                WriteGameResultToConsole(gameService);
            }
        }

        private static void WriteGridToConsole(Grid grid, bool isGameOver = false)
        {
            for (int r = grid.NumberOfRows; r > 0; r--)
            {
                Console.Write("|");

                for (int c = 1; c <= grid.NumberOfColumns; c++)
                {
                    if (grid.Counters.Any(counter => counter.PlayerType == PlayerType.Human && counter.Row == r && counter.Column == c))
                    {
                        Console.Write(" O |");
                    }
                    else if (grid.Counters.Any(counter => counter.PlayerType == PlayerType.Computer && counter.Row == r && counter.Column == c))
                    {
                        Console.Write(" X |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }

                Console.Write(Environment.NewLine);
            }

            if (!isGameOver)
            {
                Console.WriteLine("Please enter a column number. (1-6)" + Environment.NewLine);
            }
        }

        private static void WriteGameResultToConsole(GameService gameService)
        {
            switch (gameService.GameStatus)
            {
                case GameStatus.HumanWon:
                    Console.WriteLine("Congratulations, you won! Press any key to start another game.");
                    break;
                case GameStatus.ComputerWon:
                    Console.WriteLine("Oh no the computer won. Press any key to start another game.");
                    break;
                case GameStatus.NoWinnerGridFull:
                    Console.WriteLine("The grid is full and no one won. Press any key to start another game.");
                    break;
            }

            Console.ReadKey();
        }
    }
}