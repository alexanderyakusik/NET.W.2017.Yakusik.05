using NUnit.Framework;
using System;
using System.Collections;

namespace NumericUtils.UnitTests
{
    [TestFixture]
    class PolynomialTests
    {
        [Test]
        public void Ctor_EmptyArrayPassed_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(
                () => new Polynomial(Array.Empty<double>()));
        }

        [Test]
        public void Ctor_NullArrayPassed_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Polynomial(null));
        }

        [Test, TestCaseSource(typeof(PolynomialTestsData), "ToStringTestCases")]
        public string ToString_CorrectCoefficients_WorksCorrectly(double[] coefficients)
        {
            var polynomial = new Polynomial(coefficients);

            return polynomial.ToString();
        }

        [Test, TestCaseSource(typeof(PolynomialTestsData), "EqualsTestCases")]
        public bool Equals_CorrectCoefficients_WorksCorrectly(double[] initialCoefficients, double[] comparedCoefficients)
        {
            var firstPolynomial = new Polynomial(initialCoefficients);
            var secondPolynomial = new Polynomial(comparedCoefficients);

            return firstPolynomial.Equals(secondPolynomial);
        }

        public void Equals_EqualsToNull_ReturnsFalse()
        {
            var polynomial = new Polynomial(-0.25);

            Assert.IsFalse(polynomial.Equals(null));
        }

        [Test, TestCaseSource(typeof(PolynomialTestsData), "AddTestCases")]
        public Polynomial Add_CorrectCoefficients_WorksCorrectly(double[] firstCoefficients, double[] secondCoefficients)
        {
            var first = new Polynomial(firstCoefficients);
            var second = new Polynomial(secondCoefficients);

            return first + second;
        }

        [Test, TestCaseSource(typeof(PolynomialTestsData), "SubtractTestCases")]
        public Polynomial Subtract_CorrectCoefficients_WorksCorrectly(double[] firstCoefficients, double[] secondCoefficients)
        {
            var first = new Polynomial(firstCoefficients);
            var second = new Polynomial(secondCoefficients);

            return first - second;
        }

        [Test, TestCaseSource(typeof(PolynomialTestsData), "MultiplyTestCases")]
        public Polynomial Multiply_CorrectCoefficients_WorksCorrectly(double[] firstCoefficients, double[] secondCoefficients)
        {
            var first = new Polynomial(firstCoefficients);
            var second = new Polynomial(secondCoefficients);

            return first * second;
        }
    }

    class PolynomialTestsData
    {
        public static IEnumerable ToStringTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 0.5, -0.0025, 1.0, 100.125 })
                    .Returns("0,5x^3 - 0,0025x^2 + x + 100,125");
                yield return new TestCaseData(new double[] { 5.25, 0, 4.3125, 0, 0 })
                    .Returns("5,25x^4 + 4,3125x^2");
                yield return new TestCaseData(new double[] { 2.0, -4.0, 0, 96.5, 0, -96.5 })
                    .Returns("2x^5 - 4x^4 + 96,5x^2 - 96,5");
            }
        }

        public static IEnumerable EqualsTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 0.5, -0.0025, 1.0, 100.125 },
                                              new double[] { 0.5, -0.0025, 1.0, 100.125 }).Returns(true);
                yield return new TestCaseData(new double[] { 2.0, -4.0, 0, 96.5, 0, -96.5 },
                                              new double[] { 5.25, 0, 4.3125, 0, 0 }).Returns(false);
                yield return new TestCaseData(new double[] { 2.0, double.NaN, 0, double.MinValue, 0, -96.5 },
                                              new double[] { 2.0, double.NaN, 0, double.MinValue, 0, -96.5 }).Returns(true);
            }
        }

        public static IEnumerable AddTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 4, 0, 2, 0 },
                                              new double[] { 0, 1, 1, 0 })
                      .Returns(new Polynomial(new double[] { 4, 1, 3, 0 }));
                yield return new TestCaseData(new double[] { 2.25, -1, 0, 0 },
                                              new double[] { 8, 4 })
                      .Returns(new Polynomial(new double[] { 2.25, -1, 8, 4 }));
                yield return new TestCaseData(new double[] { -6, 0, 0, 5, 1 },
                                              new double[] { 1, -25, 8, 0 })
                      .Returns(new Polynomial(new double[] { -6, 1, -25, 13, 1 }));
            }
        }

        public static IEnumerable SubtractTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 4, 0, 2, 0 },
                                              new double[] { 0, 1, 1, 0 })
                      .Returns(new Polynomial(new double[] { 4, -1, 1, 0 }));
                yield return new TestCaseData(new double[] { 2.25, -1, 0, 0 },
                                              new double[] { 8, 4 })
                      .Returns(new Polynomial(new double[] { 2.25, -1, -8, -4 }));
                yield return new TestCaseData(new double[] { -6, 0, 0, 5, 1 },
                                              new double[] { 1, -25, 8, 0 })
                      .Returns(new Polynomial(new double[] { -6, -1, 25, -3, 1 }));
            }
        }

        public static IEnumerable MultiplyTestCases
        {
            get
            {
                yield return new TestCaseData(new double[] { 4, 0, 2, 0 },
                                              new double[] { 0, 1, 1, 0 })
                      .Returns(new Polynomial(new double[] { 4, 4, 2, 2, 0, 0 }));
                yield return new TestCaseData(new double[] { 2.25, -1, 0, 0 },
                                              new double[] { 8, 4 })
                      .Returns(new Polynomial(new double[] { 18, 1, -4, 0, 0 }));
                yield return new TestCaseData(new double[] { -6, 0, 0, 5, 1 },
                                              new double[] { 1, -25, 8, 0 })
                      .Returns(new Polynomial(new double[] { -6, 150, -48, 5, -124, 15, 8, 0 }));
            }
        }
    }
}
