using System.IO;
using Xunit;

using static IharBury.Algorithms.Tests.GoogleCodeJam.Year2008.Qualification.ProblemA.Solution;

namespace IharBury.Algorithms.Tests.GoogleCodeJam.Year2008.Qualification.ProblemA
{
    public sealed class Tests
    {
        [Fact]
        public void Sample1IsCorrect()
        {
            Assert.Equal(1, Solve(
                new[]
                {
                    "Yeehaw",
                    "NSM",
                    "Dont Ask",
                    "B9",
                    "Googol"
                },
                new[]
                {
                    "Yeehaw",
                    "Yeehaw",
                    "Googol",
                    "B9",
                    "Googol",
                    "NSM",
                    "B9",
                    "NSM",
                    "Dont Ask",
                    "Googol"
                }));
        }

        [Fact]
        public void Sample2IsCorrect()
        {
            Assert.Equal(0, Solve(
                new[]
                {
                    "Yeehaw",
                    "NSM",
                    "Dont Ask",
                    "B9",
                    "Googol"
                },
                new[]
                {
                    "Googol",
                    "Dont Ask",
                    "NSM",
                    "NSM",
                    "Yeehaw",
                    "Yeehaw",
                    "Googol"
                }));
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
