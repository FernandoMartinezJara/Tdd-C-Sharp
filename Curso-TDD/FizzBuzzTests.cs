using System;
using NUnit.Framework;

namespace Curso_TDD
{
/*
 * If divisible by 3       -> return "Fizz"
 * If divisible by 5       -> return "Buzz"
 * If divisible by 3 and 5 -> return "FizzBuzz"
 * Otherwise               -> return "" 
 */

    [TestFixture]
    public class FizzBuzzTests
    {
        [TestCase("Fizz", 3)]
        [TestCase("Buzz", 5)]
        [TestCase("", 7)]
        public void TestFizzBuzz(string expected, int number) {

            Assert.AreEqual(expected, FizzBuzz(number));
        }

        private string FizzBuzz(int number)
        {
            if (number % 3 == 0) return "Fizz";
            if (number % 5 == 0) return "Buzz";

            return "";
        }
    }
}
