using System;
using System.Text;

namespace Zadanie_3
{
    public class RailFence : Cipher
    {
        public override string Encode(string text, string keyString)
        {
            if (!int.TryParse(keyString, out int height))
                throw new ArgumentException("Fence height has invalid format.");
            if (height < 2)
                throw new ArgumentException("Fence height is less than 2.");
            StringBuilder builder = new StringBuilder(text.Length);
            AppendFirstRow(builder, height, text);
            AppendMiddleRows(builder, height, text);
            AppendLastRow(builder, height, text);
            return builder.ToString();
        }

        private void AppendFirstRow(StringBuilder builder, int height, string text)
        {
            int row = 0;
            int jumpAbove = (height - row) * 2 - 1 - 1;
            int i = row;
            while (i < text.Length)
            {
                builder.Append(text[i]);
                i += jumpAbove;
            }
        }

        private void AppendMiddleRows(StringBuilder builder, int height, string text)
        {
            for (int row = 1; row < height - 1; ++row)
            {
                // o ile znaków przeskakujemy w aktualnym wierszu
                int jumpAbove = (height - row) * 2 - 1 - 1;
                int jumpBelow = row * 2;
                int iteration = 0;
                int i = row;
                while (i < text.Length)
                {
                    builder.Append(text[i]);
                    if ((iteration++) % 2 == 0)
                        i += jumpAbove;
                    else
                        i += jumpBelow;
                }
            }
        }

        private void AppendLastRow(StringBuilder builder, int height, string text)
        {
            int row = height - 1;
            int jumpBelow = row * 2;
            int i = row;
            while (i < text.Length)
            {
                builder.Append(text[i]);
                i += jumpBelow;
            }
        }

        public override string Decode(string text, string keyString)
        {
            if (!int.TryParse(keyString, out int height))
                throw new ArgumentException("Fence height has invalid format.");
            if (height < 2)
                throw new ArgumentException("Fence height is less than 2.");
            var plainText = new char[text.Length];
            int cipherTextI = 0;
            FillFirstRow(plainText, height, text, ref cipherTextI);
            FillMiddleRows(plainText, height, text, ref cipherTextI);
            FillLastRow(plainText, height, text, ref cipherTextI);
            return new string(plainText);
        }

        private void FillFirstRow(char[] plainText, int height, string cipherText, ref int cipherTextI)
        {
            int row = 0;
            int jumpAbove = (height - row) * 2 - 1 - 1;
            int i = row;
            while (i < plainText.Length)
            {
                plainText[i] = cipherText[cipherTextI++];
                i += jumpAbove;
            }
        }

        private void FillMiddleRows(char[] plainText, int height, string cipherText, ref int cipherTextI)
        {
            for (int row = 1; row < height - 1; ++row)
            {
                // o ile znaków przeskakujemy w aktualnym wierszu
                int jumpAbove = (height - row) * 2 - 1 - 1;
                int jumpBelow = row * 2;
                int iteration = 0;
                int i = row;
                while (i < plainText.Length)
                {
                    plainText[i] = cipherText[cipherTextI++];
                    if ((iteration++) % 2 == 0)
                        i += jumpAbove;
                    else
                        i += jumpBelow;
                }
            }
        }

        private void FillLastRow(char[] plainText, int height, string cipherText, ref int cipherTextI)
        {
            int row = height - 1;
            int jumpBelow = row * 2;
            int i = row;
            while (i < plainText.Length)
            {
                plainText[i] = cipherText[cipherTextI++];
                i += jumpBelow;
            }
        }
    }
}
