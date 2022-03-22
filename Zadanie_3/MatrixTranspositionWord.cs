using System;
using System.Text;

namespace Zadanie_3
{
    public class MatrixTranspositionWord : Cipher
    {
        public override string Encode(string text, string keyString)
        {
            var key = PrepareKey(keyString);
            if (key.Length == 0)
                throw new ArgumentException("Key is empty.");
            if (text.Length < key.Length)
                throw new ArgumentException("Plaintext is shorter than key.");
            var map = CalculateMap(key);
            StringBuilder builder = new StringBuilder(text.Length);
            for (int i = map.MinKey; i <= map.MaxKey; ++i)
                for (int textIndex = map[i]; textIndex < text.Length; textIndex += key.Length)
                    builder.Append(text[textIndex]);
            return builder.ToString();
        }

        // mapa w i-tej komórce (i = 1, 2...) ma numer kolumny (liczony od 0), w której i znajduje się w kluczu
        private ContinuousArray<int> CalculateMap(int[] key)
        {
            var map = new ContinuousArray<int>(1, key.Length);
            for (int i = 0; i < key.Length; ++i)
                map[key[i]] = i;
            return map;
        }

        public override string Decode(string text, string keyString)
        {
            var key = PrepareKey(keyString);
            if (key.Length == 0)
                throw new ArgumentException("Key is empty.");
            if (text.Length < key.Length)
                throw new ArgumentException("Ciphertext is shorter than key.");
            var rows = text.Length / key.Length;
            var remainder = text.Length % key.Length;
            var jaggedMatrix = CreateJaggedMatrix(key, rows, remainder);
            var map = CalculateMap(key);
            int textI = 0;
            for (int i = map.MinKey; i <= map.MaxKey; ++i)
            {
                int columnIndex = map[i];
                var column = jaggedMatrix[columnIndex];
                for (int y = 0; y < column.Length; ++y)
                    column[y] = text[textI++];
            }
            StringBuilder builder = new StringBuilder(text.Length);
            for (int y = 0; y < rows; ++y)
                for (int x = 0; x < key.Length; ++x) // pętla dla pełnych wierszy
                    builder.Append(jaggedMatrix[x][y]);
            for (int x = 0; x < remainder; ++x) // pętla dla ostatniego, niepełnego wiersza (o ile istnieje)
                builder.Append(jaggedMatrix[x][rows]);
            return builder.ToString();
        }

        private char[][] CreateJaggedMatrix(int[] key, int rows, int remainder)
        {
            var jaggedMatrix = new char[key.Length][];
            int x;
            for (x = 0; x < remainder; ++x)
                jaggedMatrix[x] = new char[rows + 1];
            for (; x < key.Length; ++x)
                jaggedMatrix[x] = new char[rows];
            return jaggedMatrix;
        }

        public int[] PrepareKey(string keyString)
        {
            var word = PreprocessKeyString(keyString);
            var map = new ContinuousArray<int>('A', 'Z');
            foreach (var c in word)
                ++map[c];
            int cumulativeSum = 0;
            for (char c = 'A'; c <= 'Z'; ++c)
            {
                cumulativeSum += map[c];
                map[c] = cumulativeSum;
            }
            var columnIndexes = new int[word.Length]; // kolumny indeksowane od 1 a nie od 0
            for (int i = word.Length - 1; i >= 0; --i)
            {
                columnIndexes[i] = map[word[i]];
                --map[word[i]];
            }
            return columnIndexes;
        }

        private string PreprocessKeyString(string keyString)
        {
            StringBuilder builder = new StringBuilder();
            // pomijamy wszystkie znaki, które nie są małymi lub dużymi literami, a małe litery zamieniamy na duże
            foreach (var c in keyString)
            {
                if (c >= 'a' && c <= 'z')
                {
                    int letterIndex = c - 'a';
                    builder.Append((char)('A' + letterIndex));
                }
                else if (c >= 'A' && c <= 'Z')
                    builder.Append(c);
            }
            return builder.ToString();
        }
    }
}
