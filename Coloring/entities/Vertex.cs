namespace Coloring.entities
{
    internal class Vertex
    {
        public String labelColumn;
        public String labelRow;
        public int color;
        public double weight;

        public Vertex(String labelColumn, String labelRow)
        {
            this.labelColumn = labelColumn;
            this.labelRow = labelRow;
            this.color = 0;
            this.weight = 0;
        }
    }
}
