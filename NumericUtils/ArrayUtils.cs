using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericUtils
{
    /// <summary>
    /// An enum used to specify by which option a jagged array will be sorted
    /// </summary>
    public enum SortOption
    {
        /// <summary>
        /// Sort rows of an array by the sum of all elements in the row
        /// </summary>
        RowElementsSum,
        /// <summary>
        /// Sort rows of an array by the maximum element in the row
        /// </summary>
        RowMaximumElements,
        /// <summary>
        /// Sort rows of an array by the minimum element in the row
        /// </summary>
        RowMinimumElements
    }

    public static class ArrayUtils
    {
        /// <summary>
        /// Sorts rows of an <paramref name="array"/> by ascending order using <paramref name="sortOption"/>
        /// </summary>
        /// <param name="array">Array to be sorted</param>
        /// <param name="sortOption">Option of sorting</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        public static void SortJagged(int[][] array, SortOption sortOption)
        {
            ValidateNullArrray(array);

            Func<int[], int> rowFeatureCalculator = sortOptionsMapper[sortOption];
            BubbleSortJagged(array, rowFeatureCalculator, 
                Comparer<int>.Create((num1, num2) => num1 < num2 ? 1 : num1 == num2 ? 0 : -1));
        }

        /// <summary>
        /// Sorts rows of an <paramref name="array"/> by descending order using <paramref name="sortOption"/>
        /// </summary>
        /// <param name="array">Array to be sorted</param>
        /// <param name="sortOption">Option of sorting</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        public static void SortJaggedByDescending(int[][] array, SortOption sortOption)
        {
            ValidateNullArrray(array);

            Func<int[], int> rowFeatureCalculator = sortOptionsMapper[sortOption];
            BubbleSortJagged(array, rowFeatureCalculator,
                Comparer<int>.Create((num1, num2) => num1 < num2 ? -1 : num1 == num2 ? 0 : 1));
        }

        #region Private methods

        static ArrayUtils()
        {
            sortOptionsMapper = new Dictionary<SortOption, Func<int[], int>>
            {
                [SortOption.RowElementsSum] = (int[] row) => row.Sum(),
                [SortOption.RowMaximumElements] = (int[] row) => row.Max(),
                [SortOption.RowMinimumElements] = (int[] row) => row.Min()
            };
        }

        private static Dictionary<SortOption, Func<int[], int>> sortOptionsMapper;

        private static void BubbleSortJagged(int[][] array, Func<int[], int> rowFeatureCalculator, IComparer<int> comparer)
        {
            int[] rowFeatureArray = GetRowFeatureArray(array, rowFeatureCalculator);

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (comparer.Compare(rowFeatureArray[j], rowFeatureArray[j + 1]) < 0)
                    {
                        Swap(ref rowFeatureArray[j], ref rowFeatureArray[j + 1]);
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        private static int[] GetRowFeatureArray(int[][] array, Func<int[], int> rowFeatureCalculator)
        {
            int[] resultArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                resultArray[i] = rowFeatureCalculator(array[i]);
            }

            return resultArray;
        }

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
