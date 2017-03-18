using System;
using System.IO;
using Xunit;

using static IharBury.Algorithms.Tests.GoogleCodeJam.Year2008.Qualification.ProblemB.Solution;

namespace IharBury.Algorithms.Tests.GoogleCodeJam.Year2008.Qualification.ProblemB
{
    public sealed class Tests
    {
        [Fact]
        public void Sample1IsCorrect()
        {
            (var trainCountAtA, var trainCountAtB) = Solve(
                TimeSpan.FromMinutes(5),
                new[]
                {
                    (new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0)),
                    (new TimeSpan(10, 0, 0), new TimeSpan(13, 0, 0)),
                    (new TimeSpan(11, 0, 0), new TimeSpan(12, 30, 0))
                },
                new[]
                {
                    (new TimeSpan(12, 2, 0), new TimeSpan(15, 0, 0)),
                    (new TimeSpan(9, 0, 0), new TimeSpan(10, 30, 0))
                });

            Assert.Equal(2, trainCountAtA);
            Assert.Equal(2, trainCountAtB);
        }

        public void Sample2IsCorrect()
        {
            (var trainCountAtA, var trainCountAtB) = Solve(
                TimeSpan.FromMinutes(2),
                new[]
                {
                    (new TimeSpan(9, 0, 0), new TimeSpan(9, 1, 0)),
                    (new TimeSpan(12, 0, 0), new TimeSpan(12, 2, 0))
                },
                new ValueTuple<TimeSpan, TimeSpan>[] { });

            Assert.Equal(2, trainCountAtA);
            Assert.Equal(0, trainCountAtB);
        }

        [Fact]
        public void SmallDatasetIsCorrect()
        {
            TestDataset("SmallInput.txt", "SmallOutput.txt");
        }

        [Fact]
        public void LargeDatasetIsCorrect()
        {
            TestDataset("LargeInput.txt", "LargeOutput.txt");
        }

        private void TestDataset(string inputName, string outputName)
        {
            using (var inputStream = GetType().Assembly.GetManifestResourceStream(GetType(), inputName))
            using (var input = new StreamReader(inputStream))
            using (var outputStream = GetType().Assembly.GetManifestResourceStream(GetType(), outputName))
            using (var output = new StreamReader(outputStream))
            {
                var actualOutput = new StringWriter();
                SolveDataset(input, actualOutput);
                Assert.Equal(output.ReadToEnd().TrimEnd('\r', '\n'), actualOutput.ToString().TrimEnd('\r', '\n'));
            }
        }
    }
}
