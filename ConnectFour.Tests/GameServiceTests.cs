using System.ComponentModel;
using System.Linq;
using ConnectFour.Domain;
using NUnit.Framework;

namespace ConnectFour.Tests
{
    [TestFixture]
    public class GameServiceTests
    {
        [Test]
        public void Human_Can_Win_Game_Horizontally()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 1).PlayerType = PlayerType.Human;

            //Act
            gameService.Horizontal(grid);

            //Assert
            Assert.AreEqual(GameStatus.HumanWon, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Horizontally_With_Three_Connecting_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 1).PlayerType = PlayerType.Human;

            //Act
            gameService.Horizontal(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Horizontally_If_There_Is_Gap_Inbetween_Four_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 5 && c.Row == 1).PlayerType = PlayerType.Human;

            //Act
            gameService.Horizontal(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Win_Game_Horizontally_In_The_Middle_Of_The_Grid()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 1).PlayerType = PlayerType.Computer;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 5 && c.Row == 1).PlayerType = PlayerType.Human;

            //Winning moves
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 5 && c.Row == 2).PlayerType = PlayerType.Human;

            //Act
            gameService.Horizontal(grid);

            //Assert
            Assert.AreEqual(GameStatus.HumanWon, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Win_Game_Vertically()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 3).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 4).PlayerType = PlayerType.Human;

            //Act
            gameService.Vertical(grid);

            //Assert
            Assert.AreEqual(GameStatus.HumanWon, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Vertically_With_Three_Connecting_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 3).PlayerType = PlayerType.Human;

            gameService.Vertical(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Vertically_If_Computer_Has_Counter_Inbetween_Four_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 3).PlayerType = PlayerType.Computer;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 4).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 5).PlayerType = PlayerType.Human;

            gameService.Vertical(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Win_Game_Vertically_In_The_Middle_Of_The_Grid()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act

            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 1).PlayerType = PlayerType.Computer;

            //Winning moves
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 3).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 4).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 5).PlayerType = PlayerType.Human;

            gameService.Vertical(grid);

            //Assert
            Assert.AreEqual(GameStatus.HumanWon, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Win_Game_Diagonally_To_The_Right()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 3).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 4).PlayerType = PlayerType.Human;

            gameService.Diagonal(grid);

            //Assert
            Assert.AreEqual(GameStatus.HumanWon, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Win_Game_Diagonally_To_The_Left()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 6 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 5 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 3).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 4).PlayerType = PlayerType.Human;

            gameService.Diagonal(grid);

            //Assert
            Assert.AreEqual(GameStatus.HumanWon, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Diagonally_With_Three_Connecting_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 3).PlayerType = PlayerType.Human;

            gameService.Diagonal(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Diagonally_With_Gap_Inbetween_Four_Connecting_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 4).PlayerType = PlayerType.Human;

            gameService.Diagonal(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }

        [Test]
        public void Human_Can_Not_Win_Game_Diagonally_With_Computer_Counter_Inbetween_Four_Connecting_Counters()
        {
            //Arrange
            Grid grid = new Grid(6, 7);
            GameService gameService = new GameService();

            //Act
            grid.Counters.SingleOrDefault(c => c.Column == 1 && c.Row == 1).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 2 && c.Row == 2).PlayerType = PlayerType.Human;
            grid.Counters.SingleOrDefault(c => c.Column == 3 && c.Row == 3).PlayerType = PlayerType.Computer;
            grid.Counters.SingleOrDefault(c => c.Column == 4 && c.Row == 4).PlayerType = PlayerType.Human;

            gameService.Diagonal(grid);

            //Assert
            Assert.AreEqual(GameStatus.NoWinner, gameService.GameStatus);
        }
    }
}
