using FluentAssertions;
using NUnit.Framework;
using TicTacToe.Domain;

namespace TicTacToe.Tests
{
    public class GameEngineTests
    {
        private GameEngine gameEngine;

        [SetUp]
        public void Init()
        {
            gameEngine = new GameEngine();
        }

        [Test]
        public void Field_Contains_Nine_Cells()
        {
            gameEngine.Field.Cells.Should().HaveCount(9);
        }

        [Test]
        public void Field_Is_Empty_At_The_Start_Of_The_Game()
        {
            gameEngine.Field.Cells.Should().OnlyContain(item => item == 0);
        }

        [Test]
        public void We_Have_No_Winner_At_The_Start_Of_The_Game()
        {
            gameEngine.GameState.Should().NotBe(GameState.PlayerXWin).And.NotBe(GameState.PlayerYWin);
        }

        [Test]
        public void We_Have_No_Draw_At_The_Start_Of_The_Game()
        {
            gameEngine.GameState.Should().NotBe(GameState.Draw);
        }

        [Test]
        public void Invalid_Player_Move_Throws_Exception()
        {
            Action action = () => gameEngine.HandlePlayerMove(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void PlayerX_Goes_First()
        {
            var playerMove = new PlayerMove() { CellNumber = 1 };

            gameEngine.HandlePlayerMove(playerMove);

            gameEngine.Field.Cells[0].Should().Be((byte)Player.X);
        }

        [Test]
        public void PlayerO_Goes_Second()
        {
            var playerXMove = new PlayerMove() { CellNumber = 1 };
            var playerYMove = new PlayerMove() { CellNumber = 2 };

            gameEngine.HandlePlayerMove(playerXMove);
            gameEngine.HandlePlayerMove(playerYMove);

            gameEngine.Field.Cells[1].Should().Be((byte)Player.O);
        }

        [Test]
        public void Moving_Outside_The_Field_Is_Not_Allowed()
        {
            var playerXMove = new PlayerMove() { CellNumber = 15 };

            var result = gameEngine.HandlePlayerMove(playerXMove);

            result.Should().BeFalse();
        }

        [Test]
        public void Move_To_Occupied_Cell_Is_Not_Allowed()
        {
            var playerXMove = new PlayerMove() { CellNumber = 3 };
            var playerOMove = new PlayerMove() { CellNumber = 3 };

            gameEngine.HandlePlayerMove(playerXMove);
            var result = gameEngine.HandlePlayerMove(playerOMove);

            result.Should().BeFalse();
        }
    }
}
