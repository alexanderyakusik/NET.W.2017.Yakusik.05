using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NumericUtils
{
    public class Polynomial
    {
        /// <summary>
        /// Constructor for creating polynomial using <paramref name="coefficients"/>.
        /// </summary>
        /// <param name="coefficients">Coefficients from leading to lower.</param>
        public Polynomial(params double[] coefficients)
        {
            ValidateNullOrEmptyArray(coefficients);

            double[] correctCoefficients = GetCoefficientsWithRemovedZeroes(coefficients);

            this.coefficients = correctCoefficients;
        }

        /// <summary>
        /// Returns string representation of the polynomial.
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            AppendFirstElement(stringBuilder);
            AppendAllElementsExceptFirst(stringBuilder);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Checks if the polynomial coefficients are equal to the coefficients of <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">Object to compare coefficients to</param>
        /// <returns>True if coefficients are equal. Otherwise, returns false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals(obj as Polynomial);
        }

        /// <summary>
        /// Checks if the polynomial coefficients are equal to the coefficients of <paramref name="polynomial"/>.
        /// </summary>
        /// <param name="polynomial">Object to compare coefficients to</param>
        /// <returns>True if coefficients are equal. Otherwise, returns false.</returns>
        public bool Equals(Polynomial polynomial)
        {
            if (ReferenceEquals(polynomial, null))
            {
                return false;
            }

            if (ReferenceEquals(this, polynomial))
            {
                return true;
            }

            if (GetType() != polynomial.GetType())
            {
                return false;
            }

            if (coefficients.Length != polynomial.coefficients.Length)
            {
                return false;
            }

            for (int i = 0; i < coefficients.Length; i++)
            {
                if (coefficients[i] - polynomial.coefficients[i] >= epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns hash of the polynomial based on coefficients array hash.
        /// </summary>
        /// <returns>Polynomial's hash code.</returns>
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)coefficients).GetHashCode(EqualityComparer<double>.Default);
        }

        /// <summary>
        /// Performs addition operation of two polynomials.
        /// </summary>
        /// <param name="first">Augend</param>
        /// <param name="second">Addend</param>
        /// <returns>Sum</returns>
        /// <exception cref="ArgumentNullException"><paramref name="first"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="second"/> is null.</exception>
        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            ValidateNullPolynomial(first);
            ValidateNullPolynomial(second);

            return new Polynomial(CalculateAdditiveOperation(first, second, (num1, num2) => num1 + num2));
        }

        /// <summary>
        /// Performs addition operation of two polynomials.
        /// </summary>
        /// <param name="first">Augend</param>
        /// <param name="second">Addend</param>
        /// <returns>Sum</returns>
        /// /// <exception cref="ArgumentNullException"><paramref name="first"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="second"/> is null.</exception>
        public static Polynomial Add(Polynomial first, Polynomial second)
        {
            return first + second;
        }

        /// <summary>
        /// Performs subtraction operation of two polynomials.
        /// </summary>
        /// <param name="minuend">Minuend</param>
        /// <param name="subtrahend">Subtrahend</param>
        /// <returns>Difference</returns>
        /// /// <exception cref="ArgumentNullException"><paramref name="first"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="second"/> is null.</exception>
        public static Polynomial operator -(Polynomial minuend, Polynomial subtrahend)
        {
            ValidateNullPolynomial(minuend);
            ValidateNullPolynomial(subtrahend);

            return new Polynomial(CalculateAdditiveOperation(minuend, subtrahend, (num1, num2) => num1 - num2));
        }

        /// <summary>
        /// Performs subtraction operation of two polynomials.
        /// </summary>
        /// <param name="minuend">Minuend</param>
        /// <param name="subtrahend">Subtrahend</param>
        /// <returns>Difference</returns>
        /// /// <exception cref="ArgumentNullException"><paramref name="first"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="second"/> is null.</exception>
        public static Polynomial Subtract(Polynomial minuend, Polynomial subtrahend)
        {
            return minuend - subtrahend;
        }

        /// <summary>
        /// Performs multiplication operation of two polynomials.
        /// </summary>
        /// <param name="first">Multiplicand</param>
        /// <param name="second">Multiplier</param>
        /// <returns>Product</returns>
        /// /// <exception cref="ArgumentNullException"><paramref name="first"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="second"/> is null.</exception>
        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            ValidateNullPolynomial(first);
            ValidateNullPolynomial(second);

            double[] resultCoefficients = new double[first.coefficients.Length + second.coefficients.Length - 1];

            for (int i = 0; i < first.coefficients.Length; i++)
            {
                for (int j = 0; j < second.coefficients.Length; j++)
                {
                    resultCoefficients[i + j] += first.coefficients[i] * second.coefficients[j];
                }
            }

            return new Polynomial(resultCoefficients);
        }

        /// <summary>
        /// Performs multiplication operation of two polynomials.
        /// </summary>
        /// <param name="first">Multiplicand</param>
        /// <param name="second">Multiplier</param>
        /// <returns>Product</returns>
        /// /// <exception cref="ArgumentNullException"><paramref name="first"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="second"/> is null.</exception>
        public static Polynomial Multiply(Polynomial first, Polynomial second)
        {
            return first * second;
        }

        /// <summary>
        /// Checks if the polynomial coefficients of <paramref name="first"/> are equal to
        /// the coefficients of <paramref name="second"/>
        /// </summary>
        /// <param name="first">First polynomial to compare</param>
        /// <param name="second">Second polynomial to compare</param>
        /// <returns>True if coefficients are equal. Otherwise, returns false.</returns>
        public static bool operator ==(Polynomial first, Polynomial second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Checks if the polynomial coefficients of <paramref name="first"/> are different from
        /// the coefficients of <paramref name="second"/>
        /// </summary>
        /// <param name="first">First polynomial to compare</param>
        /// <param name="second">Second polynomial to compare</param>
        /// <returns>True if coefficients are different. Otherwise, returns true.</returns>
        public static bool operator !=(Polynomial first, Polynomial second)
        {
            return !(first == second);
        }

        #region Private fields

        private static double epsilon;

        private double[] coefficients;

        #endregion

        #region Private methods

        static Polynomial()
        {
            epsilon = 1E-6;
        }

        private static void ValidateNullPolynomial(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new ArgumentNullException();
            }
        }

        private static void ValidateNullOrEmptyArray(double[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} cannot be null.");
            }

            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} must contain one or more elements.");
            }
        }

        private void AppendFirstElement(StringBuilder stringBuilder)
        {
            if (coefficients[0] == -1)
            {
                stringBuilder.Append('-');
            }
            else if (coefficients[0] != 1)
            {
                stringBuilder.Append(coefficients[0]);
            }
            stringBuilder.Append("x^" + (coefficients.Length - 1));
        }

        private void AppendAllElementsExceptFirst(StringBuilder stringBuilder)
        {
            for (int i = 1; i < coefficients.Length; i++)
            {
                if (coefficients[i] == 0)
                {
                    continue;
                }

                AppendCoefficient(stringBuilder, i);

                if (i < coefficients.Length - 2)
                {
                    stringBuilder.Append("x^" + (coefficients.Length - 1 - i));
                }
                else if (i == coefficients.Length - 2)
                {
                    stringBuilder.Append("x");
                }
            }
        }

        private void AppendCoefficient(StringBuilder stringBuilder, int coefficientIndex)
        {
            if (coefficients[coefficientIndex] < 0)
            {
                stringBuilder.Append(" - ");
                if (coefficients[coefficientIndex] != -1)
                {
                    stringBuilder.Append(-coefficients[coefficientIndex]);
                }
            }
            else
            {
                stringBuilder.Append(" + ");
                if (coefficients[coefficientIndex] != 1)
                {
                    stringBuilder.Append(coefficients[coefficientIndex]);
                }
            }
        }

        private double[] GetCoefficientsWithRemovedZeroes(double[] coefficients)
        {
            int nonZeroItemIndex = Array.FindIndex(coefficients, (coefficient) => coefficient != 0.0);

            double[] newCoefficients;

            if (nonZeroItemIndex == -1)
            {
                newCoefficients = new double[] { 0 };
            }
            else
            {
                newCoefficients = new double[coefficients.Length - nonZeroItemIndex];
                Array.Copy(coefficients, nonZeroItemIndex, newCoefficients, 0, newCoefficients.Length);
            }

            return newCoefficients;
        }

        private static double[] CalculateAdditiveOperation(Polynomial first, Polynomial second,
                                                           Func<double, double, double> operation)
        {
            double[] resultCoefficients = new double[Math.Max(first.coefficients.Length, second.coefficients.Length)];

            int minLength = Math.Min(first.coefficients.Length, second.coefficients.Length);

            for (int i = 0; i < first.coefficients.Length; i++)
            {
                resultCoefficients[i] = first.coefficients[i];
            }

            for (int i = resultCoefficients.Length - minLength; i < resultCoefficients.Length; i++)
            {
                resultCoefficients[i] = operation(resultCoefficients[i], 
                                                  second.coefficients[i - (resultCoefficients.Length - minLength)]);
            }

            return resultCoefficients;
        }

        #endregion
    }
}
