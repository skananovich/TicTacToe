using TicTacToe.Domain;

namespace TicTacToe
{
    public class GameEngine
    {
        private readonly Field field = new Field();
        private Player currentPlayer = Player.X;

        public Field Field { get { return field.Clone(); } }
        public GameState GameState { get; private set; } = GameState.InProgress;

        public bool HandlePlayerMove(PlayerMove playerMove)
        {
            ArgumentNullException.ThrowIfNull(playerMove);

            if (ValidatePlayerMove(playerMove) == false)
                return false;

            field.Cells[playerMove.CellNumber - 1] = (byte)currentPlayer;

            SwitchCurrentPlayer();

            return true;
        }

        private void SwitchCurrentPlayer()
        {
            currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
        }

        private bool ValidatePlayerMove(PlayerMove playerMove)
        {
            return playerMove.CellNumber > 0 
                && playerMove.CellNumber <= Field.FieldSize
                && field.Cells[playerMove.CellNumber - 1] == 0;
        }
    }
}
