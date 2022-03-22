using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zadanie_3
{
    public class MatrixTranspositionNumbers : Cipher
    {
        public override string Encode(string text, string keyString)
        {
            var key = PrepareKey(keyString);
            if (key.Length == 0)
                throw new ArgumentException("Key is empty.");
            if (text.Length < key.Length)
                throw new ArgumentException("Plaintext is shorter than key.");
            var rows = text.Length / key.Length + 1; // liczba wszystkich wierszy (pełnych i ewentualnego niepełnego)
            StringBuilder builder = new StringBuilder(text.Length);
            for (int keyIteration = 0; keyIteration < rows; ++keyIteration)
                for (int i = 0; i < key.Length; ++i)
                {
                    int textI = key[i] - 1 + keyIteration * key.Length;
                    if (textI < text.Length)
                        builder.Append(text[textI]);
                }
            return builder.ToString();
        }

        public override string Decode(string text, string keyString)
        {
            var key = PrepareKey(keyString);
            if (key.Length == 0)
                throw new ArgumentException("Key is empty.");
            if (text.Length < key.Length)
                throw new ArgumentException("Ciphertext is shorter than key.");
            var rows = text.Length / key.Length + 1;
            var plainText = new char[text.Length];
            var cipherTextI = 0;
            for (int keyIteration = 0; keyIteration < rows; ++keyIteration)
                for (int i = 0; i < key.Length; ++i)
                {
                    int plainTextI = key[i] - 1 + keyIteration * key.Length;
                    if (plainTextI < text.Length)
                        plainText[plainTextI] = text[cipherTextI++];
                }
            return new string(plainText);
        }

        public int[] PrepareKey(string keyString)
        {
            StringBuilder builder = new StringBuilder();
            // pomijamy wszystkie znaki, które nie są '-' i nie są cyframi
            foreach (var c in keyString)
                if (c == '-' || (c >= '0' && c <= '9'))
                    builder.Append(c);
            var split = builder.ToString().Split('-');
            var columnIndexes = new LinkedList<int>(); // kolumny indeksowane od 1 a nie od 0
            foreach (var s in split)
                if (int.TryParse(s, out int index))
                    columnIndexes.AddLast(index);
            ValidateKey(columnIndexes);
            return columnIndexes.ToArray();
        }

        private void ValidateKey(LinkedList<int> columnIndexes)
        {
            var set = new HashSet<int>();
            foreach (var index in columnIndexes)
            {
                if (set.Contains(index))
                    throw new ArgumentException("Key contains duplicated column indexes.");
                else
                    set.Add(index);
            }
            for (int i = 1; i <= columnIndexes.Count; ++i)
                if (!set.Contains(i))
                    throw new ArgumentException($"Key does not contain all column indexes from 1 to {columnIndexes.Count}.");
        }
    }
}
