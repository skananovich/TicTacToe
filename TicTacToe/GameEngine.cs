using TicTacToe.Domain;

namespace TicTacToe
{
    public class GameEngine
    {
        private readonly Field field = new();
        private readonly List<List<int>> WinCombination =
            [
                // Rows
                [0, 1, 2], [3, 4, 5], [6, 7, 8],
                // Columns
                [0, 3, 6], [1, 4, 7], [2, 5, 8],
                // Diagonals
                [0, 4, 8], [2, 4, 6]
            ];

        public Field Field { get { return field.Clone(); } }
        public GameState GameState { get; private set; } = GameState.PlayerXMove;

        public bool HandlePlayerMove(PlayerMove playerMove)
        {
            ArgumentNullException.ThrowIfNull(playerMove);

            if (ValidatePlayerMove(playerMove) == false)
                return false;

            MarkMoveOnField(playerMove);

            if (CheckWin())
            {
                GameState = GameState == GameState.PlayerXMove ? GameState.PlayerXWin : GameState.PlayerYWin;
            }
            else if (CheckDraw())
            {
                GameState = GameState.Draw;
            }
            else
            {
                SwitchCurrentPlayer();
            }

            return true;
        }

        private void MarkMoveOnField(PlayerMove playerMove)
        {
            field.Cells[playerMove.CellNumber - 1] = GetCurrentPlayer();
        }

        private void SwitchCurrentPlayer()
        {
            GameState = GameState == GameState.PlayerXMove ? GameState.PlayerYMove : GameState.PlayerXMove;
        }

        private bool ValidatePlayerMove(PlayerMove playerMove)
        {
            return (GameState == GameState.PlayerXMove || GameState == GameState.PlayerYMove)
                && playerMove.CellNumber > 0 
                && playerMove.CellNumber <= Field.FieldSize
                && field.Cells[playerMove.CellNumber - 1] == Player.None;
        }

        private bool CheckWin()
        {
            var currentPlayer = GetCurrentPlayer();
            return WinCombination.Any(
                combination => field
                    .Cells
                    .Where((_, index) => combination.Contains(index))
                    .All(cell => cell == currentPlayer));
        }

        private bool CheckDraw()
        {
            return field.Cells.All(cell => cell != Player.None);
        }

        private Player GetCurrentPlayer()
        {
            return GameState == GameState.PlayerXMove ? Player.X : Player.O;
        }
    }
}
