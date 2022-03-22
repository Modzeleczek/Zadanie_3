using System;

namespace Zadanie_3
{
    public class ContinuousArray<TValue>
    {
        public int MinKey { get; }
        public int MaxKey { get; }
        public int Length { get; }
        private TValue[] Table;

        public ContinuousArray(int minKey, int maxKey)
        {
            if (maxKey < minKey)
                throw new ArgumentException("minKey must be less or equal to maxKey");
            MinKey = minKey;
            MaxKey = maxKey;
            Length = maxKey - minKey + 1;
            Table = new TValue[Length];
        }

        public TValue this[int key]
        {
            get { return Table[key - MinKey]; }
            set { Table[key - MinKey] = value; }
        }
    }
}
