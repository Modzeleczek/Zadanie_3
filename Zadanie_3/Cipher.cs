namespace Zadanie_3
{
    public abstract class Cipher
    {
        public abstract string Encode(string text, string keyString);
        public abstract string Decode(string text, string keyString);
    }
}
