using TicTacToe.Domain;

namespace TicTacToe
{
    public class GameEngine
    {
        public readonly Field field = new Field();
        public GameState GameState { get; private set; }

        public void HandlePlayerMove(PlayerMove playerMove)
        {

        }
    }
}
