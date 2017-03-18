using System;
using System.Collections.Generic;

namespace IharBury.Algorithms
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Combines all the items in <paramref name="items"/>
        /// into sequential groups of custom type <typeparamref name="TGroup"/>.
        /// The given <paramref name="shouldAddToGroup"/> delegate is used
        /// to determine whether an item should be added to the existing group or to a new group.
        /// </summary>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <typeparam name="TGroup">Type of the groups.</typeparam>
        /// <param name="items">
        /// The items to be grouped.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <param name="createGroup">
        /// A delegate to create a new group with the given item in it.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <param name="addToGroup">
        /// A delegate to add a new item to a group.
        /// Returns the group with the new item added which can be same group object.
        /// Can modify the old group object.
        /// Cannot modify the item.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <param name="shouldAddToGroup">
        /// The delegate to determine whether an item should be added to the existing group or to a new group.
        /// Cannot modify the group.
        /// Cannot modify the item.
        /// Cannot be <c>null</c>.
        /// </param>
        public static IEnumerable<TGroup> GroupWhile<TItem, TGroup>(
            this IEnumerable<TItem> items,
            Func<TItem, TGroup> createGroup,
            Func<TGroup, TItem, TGroup> addToGroup,
            Func<TGroup, TItem, bool> shouldAddToGroup)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (createGroup == null)
                throw new ArgumentNullException(nameof(createGroup));
            if (addToGroup == null)
                throw new ArgumentNullException(nameof(addToGroup));
            if (shouldAddToGroup == null)
                throw new ArgumentNullException(nameof(shouldAddToGroup));

            var group = Optional<TGroup>.None;

            foreach (var item in items)
            {
                if (group.HasValue)
                {
                    if (shouldAddToGroup(group.Value, item))
                    {
                        group = addToGroup(group.Value, item);
                    }
                    else
                    {
                        yield return group.Value;
                        group = createGroup(item);
                    }
                }
                else
                {
                    group = createGroup(item);
                }
            }

            if (group.HasValue)
                yield return group.Value;
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of the values calculated by <paramref name="getValue"/> for all the items up to the current item (inclusive).
        /// </summary>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <typeparam name="TValue">Type of the calculated values.</typeparam>
        /// <typeparam name="TSum">Type of the sum.</typeparam>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="getValue">
        /// A delegate to calculate the values to be summed. Cannot modify the item. Cannot be <c>null</c>.
        /// </param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <param name="add">
        /// A delegate to add a value to the running sum and return the result which can be the same sum object.
        /// Can modify the old sum object.
        /// Cannot modify the item.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<TSum> GetRunningSum<TItem, TValue, TSum>(
            this IEnumerable<TItem> items,
            Func<TItem, TValue> getValue,
            TSum initialSum,
            Func<TSum, TValue, TSum> add)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));
            if (add == null)
                throw new ArgumentNullException(nameof(add));

            var currentSum = initialSum;

            foreach (var item in items)
            {
                currentSum = add(currentSum, getValue(item));
                yield return currentSum;
            }
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of the values calculated by <paramref name="getValue"/> for all the items up to the current item (inclusive).
        /// </summary>
        /// <exception cref="OverflowException">If the sum does not fit into <see cref="int"/>.</exception>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="getValue">
        /// A delegate to calculate the values to be summed. Cannot modify the item. Cannot be <c>null</c>.
        /// </param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<int> GetRunningSumChecked<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, int> getValue,
            int initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return items.GetRunningSum(getValue, initialSum, (sum, item) => checked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of the values calculated by <paramref name="getValue"/> for all the items up to the current item (inclusive).
        /// No exception is thrown when the sum does not fit into <see cref="int"/>.
        /// </summary>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="getValue">
        /// A delegate to calculate the values to be summed. Cannot modify the item. Cannot be <c>null</c>.
        /// </param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<int> GetRunningSumUnchecked<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, int> getValue,
            int initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return items.GetRunningSum(getValue, initialSum, (sum, item) => unchecked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of the values calculated by <paramref name="getValue"/> for all the items up to the current item (inclusive).
        /// </summary>
        /// <exception cref="OverflowException">If the sum does not fit into <see cref="long"/>.</exception>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="getValue">
        /// A delegate to calculate the values to be summed. Cannot modify the item. Cannot be <c>null</c>.
        /// </param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<long> GetRunningSumChecked<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, long> getValue,
            long initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return items.GetRunningSum(getValue, initialSum, (sum, item) => checked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of the values calculated by <paramref name="getValue"/> for all the items up to the current item (inclusive).
        /// No exception is thrown when the sum does not fit into <see cref="long"/>.
        /// </summary>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="getValue">
        /// A delegate to calculate the values to be summed. Cannot modify the item. Cannot be <c>null</c>.
        /// </param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<long> GetRunningSumUnchecked<TItem>(
            this IEnumerable<TItem> items,
            Func<TItem, long> getValue,
            long initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return items.GetRunningSum(getValue, initialSum, (sum, item) => unchecked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of all the items up to the current item (inclusive).
        /// </summary>
        /// <typeparam name="TItem">Type of the items.</typeparam>
        /// <typeparam name="TSum">Type of the sum.</typeparam>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <param name="add">
        /// A delegate to add an item to the running sum and return the result which can be the same sum object.
        /// Can modify the old sum object.
        /// Cannot modify the item.
        /// Cannot be <c>null</c>.
        /// </param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<TSum> GetRunningSum<TItem, TSum>(
            this IEnumerable<TItem> items,
            TSum initialSum,
            Func<TSum, TItem, TSum> add)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (add == null)
                throw new ArgumentNullException(nameof(add));

            var currentSum = initialSum;

            foreach (var item in items)
            {
                currentSum = add(currentSum, item);
                yield return currentSum;
            }
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of all the items up to the current item (inclusive).
        /// </summary>
        /// <exception cref="OverflowException">If the sum does not fit into <see cref="int"/>.</exception>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<int> GetRunningSumChecked(
            this IEnumerable<int> items,
            int initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return items.GetRunningSum(initialSum, (sum, item) => checked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of all the items up to the current item (inclusive).
        /// No exception is thrown when the sum does not fit into <see cref="int"/>.
        /// </summary>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<int> GetRunningSumUnchecked(
            this IEnumerable<int> items,
            int initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return items.GetRunningSum(initialSum, (sum, item) => unchecked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of all the items up to the current item (inclusive).
        /// </summary>
        /// <exception cref="OverflowException">If the sum does not fit into <see cref="long"/>.</exception>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<long> GetRunningSumChecked(
            this IEnumerable<long> items,
            long initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return items.GetRunningSum(initialSum, (sum, item) => checked(sum + item));
        }

        /// <summary>
        /// For each item in <paramref name="items"/> calculates running sum
        /// of all the items up to the current item (inclusive).
        /// No exception is thrown when the sum does not fit into <see cref="long"/>.
        /// </summary>
        /// <param name="items">The items. Cannot be <c>null</c>.</param>
        /// <param name="initialSum">Initial value of the running sum before the first item is added.</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of the running sums for each item.</returns>
        public static IEnumerable<long> GetRunningSumUnchecked(
            this IEnumerable<long> items,
            long initialSum = 0)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            return items.GetRunningSum(initialSum, (sum, item) => unchecked(sum + item));
        }

        /// <summary>
        /// Returns <see cref="Optional{T}"/> for the max item in a sequence
        /// or <see cref="Optional{T}.None"/> if the sequence is empty.
        /// </summary>
        /// <param name="items">The sequence.</param>
        public static Optional<int> MaxOptional(this IEnumerable<int> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var max = Optional<int>.None;

            foreach (var item in items)
                if (!max.HasValue || (max.Value < item))
                    max = item;

            return max;
        }

        /// <summary>
        /// Returns <see cref="Optional{T}"/> for the min item in a sequence
        /// or <see cref="Optional{T}.None"/> if the sequence is empty.
        /// </summary>
        /// <param name="items">The sequence.</param>
        public static Optional<int> MinOptional(this IEnumerable<int> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var min = Optional<int>.None;

            foreach (var item in items)
                if (!min.HasValue || (min.Value > item))
                    min = item;

            return min;
        }

        /// <summary>
        /// Returns <see cref="Optional{T}"/> for the max item in a sequence
        /// or <see cref="Optional{T}.None"/> if the sequence is empty.
        /// </summary>
        /// <param name="items">The sequence.</param>
        public static Optional<long> MaxOptional(this IEnumerable<long> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var max = Optional<long>.None;

            foreach (var item in items)
                if (!max.HasValue || (max.Value < item))
                    max = item;

            return max;
        }

        /// <summary>
        /// Returns <see cref="Optional{T}"/> for the min item in a sequence
        /// or <see cref="Optional{T}.None"/> if the sequence is empty.
        /// </summary>
        /// <param name="items">The sequence.</param>
        public static Optional<long> MinOptional(this IEnumerable<long> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var min = Optional<long>.None;

            foreach (var item in items)
                if (!min.HasValue || (min.Value > item))
                    min = item;

            return min;
        }
    }
}
