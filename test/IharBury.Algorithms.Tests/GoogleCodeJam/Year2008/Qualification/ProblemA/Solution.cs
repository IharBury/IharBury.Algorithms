using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static System.FormattableString;
using static System.Globalization.CultureInfo;
using static System.Linq.Enumerable;

namespace IharBury.Algorithms.Tests.GoogleCodeJam.Year2008.Qualification.ProblemA
{
    public static class Solution
    {
        public static int Solve(IReadOnlyCollection<string> searchEngines, IReadOnlyCollection<string> queries)
        {
            var queryGroupCount = queries
                .GroupWhile(
                    item => new HashSet<string>() { item },
                    (group, item) =>
                    {
                        group.Add(item);
                        return group;
                    },
                    (group, item) => group.Contains(item) || (group.Count + 1 < searchEngines.Count))
                .Count();
            return queryGroupCount == 0 ? 0 : queryGroupCount - 1;
        }

        public static void SolveDataset(TextReader input, TextWriter output)
        {
            var caseCount = int.Parse(input.ReadLine(), InvariantCulture);

            for (var caseNumber = 1; caseNumber <= caseCount; caseNumber++)
            {
                var searchEngineCount = int.Parse(input.ReadLine(), InvariantCulture);
                var searchEngines = Range(1, searchEngineCount).Select(_ => input.ReadLine()).ToList();
                var queryCount = int.Parse(input.ReadLine(), InvariantCulture);
                var queries = Range(1, queryCount).Select(_ => input.ReadLine()).ToList();

                var switchCount = Solve(searchEngines, queries);

                output.WriteLine(Invariant($"Case #{caseNumber}: {switchCount}"));
            }
        }
    }
}
