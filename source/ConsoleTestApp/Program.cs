using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleTestApp
{
    internal class Program
    {
        private const int NUMBER_OF_VALUES = 500000000;
        private const int TEST_REPEAT_TIMES = 5;
        private static IList<byte> m_values;

        private static IEnumerable<byte> CreateRandomBytes(int count)
        {
            var enumerable = Enumerable.Repeat(1, count);
            var rand = new Random(987);
            return enumerable.Select(x => (byte) rand.Next(10));
        }

        private static void ForeachSummation(byte[] values)
        {
            var stopWatch = Stopwatch.StartNew();
            long sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }

            Console.WriteLine(
                $"Foreach sum of {NUMBER_OF_VALUES} {values.GetType()} " +
                $"took {stopWatch.Elapsed}, Sum={sum}.");
        }

        private static void ForeachSummation(IEnumerable<byte> values)
        {
            var stopWatch = Stopwatch.StartNew();
            long sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }

            Console.WriteLine(
                $"Foreach sum of {NUMBER_OF_VALUES} {values.GetType()} " +
                $"took {stopWatch.Elapsed}, Sum={sum}.");
        }

        private static void LinqAsParallelSum(byte[] values)
        {
            var stopWatch = Stopwatch.StartNew();
            var sum = values.AsParallel().Sum(x => (long) x);
            Console.WriteLine(
                $"LINQ AsParallel Sum of {NUMBER_OF_VALUES} {values.GetType()} " +
                $"took {stopWatch.Elapsed}, Sum={sum}.");
        }

        private static void LinqAsParallelSum(IEnumerable<byte> values)
        {
            var stopWatch = Stopwatch.StartNew();
            var sum = values.AsParallel().Sum(x => (long) x);
            Console.WriteLine(
                $"LINQ AsParallel Sum of {NUMBER_OF_VALUES} {values.GetType()} " +
                $"took {stopWatch.Elapsed}, Sum={sum}.");
        }

        private static void LinqSum(byte[] values)
        {
            var stopWatch = Stopwatch.StartNew();
            var sum = values.Sum(x => (long) x);
            Console.WriteLine(
                $"LINQ Sum of {NUMBER_OF_VALUES} {values.GetType()} " +
                $"took {stopWatch.Elapsed}, Sum={sum}.");
        }

        private static void LinqSum(IEnumerable<byte> values)
        {
            var stopWatch = Stopwatch.StartNew();
            var sum = values.Sum(x => (long) x);
            Console.WriteLine(
                $"LINQ Sum of {NUMBER_OF_VALUES} {values.GetType()} " +
                $"took {stopWatch.Elapsed}, Sum={sum}.");
        }

        private static void Main()
        {
            TestSummationMethods();

            Console.ReadLine();
        }

        private static void TestSummationMethods()
        {
            Console.WriteLine($"Creating {NUMBER_OF_VALUES} random bytes...");
            var stopWatch = Stopwatch.StartNew();
            m_values = CreateRandomBytes(NUMBER_OF_VALUES).ToList();
            Console.WriteLine($"Creating {NUMBER_OF_VALUES} random bytes took {stopWatch.Elapsed}.");
            Console.WriteLine("Running various tests...");

            Enumerable.Range(1, TEST_REPEAT_TIMES).ToList().ForEach(x => ForeachSummation(m_values));
            Enumerable.Range(1, TEST_REPEAT_TIMES).ToList().ForEach(x => ForeachSummation(m_values.ToArray()));

            Enumerable.Range(1, TEST_REPEAT_TIMES).ToList().ForEach(x => LinqSum(m_values));
            Enumerable.Range(1, TEST_REPEAT_TIMES).ToList().ForEach(x => LinqSum(m_values.ToArray()));

            Enumerable.Range(1, TEST_REPEAT_TIMES).ToList().ForEach(x => LinqAsParallelSum(m_values));
            Enumerable.Range(1, TEST_REPEAT_TIMES).ToList().ForEach(x => LinqAsParallelSum(m_values.ToArray()));
        }
    }
}