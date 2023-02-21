using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StructBenchmarking
{
    public interface IStructAndArrayBenchmarks
    {
        ITask RunStructBenchmark(int repetitionsCount);
        ITask RunClassBenchmark(int repetitionsCount);
    }

    public class StructClassArrayTask : IStructAndArrayBenchmarks
    {
        public ITask RunStructBenchmark(int repetitionsCount)
        {
            return new StructArrayCreationTask(repetitionsCount);
        }

        public ITask RunClassBenchmark(int repetitionsCount)
        {
            return new ClassArrayCreationTask(repetitionsCount);
        }
    }

    public class StructClassMethodCallTask : IStructAndArrayBenchmarks
    {
        public ITask RunStructBenchmark(int repetitionsCount)
        {
            return new MethodCallWithStructArgumentTask(repetitionsCount);
        }

        public ITask RunClassBenchmark(int repetitionsCount)
        {
            return new MethodCallWithClassArgumentTask(repetitionsCount);
        }
    }
    
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount)
        {
            return RunExperiment(new StructClassArrayTask(), benchmark, repetitionsCount);
        }
        
        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount)
        {
            return RunExperiment(new StructClassMethodCallTask(), benchmark, repetitionsCount);
        }

        private static ChartData RunExperiment(IStructAndArrayBenchmarks factory,
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            
            foreach (var objectSize in Constants.FieldCounts)
            {
                var classTestResult = benchmark
                    .MeasureDurationInMs(factory.RunClassBenchmark(objectSize), repetitionsCount);
                classesTimes.Add(new ExperimentResult(objectSize, classTestResult));
                var structTestResult = benchmark
                    .MeasureDurationInMs(factory.RunStructBenchmark(objectSize), repetitionsCount);
                structuresTimes.Add(new ExperimentResult(objectSize, structTestResult));
            }

            return new ChartData
            {
                Title = "Experiment is successful",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}