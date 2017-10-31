using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NumericUtils.UnitTests
{
    [TestFixture]
    class ArrayUtilsTests
    {
        [Test, TestCaseSource(typeof(ArrayUtilsTestsData), "SortJaggedTestCases")]
        public int[][] SortJagged_CorrectArrayPassed_SortsCorrectly(int[][] array, IComparer<int[]> comparer)
        {
            ArrayUtils.SortJagged(array, comparer);

            return array;
        }

        [TestCase(null, null)]
        public void SortJagged_NullReferencePassed_ArgumentNullExceptionThrown(int[][] array, IComparer<int[]> comparer)
        {
            Assert.Throws<ArgumentNullException>(
                () => ArrayUtils.SortJagged(array, comparer));
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
                    }, Comparer<int[]>.Create((lhs, rhs) => lhs.Sum() < rhs.Sum() ? -1 : lhs.Sum() == rhs.Sum() ? 0 : 1))
                    .Returns(
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
                    }, Comparer<int[]>.Create((lhs, rhs) => lhs.Max() < rhs.Max() ? -1 : (lhs.Max() == rhs.Max()) ? 0 : 1))
                    .Returns(
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
                    }, Comparer<int[]>.Create((lhs, rhs) => lhs.Min() < rhs.Min() ? -1 : (lhs.Min() == rhs.Min()) ? 0 : 1))
                    .Returns(
                    new int[][] {
                        new int[] { -6 },
                        new int[] { 5, -3, 9},
                        new int[] { 0, 1 }
                    });

                yield return new TestCaseData(
                    new int[][]
                    {
                        new int[] { 8, 2, 5, -1, -5 },
                        new int[] { -4, -7, 2, 5 },
                        new int[] { 1, 5 }
                    }, Comparer<int[]>.Create((lhs, rhs) => lhs.Sum() < rhs.Sum() ? 1 : (lhs.Sum() == rhs.Sum()) ? 0 : -1))
                    .Returns(
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
                    }, Comparer<int[]>.Create((lhs, rhs) => lhs.Max() < rhs.Max() ? 1 : (lhs.Max() == rhs.Max()) ? 0 : -1))
                    .Returns(
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
                    }, Comparer<int[]>.Create((lhs, rhs) => lhs.Min() < rhs.Min() ? 1 : (lhs.Min() == rhs.Min()) ? 0 : -1))
                    .Returns(
                    new int[][] {
                        new int[] { 4, 62, -3, 7 },
                        new int[] { -4, -6, 9, 10 },
                        new int[] { 9, -7, 3, 12, 69 }
                    });
            }
        }
    }
}
