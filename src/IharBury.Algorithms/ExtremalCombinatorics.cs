using System;
using System.Collections.Generic;
using System.Linq;

namespace IharBury.Algorithms
{
    public static class ExtremalCombinatorics
    {
        public static IEnumerable<Tuple<T1, T2, T3>> GetMaxSetOfUniqueCombinationsWithPairUseLimit<T1, T2, T3>(
            IReadOnlyList<T1> items1,
            IReadOnlyList<T2> items2,
            IReadOnlyList<T3> items3,
            int maxPairUseLimit)
        {
            if (items1 == null)
                throw new ArgumentNullException(nameof(items1));
            if (items2 == null)
                throw new ArgumentNullException(nameof(items2));
            if (items3 == null)
                throw new ArgumentNullException(nameof(items3));
            if (maxPairUseLimit < 0)
                throw new ArgumentOutOfRangeException(nameof(maxPairUseLimit));

            var itemListsOrderedByCount = new[]
            {
                new { List = (object)items1, items1.Count },
                new { List = (object)items2, items2.Count },
                new { List = (object)items3, items3.Count }
            }
                .OrderBy(itemList => itemList.Count)
                .ToList();
            var item1ListIndex = itemListsOrderedByCount.FindIndex(itemList => itemList.List == items1);
            var item2ListIndex = itemListsOrderedByCount.FindIndex(itemList => itemList.List == items2);
            var item3ListIndex = itemListsOrderedByCount.FindIndex(itemList => itemList.List == items3);
            var effectiveMaxPairUse = Math.Min(maxPairUseLimit, itemListsOrderedByCount[2].Count);
            var itemIndexesInListsOrderedByCount = new int[3];
            foreach (var minSizeItemListItemIndex in Enumerable.Range(0, itemListsOrderedByCount[0].Count))
            {
                itemIndexesInListsOrderedByCount[0] = minSizeItemListItemIndex;
                foreach (var midSizeItemListItemIndex in Enumerable.Range(0, itemListsOrderedByCount[1].Count))
                {
                    itemIndexesInListsOrderedByCount[1] = midSizeItemListItemIndex;
                    foreach (var combinationSubindex in Enumerable.Range(0, effectiveMaxPairUse))
                    {
                        var maxSizeItemListItemIndex = 
                            (minSizeItemListItemIndex + midSizeItemListItemIndex + combinationSubindex) %
                            itemListsOrderedByCount[2].Count;
                        itemIndexesInListsOrderedByCount[2] = maxSizeItemListItemIndex;
                        yield return Tuple.Create(
                            items1[itemIndexesInListsOrderedByCount[item1ListIndex]],
                            items2[itemIndexesInListsOrderedByCount[item2ListIndex]],
                            items3[itemIndexesInListsOrderedByCount[item3ListIndex]]);
                    }
                }
            }
        }
    }
}
