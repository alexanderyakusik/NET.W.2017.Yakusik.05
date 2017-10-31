using System;
using System.Collections.Generic;

namespace NumericUtils
{
    public static class ArrayUtils
    {
        /// <summary>
        /// Sorts rows of an <paramref name="array"/> using <paramref name="comparer"/>.
        /// </summary>
        /// <param name="array">Array to be sorted</param>
        /// <param name="comparer">Object used to compare two arrays</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        public static void SortJagged(int[][] array, IComparer<int[]> comparer)
        {
            ValidateNullArrray(array);

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    int comparison = comparer.Compare(array[j], array[j + 1]);

                    if (comparison > 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        #region Private methods

        private static void Swap<T>(ref T firstItem, ref T secondItem)
        {
            T temp = firstItem;
            firstItem = secondItem;
            secondItem = temp;
        }

        private static void ValidateNullArrray(int[][] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} cannot be null.");
            }
        }

        #endregion Private methods
    }
}
