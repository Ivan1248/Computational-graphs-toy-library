using System.IO;
using System.Text;

namespace LinAlg
{
    static class Util
    {
        public static Matrix LoadMatrixFromFile(string filePath) => 
            LoadMatrixFromText(File.ReadAllText(filePath));

        public static Matrix LoadMatrixFromText(string text)
        {
            string[] rowreprs = text.Split('\n');
            var rows = new double[rowreprs.Length][];
            for (int i = 0; i < rowreprs.Length; i++)
                rows[i] = Parsing.ParseDoubleArray(rowreprs[i]);

            var elements = new double[rows.Length, rows[0].Length];
            for (int i = 0; i < rows.Length; i++)
                for (int j = 0; j < rows[0].Length; j++)
                    elements[i, j] = rows[i][j];

            return Matrix.Of(elements, true);
        }

        public static void SaveMatrixToFile(Matrix m, string filePath)
        {
            var sb = new StringBuilder(128);
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColCount; j++)
                    sb.Append(m[i, j]).Append(' ');
                sb.Length -= 1;
                sb.Append('\n');
            }
            sb.Length -= 1;
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
