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
    }
}
