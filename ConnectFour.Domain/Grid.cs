using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectFour.Domain
{
    public class Grid
    {
        public Grid(int numberOfColumns, int numberOfRows)
        {
            this.Counters = new List<Counter>();
            this.NumberOfColumns = numberOfColumns;
            this.NumberOfRows = numberOfRows;

            for (int rowLoop = 1; rowLoop <= this.NumberOfRows; rowLoop++)
            {
                for (int columnLoop = 1; columnLoop <= this.NumberOfColumns; columnLoop++)
                {
                    this.Counters.Add(new Counter
                    {
                        PlayerType = PlayerType.NotAssigned,
                        Row = rowLoop,
                        Column = columnLoop
                    });
                }
            }
        }

        public int NumberOfColumns { get; set; }

        public int NumberOfRows { get; set; }

        public bool IsComputersTurn { get; set; }

        public List<Counter> Counters { get; set; }

        public void AddCounter(Counter counter)
        {
            int numberOfCountersInColumn =
                Counters.Count(c => c.Column == counter.Column && c.PlayerType != PlayerType.NotAssigned);

            counter.Row = numberOfCountersInColumn + 1;

            Counters.SingleOrDefault(c => c.Column == counter.Column && c.Row == counter.Row).PlayerType = counter.PlayerType;

            OnCounterAdded();
        }

        public void TakeComputersTurn()
        {
            Random random = new Random();

            bool moveTaken = false;

            while (!moveTaken)
            {
                int randomColumn = random.Next(1, NumberOfColumns + 1);

                if (!IsColumnFull(randomColumn))
                {
                    AddCounter(new Counter { Column = randomColumn, PlayerType = PlayerType.Computer });

                    moveTaken = true;
                }
            }
        }

        public bool IsColumnFull(int column)
        {
            return Counters.Count(c => c.Column == column && c.PlayerType != PlayerType.NotAssigned) == NumberOfRows;
        }

        public delegate void CounterAddedEventHandler(object source, EventArgs e);

        public event CounterAddedEventHandler CounterAdded;

        protected virtual void OnCounterAdded()
        {
            if (CounterAdded != null)
            {
                CounterAdded(this, EventArgs.Empty);
            }
        }

        public bool IsValidUserInput(string input)
        {
            int convertedInput;

            if (!int.TryParse(input, out convertedInput))
            {
                return false;
            }

            if (convertedInput > this.NumberOfColumns || convertedInput <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
