namespace TicTacToe.Domain
{
    public class Field
    {
        public const int FieldSize = 9;

        public List<Player> Cells { get; set; } =
            [Player.None,Player.None,Player.None,
             Player.None,Player.None,Player.None,
             Player.None,Player.None,Player.None];

        public Field() { }

        private Field(List<Player> cells)
        {
            Cells = new List<Player>(cells);
        }

        public Field Clone()
        {
            return new Field(Cells);
        }
    }
}