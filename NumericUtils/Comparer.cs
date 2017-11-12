using System;
using System.Collections.Generic;

namespace NumericUtils
{
    internal class Comparer<T> : IComparer<T>
    {
        private Func<T, T, int> func;

        public Comparer(Func<T, T, int> comparerFunction)
        {
            func = comparerFunction;
        }

        public int Compare(T x, T y)
        {
            return func(x, y);
        }
    }
}
