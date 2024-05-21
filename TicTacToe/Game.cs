using System.Text;
using TicTacToe.Domain;

namespace TicTacToe
{
    public class Game
    {
        private const int FieldSize = 3;
        private const char PlayerXMark = 'X';
        private const char PlayerOMark = 'O';

        public void Start()
        {
            do
            {
                Console.WriteLine("Tic Tac Toe!!");
                Console.WriteLine("Enter the cell number to occupy it.");
                StartGame();
                Console.WriteLine("Enter 'y' to start a new game or any to exit.");
            }
            while (Console.ReadKey().KeyChar == 'y');
        }

        private void StartGame()
        {
            var gameEngine = new GameEngine();

            do
            {
                RenderField(gameEngine);

                var currentPlayerMark = gameEngine.GameState == GameState.PlayerXMove ? PlayerXMark : PlayerOMark;
                Console.WriteLine($"Player {currentPlayerMark}, it's your turn.");

                ProcessPlayerInput(gameEngine);
            }
            while (gameEngine.GameState == GameState.PlayerXMove || gameEngine.GameState == GameState.PlayerYMove);

            RenderField(gameEngine);

            if (gameEngine.GameState == GameState.Draw)
            {
                Console.WriteLine($"It's a draw (-_-)");
            }
            else
            {
                var winner = gameEngine.GameState == GameState.PlayerXWin ? PlayerXMark : PlayerOMark;
                Console.WriteLine($"Player {winner} is a WINNER!!!");
            }
        }

        private void RenderField(GameEngine gameEngine)
        {
            var stringBuilder = new StringBuilder();

            for (var row = 0; row < FieldSize; ++row)
            {
                stringBuilder.Append('|');

                for (var col = 0; col < FieldSize; ++col)
                {
                    var fieldIndex = row * FieldSize + col;
                    var fieldValue = gameEngine.Field.Cells[fieldIndex];

                    if (fieldValue == Player.None)
                        stringBuilder.Append(fieldIndex + 1);
                    else if (fieldValue == Player.X)
                        stringBuilder.Append(PlayerXMark);
                    else stringBuilder.Append(PlayerOMark);
                    stringBuilder.Append('|');

                }
                stringBuilder.Append(Environment.NewLine);
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        private void ProcessPlayerInput(GameEngine gameEngine)
        {
            var selectedСell = 0;
            while (selectedСell == 0)
            {
                var playerInput = Console.ReadLine();
                if (Int32.TryParse(playerInput, out var inputValue) && 
                    gameEngine.HandlePlayerMove(new PlayerMove { CellNumber = inputValue }))
                {
                    selectedСell = inputValue;
                }
                else
                {
                    var currentPlayerMark = gameEngine.GameState == GameState.PlayerXMove ? PlayerXMark : PlayerOMark;
                    Console.WriteLine($"Player {currentPlayerMark} entered an incorrect cell number, please enter again.");
                }
            }
        }
    }
}