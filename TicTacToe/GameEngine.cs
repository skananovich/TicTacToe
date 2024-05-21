using TicTacToe.Domain;

namespace TicTacToe
{
    public class GameEngine
    {
        private readonly Field field = new Field();
        private Player currentPlayer = Player.X;

        public Field Field { get { return field.Clone(); } }
        public GameState GameState { get; private set; } = GameState.InProgress;

        public void HandlePlayerMove(PlayerMove playerMove)
        {
            ArgumentNullException.ThrowIfNull(playerMove);

            field.Cells[playerMove.CellNumber - 1] = (byte)currentPlayer;
        }
    }
}
