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
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is null.</exception>
        public static void SortJagged(int[][] array, IComparer<int[]> comparer)
        {
            comparer = comparer ?? throw new ArgumentNullException("Value cannot be null");
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

        /// <summary>
        /// Sorts rows of an <paramref name="array"/> using <paramref name="comparisonFunction"/>.
        /// </summary>
        /// <param name="array">Array to be sorted</param>
        /// <param name="comparer">Function used to compare two arrays</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="comparisonFunction"/> is null.</exception>
        public static void SortJagged(int[][] array, Func<int[], int[], int> comparisonFunction)
        {
            comparisonFunction = comparisonFunction ?? throw new ArgumentNullException("Value cannot be null");

            SortJagged(array, new Comparer<int[]>(comparisonFunction));
        }

        /// <summary>
        /// Sorts rows of an <paramref name="array"/> using <paramref name="comparer"/>.
        /// </summary>
        /// <param name="array">Array to be sorted</param>
        /// <param name="comparer">Object used to compare two arrays</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is null.</exception>
        public static void SortJaggedAlternative(int[][] array, IComparer<int[]> comparer)
        {
            comparer = comparer ?? throw new ArgumentNullException("Value cannot be null.");

            SortJaggedAlternative(array, comparer.Compare);
        }

        /// <summary>
        /// Sorts rows of an <paramref name="array"/> using <paramref name="comparisonFunction"/>.
        /// </summary>
        /// <param name="array">Array to be sorted</param>
        /// <param name="comparer">Function used to compare two arrays</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="comparisonFunction"/> is null.</exception>
        public static void SortJaggedAlternative(int[][] array, Func<int[], int[], int> comparisonFunction)
        {
            comparisonFunction = comparisonFunction ?? throw new ArgumentNullException("Value cannot be null");
            ValidateNullArrray(array);

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    int comparison = comparisonFunction(array[j], array[j + 1]);

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
