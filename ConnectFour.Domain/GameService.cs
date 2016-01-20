using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectFour.Domain
{
    public class GameService
    {
        public GameService()
        {
            GameStatus = GameStatus.NoWinner;
        }

        public GameStatus GameStatus { get; set; }

        public void OnCounterAdded(object source, EventArgs e)
        {
            Grid grid = source as Grid;

            if (grid != null)
            {
                this.Horizontal(grid);

                if (this.GameStatus == GameStatus.NoWinner)
                {
                    this.Vertical(grid);
                }

                if (this.GameStatus == GameStatus.NoWinner)
                {
                    this.Diagonal(grid);
                }
                
                if (this.GameStatus == GameStatus.NoWinner && grid.Counters.All(c => c.PlayerType != PlayerType.NotAssigned))
                {
                    this.GameStatus = GameStatus.NoWinnerGridFull;
                }
            }
        }

        public void Horizontal(Grid grid)
        {
            for (int rowLoop = 1; rowLoop < grid.NumberOfRows; rowLoop++)
            {
                int connectingHumanCounters = 0;
                int connectingComputerCounters = 0;

                foreach (Counter counter in grid.Counters.Where(c => c.Row == rowLoop))
                {
                    switch (counter.PlayerType)
                    {
                        case PlayerType.Human:
                            connectingHumanCounters += 1;
                            connectingComputerCounters = 0;
                            break;
                        case PlayerType.Computer:
                            connectingComputerCounters += 1;
                            connectingHumanCounters = 0;
                            break;
                        case PlayerType.NotAssigned:
                            connectingHumanCounters = 0;
                            connectingComputerCounters = 0;
                            break;
                    }

                    if (connectingHumanCounters == 4)
                    {
                        this.GameStatus = GameStatus.HumanWon;
                    }

                    if (connectingComputerCounters == 4)
                    {
                        this.GameStatus = GameStatus.ComputerWon;
                    }
                }
            }
        }

        public void Vertical(Grid grid)
        {
            for (int columnLoop = 0; columnLoop <= grid.NumberOfColumns; columnLoop++)
            {
                int connectingHumanCounters = 0;
                int connectingComputerCounters = 0;

                foreach (Counter counter in grid.Counters.Where(c => c.Column == columnLoop))
                {
                    switch (counter.PlayerType)
                    {
                        case PlayerType.Human:
                            connectingHumanCounters += 1;
                            connectingComputerCounters = 0;
                            break;
                        case PlayerType.Computer:
                            connectingComputerCounters += 1;
                            connectingHumanCounters = 0;
                            break;
                        case PlayerType.NotAssigned:
                            connectingHumanCounters = 0;
                            connectingComputerCounters = 0;
                            break;
                    }

                    if (connectingHumanCounters == 4)
                    {
                        this.GameStatus = GameStatus.HumanWon;
                    }

                    if (connectingComputerCounters == 4)
                    {
                        this.GameStatus = GameStatus.ComputerWon;
                    }
                }
            }
        }

        public void Diagonal(Grid grid)
        {
            List<Counter> humanCounters = grid.Counters.Where(c => c.PlayerType == PlayerType.Human).ToList();
            List<Counter> computerCounters = grid.Counters.Where(c => c.PlayerType == PlayerType.Computer).ToList();

            foreach (Counter counter in humanCounters)
            {
                if (humanCounters.Any(c => c.Column == (counter.Column + 1) && c.Row == (counter.Row + 1)) &&
                    humanCounters.Any(c => c.Column == (counter.Column + 2) && c.Row == (counter.Row + 2)) &&
                    humanCounters.Any(c => c.Column == (counter.Column + 3) && c.Row == (counter.Row + 3)))
                {
                    this.GameStatus = GameStatus.HumanWon;
                }

                if (humanCounters.Any(c => c.Column == (counter.Column - 1) && c.Row == (counter.Row + 1)) &&
                    humanCounters.Any(c => c.Column == (counter.Column - 2) && c.Row == (counter.Row + 2)) &&
                    humanCounters.Any(c => c.Column == (counter.Column - 3) && c.Row == (counter.Row + 3)))
                {
                    this.GameStatus = GameStatus.HumanWon;
                }
            }

            foreach (Counter counter in computerCounters)
            {
                if (computerCounters.Any(c => c.Column == (counter.Column + 1) && c.Row == (counter.Row + 1)) &&
                    computerCounters.Any(c => c.Column == (counter.Column + 2) && c.Row == (counter.Row + 2)) &&
                    computerCounters.Any(c => c.Column == (counter.Column + 3) && c.Row == (counter.Row + 3)))
                {
                    this.GameStatus = GameStatus.ComputerWon;
                }

                if (computerCounters.Any(c => c.Column == (counter.Column - 1) && c.Row == (counter.Row + 1)) &&
                    computerCounters.Any(c => c.Column == (counter.Column - 2) && c.Row == (counter.Row + 2)) &&
                    computerCounters.Any(c => c.Column == (counter.Column - 3) && c.Row == (counter.Row + 3)))
                {
                    this.GameStatus = GameStatus.ComputerWon;
                }
            }
        }
    }
}
