using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rail_fence_cipher
{
    internal class RailFenceCipher
    {
        private static char[][] BuildMatrix(int rows, int cols)
        {
            char[][] matrix = new char[rows][];
            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = new char[cols];
            }
            return matrix;
        }
        private static string BuildString(char[][] matrix)
        {
            string result = string.Empty;
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] != '\0')
                    {
                        result += matrix[row][col];
                    }
                }
            }
            return result;
        }
        private static char[][] TransposeMatrix(char[][] matrix)
        {
            char[][] result = BuildMatrix(matrix[0].Length, matrix.Length);
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    result[col][row] = matrix[row][col];
                }
            }
            return result;

        }
        public string Encrypt(string decryptedText, int key)
        {
            char[][] matrix = BuildMatrix(key, decryptedText.Length);
            int rowIncrement = 1;
            for (int row = 0, col = 0; col < matrix[row].Length; col++)
            {
                if (row + rowIncrement == matrix.Length || row + rowIncrement == -1)
                {
                    rowIncrement *= -1;
                }
                matrix[row][col] = decryptedText[col];
                row += rowIncrement;
            }
            return (BuildString(matrix));
        }
        public string Decrypt(string encryptedText, int key)
        {
            string result = string.Empty;
            char[][] matrix = BuildMatrix(key, encryptedText.Length);

            int rowIncrement = 1;
            int textId = 0;

            for (int selectedRow = 0; selectedRow < matrix.Length; selectedRow++)
            {
                for (int row = 0, col = 0; col < matrix[row].Length; col++)
                {
                    if (row == selectedRow)
                    {
                        matrix[row][col] = encryptedText[textId++];
                    }
                    row += rowIncrement;
                }
            }
            matrix = TransposeMatrix(matrix);
            return (BuildString(matrix));
        }
    }
}

