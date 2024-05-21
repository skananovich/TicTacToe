using TicTacToe.Domain;

namespace TicTacToe
{
    public class GameEngine
    {
        private readonly Field field = new Field();
        private readonly List<List<int>> WinCombination =
            [
                // Rows
                [0, 1, 2], [3, 4, 5], [6, 7, 8],
                // Columns
                [0, 3, 6], [1, 4, 7], [2, 5, 8],
                // Diagonals
                [0, 4, 8], [2, 4, 6]
            ];

        private Player currentPlayer = Player.X;

        public Field Field { get { return field.Clone(); } }
        public GameState GameState { get; private set; } = GameState.InProgress;

        public bool HandlePlayerMove(PlayerMove playerMove)
        {
            ArgumentNullException.ThrowIfNull(playerMove);

            if (ValidatePlayerMove(playerMove) == false)
                return false;

            MarkMoveOnField(playerMove);

            if (CheckWin())
            {
                GameState = currentPlayer == Player.X ? GameState.PlayerXWin : GameState.PlayerYWin;
            }
            else if (CheckDraw())
            {
                GameState = GameState.Draw;
            }

            SwitchCurrentPlayer();

            return true;
        }

        private void MarkMoveOnField(PlayerMove playerMove)
        {
            field.Cells[playerMove.CellNumber - 1] = (byte)currentPlayer;
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

        private bool CheckWin()
        {
            return WinCombination.Any(
                combination => field
                    .Cells
                    .Where((_, index) => combination.Contains(index))
                    .All(cell => cell == (byte)currentPlayer));
        }

        private bool CheckDraw()
        {
            return field.Cells.All(cell => cell != 0);
        }
    }
}
