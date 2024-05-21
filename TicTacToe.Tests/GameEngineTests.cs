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

        [Test]
        public void Player_Can_Move_Again_After_An_Incorrect_Move()
        {
            var playerXMove = new PlayerMove() { CellNumber = 3 };
            var playerOMove1 = new PlayerMove() { CellNumber = 3 };
            var playerOMove2 = new PlayerMove() { CellNumber = 4 };

            gameEngine.HandlePlayerMove(playerXMove);
            gameEngine.HandlePlayerMove(playerOMove1);
            gameEngine.HandlePlayerMove(playerOMove2);

            gameEngine.Field.Cells[3].Should().Be((byte)Player.O);
        }

        // Horizontal combinations
        [TestCase(1, 4, 2, 5, 3)]
        [TestCase(4, 1, 5, 2, 6)]
        [TestCase(7, 1, 8, 2, 9)]
        // Vertical combinations
        [TestCase(1, 2, 4, 5, 7)]
        [TestCase(2, 3, 5, 6, 8)]
        [TestCase(3, 2, 6, 5, 9)]
        // Diagonal combinations
        [TestCase(1, 2, 5, 3, 9)]
        [TestCase(3, 2, 5, 4, 7)]
        public void Player_Must_Win_With_A_Winning_Combination_Of_Moves(
            int xMove1, int oMove1,
            int xMove2, int oMove2,
            int xMove3)
        {
            var playerXMove1 = new PlayerMove() { CellNumber = xMove1 };
            var playerOMove1 = new PlayerMove() { CellNumber = oMove1 };
            var playerXMove2 = new PlayerMove() { CellNumber = xMove2 };
            var playerOMove2 = new PlayerMove() { CellNumber = oMove2 };
            var playerXMove3 = new PlayerMove() { CellNumber = xMove3 };

            gameEngine.HandlePlayerMove(playerXMove1);
            gameEngine.HandlePlayerMove(playerOMove1);
            gameEngine.HandlePlayerMove(playerXMove2);
            gameEngine.HandlePlayerMove(playerOMove2);
            gameEngine.HandlePlayerMove(playerXMove3);

            gameEngine.GameState.Should().Be(GameState.PlayerXWin);
        }

        [TestCase(1, 4, 2, 5, 6, 3, 7, 8, 9)]
        [TestCase(1, 5, 3, 2, 8, 9, 6, 4, 7)]
        public void Game_Must_End_In_A_Draw_With_A_Draw_Combination_Of_Moves(
            int xMove1, int oMove1,
            int xMove2, int oMove2,
            int xMove3, int oMove3,
            int xMove4, int oMove4,
            int xMove5)
        {
            var playerXMove1 = new PlayerMove() { CellNumber = xMove1 };
            var playerOMove1 = new PlayerMove() { CellNumber = oMove1 };
            var playerXMove2 = new PlayerMove() { CellNumber = xMove2 };
            var playerOMove2 = new PlayerMove() { CellNumber = oMove2 };
            var playerXMove3 = new PlayerMove() { CellNumber = xMove3 };
            var playerOMove3 = new PlayerMove() { CellNumber = oMove3 };
            var playerXMove4 = new PlayerMove() { CellNumber = xMove4 };
            var playerOMove4 = new PlayerMove() { CellNumber = oMove4 };
            var playerXMove5 = new PlayerMove() { CellNumber = xMove5 };

            gameEngine.HandlePlayerMove(playerXMove1);
            gameEngine.HandlePlayerMove(playerOMove1);
            gameEngine.HandlePlayerMove(playerXMove2);
            gameEngine.HandlePlayerMove(playerOMove2);
            gameEngine.HandlePlayerMove(playerXMove3);
            gameEngine.HandlePlayerMove(playerOMove3);
            gameEngine.HandlePlayerMove(playerXMove4);
            gameEngine.HandlePlayerMove(playerOMove4);
            gameEngine.HandlePlayerMove(playerXMove5);

            gameEngine.GameState.Should().Be(GameState.Draw);
        }

        [Test]
        public void Player_Cannot_Make_A_Move_After_Winning()
        {
            var playerXMove1 = new PlayerMove() { CellNumber = 1 };
            var playerOMove1 = new PlayerMove() { CellNumber = 4 };
            var playerXMove2 = new PlayerMove() { CellNumber = 2 };
            var playerOMove2 = new PlayerMove() { CellNumber = 5 };
            var playerXMove3 = new PlayerMove() { CellNumber = 3 };
            var playerOMove3 = new PlayerMove() { CellNumber = 6 };

            gameEngine.HandlePlayerMove(playerXMove1);
            gameEngine.HandlePlayerMove(playerOMove1);
            gameEngine.HandlePlayerMove(playerXMove2);
            gameEngine.HandlePlayerMove(playerOMove2);
            gameEngine.HandlePlayerMove(playerXMove3);
            var result = gameEngine.HandlePlayerMove(playerOMove3);

            result.Should().BeFalse();
        }
    }
}
