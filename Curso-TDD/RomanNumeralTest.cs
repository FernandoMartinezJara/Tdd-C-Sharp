using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Curso_TDD
{
    [TestFixture]
    public class RomanNumeralTest
    {
        [TestCase(1, "I")]
        [TestCase(2, "II")]
        [TestCase(4, "IV")]
        [TestCase(5, "V")]
        [TestCase(10, "X")]
        [TestCase(15, "XV")]
        public void Parse(int expected, string romanNumber)
        {
            Assert.AreEqual(expected, Roman.Parse(romanNumber));
        }
    }

    public class Roman
    {

        private static readonly Dictionary<char, int> RomanNumbers = new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };

        public static int Parse(string romanNumber)
        {
            int result = 0;

            for (int i = 0; i < romanNumber.Length; i++)
            {
                if (i + 1 < romanNumber.Length && IsSubstractive(romanNumber, i))
                {
                    result -= RomanNumbers[romanNumber[i]];
                }
                else
                {
                    result += RomanNumbers[romanNumber[i]];
                }
            }

            return result;
        }

        private static bool IsSubstractive(string romanNumber, int i)
        {
            return RomanNumbers[romanNumber[i]] < RomanNumbers[romanNumber[i + 1]];
        }
    }
}
