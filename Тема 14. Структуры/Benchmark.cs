﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run();
            
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
            // и как-то повлияет на них.
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            stopwatch.Stop();
            var ts = stopwatch.Elapsed.TotalMilliseconds;
            return ts / repetitionCount;
        }
    }

    public class BuilderTest : ITask
    {
        public void Run()
        {
            var str = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                str.Append("a");
            }
            var ans = str.ToString();
        }
    }
    
    public class StringTest : ITask
    {
        public void Run()
        {
            var ans = new string('a', 10000);
        }
    }
    

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();

            var firstTest = benchmark.MeasureDurationInMs(new StringTest(), 10000);
            var secondTest = benchmark.MeasureDurationInMs(new BuilderTest(), 10000);
            Assert.Less(firstTest, secondTest);
        }
    }
}