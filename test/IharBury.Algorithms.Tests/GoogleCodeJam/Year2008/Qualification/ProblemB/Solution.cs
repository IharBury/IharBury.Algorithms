using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static System.FormattableString;
using static System.Globalization.CultureInfo;
using static System.Linq.Enumerable;

namespace IharBury.Algorithms.Tests.GoogleCodeJam.Year2008.Qualification.ProblemB
{
    public static class Solution
    {
        public static (int trainCountAtA, int trainCountAtB) Solve(
            TimeSpan turnaroundTime,
            IReadOnlyCollection<(TimeSpan departureTime, TimeSpan arrivalTime)> tripsFromAToB,
            IReadOnlyCollection<(TimeSpan departureTime, TimeSpan arrivalTime)> tripsFromBToA)
        {
            return (SolveOneWay(turnaroundTime, tripsFromAToB, tripsFromBToA),
                SolveOneWay(turnaroundTime, tripsFromBToA, tripsFromAToB));
        }

        private static int SolveOneWay(
            TimeSpan turnaroundTime,
            IReadOnlyCollection<(TimeSpan departureTime, TimeSpan arrivalTime)> outboundTrips,
            IReadOnlyCollection<(TimeSpan departureTime, TimeSpan arrivalTime)> inboundTrips)
        {
            var trainRequirementChanges = outboundTrips
                .Select(trip => (time: trip.departureTime, changeAmount: 1))
                .Concat(inboundTrips.Select(trip => (time: trip.arrivalTime + turnaroundTime, changeAmount: -1)));

            return trainRequirementChanges
                .OrderBy(change => change.time)
                .ThenBy(change => change.changeAmount)
                .GetRunningSumChecked(change => change.changeAmount)
                .MaxOptional()
                .OrDefault(0)
                .ButMin(0);
        }

        public static void SolveDataset(TextReader input, TextWriter output)
        {
            (TimeSpan departure, TimeSpan arrival) ParseTrip(string trip)
            {
                var times = trip.Split(' ').Select(time => TimeSpan.Parse(time, InvariantCulture)).ToList();
                return (times[0], times[1]);
            }

            var caseCount = int.Parse(input.ReadLine(), InvariantCulture);

            for (var caseNumber = 1; caseNumber <= caseCount; caseNumber++)
            {
                var turnaround = TimeSpan.FromMinutes(int.Parse(input.ReadLine(), InvariantCulture));

                var tripCounts = input.ReadLine().Split(' ').Select(count => int.Parse(count, InvariantCulture)).ToList();
                var tripCountFromAToB = tripCounts[0];
                var tripCountFromBToA = tripCounts[1];

                var tripsFromAToB = Range(1, tripCountFromAToB).Select(_ => ParseTrip(input.ReadLine())).ToList();
                var tripsFromBToA = Range(1, tripCountFromBToA).Select(_ => ParseTrip(input.ReadLine())).ToList();

                (var trainCountAtA, var trainCountAtB) = Solve(turnaround, tripsFromAToB, tripsFromBToA);

                output.WriteLine(Invariant($"Case #{caseNumber}: {trainCountAtA} {trainCountAtB}"));
            }
        }
    }
}
