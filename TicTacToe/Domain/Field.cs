namespace TicTacToe.Domain
{
    public class Field
    {
        public List<byte> Cells { get; set; } =
            [0,0,0,
             0,0,0,
             0,0,0];

        public Field() { }

        private Field(List<byte> cells)
        {
            Cells = new List<byte>(cells);
        }

        public Field Clone()
        {
            return new Field(Cells);
        }
    }
}