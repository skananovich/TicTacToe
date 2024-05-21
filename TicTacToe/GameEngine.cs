using TicTacToe.Domain;

namespace TicTacToe
{
    public class GameEngine
    {
        public readonly Field Field = new Field();
        public GameState GameState { get; private set; } = GameState.InProgress;

        public void HandlePlayerMove(PlayerMove playerMove)
        {

        }
    }
}
