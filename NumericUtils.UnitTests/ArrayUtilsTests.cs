using NUnit.Framework;
using System;
using System.Collections;

namespace NumericUtils.UnitTests
{
    [TestFixture]
    class ArrayUtilsTests
    {
        [Test, TestCaseSource(typeof(ArrayUtilsTestsData), "SortJaggedTestCases")]
        public int[][] SortJagged_CorrectArrayPassed_SortsCorrectly(int[][] array, SortOption sortOption)
        {
            ArrayUtils.SortJagged(array, sortOption);

            return array;
        }

        [TestCase(null, SortOption.RowElementsSum)]
        public void SortJagged_NullReferencePassed_ArgumentNullExceptionThrown(int[][] array, SortOption sortOption)
        {
            Assert.Throws<ArgumentNullException>(
                () => ArrayUtils.SortJagged(array, sortOption));
        }

        [Test, TestCaseSource(typeof(ArrayUtilsTestsData), "SortJaggedByDescendingTestCases")]
        public int[][] SortJaggedByDescending_CorrectArrayPassed_SortsCorrectly(int[][] array, SortOption sortOption)
        {
            ArrayUtils.SortJaggedByDescending(array, sortOption);

            return array;
        }

        [TestCase(null, SortOption.RowElementsSum)]
        public void SortJaggedByDescending_NullReferencePassed_ArgumentNullExceptionThrown(int[][] array, SortOption sortOption)
        {
            Assert.Throws<ArgumentNullException>(
                () => ArrayUtils.SortJaggedByDescending(array, sortOption));
        }
    }

    class ArrayUtilsTestsData
    {
        public static IEnumerable SortJaggedTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new int[][] 
                    {
                        new int[] { 5, 2, 9 },
                        new int[] { -6 },
                        new int[] { 0, 1 }
                    }, SortOption.RowElementsSum).Returns(
                    new int[][] {
                        new int[] { -6 },
                        new int[] { 0, 1 },
                        new int[] { 5, 2, 9 }
                    });

                yield return new TestCaseData(
                    new int[][]
                    {
                        new int[] { 5, 9 },
                        new int[] { -6, 14 },
                        new int[] { 0, 1, -4, 7 }
                    }, SortOption.RowMaximumElements).Returns(
                    new int[][] {
                        new int[] { 0, 1, -4, 7 },
                        new int[] { 5, 9 },
                        new int[] { -6, 14 }
                    });

                yield return new TestCaseData(
                    new int[][]
                    {
                        new int[] { 5, -3, 9 },
                        new int[] { -6 },
                        new int[] { 0, 1 }
                    }, SortOption.RowMinimumElements).Returns(
                    new int[][] {
                        new int[] { -6 },
                        new int[] { 5, -3, 9},
                        new int[] { 0, 1 }
                    });
            }
        }

        public static IEnumerable SortJaggedByDescendingTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new int[][]
                    {
                        new int[] { 8, 2, 5, -1, -5 },
                        new int[] { -4, -7, 2, 5 },
                        new int[] { 1, 5 }
                    }, SortOption.RowElementsSum).Returns(
                    new int[][] {
                        new int[] { 8, 2, 5, -1, -5 },
                        new int[] { 1, 5 },
                        new int[] { -4, -7, 2, 5 }
                    });

                yield return new TestCaseData(
                    new int[][]
                    {
                        new int[] { 6, 3, 3, -8 },
                        new int[] { 5, 7, 10, 56 },
                        new int[] { -4, -6, 9, 20 }
                    }, SortOption.RowMaximumElements).Returns(
                    new int[][] {
                        new int[] { 5, 7, 10, 56 },
                        new int[] { -4, -6, 9, 20 },
                        new int[] { 6, 3, 3, -8 }
                    });

                yield return new TestCaseData(
                    new int[][]
                    {
                        new int[] { 4, 62, -3, 7 },
                        new int[] { 9, -7, 3, 12, 69 },
                        new int[] { -4, -6, 9, 10 }
                    }, SortOption.RowMinimumElements).Returns(
                    new int[][] {
                        new int[] { 4, 62, -3, 7 },
                        new int[] { -4, -6, 9, 10 },
                        new int[] { 9, -7, 3, 12, 69 }
                    });
            }
        }
    }
}
