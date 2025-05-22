namespace SWSA.MvcPortal.Commons.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Safely adds a range of items to the collection.
    /// This method avoids adding duplicate values by checking existing elements.
    /// </summary>
    public static void AddRangeSafe<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection == null || items == null) return;

        var existing = new HashSet<T>(collection);
        foreach (var item in items)
        {
            if (existing.Add(item)) // Add returns false if already exists
            {
                collection.Add(item);
            }
        }
    }

    /// <summary>
    /// Safely removes a range of items from the collection.
    /// Items that do not exist will be ignored without throwing exceptions.
    /// </summary>
    public static void RemoveRangeSafe<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection == null || items == null) return;

        var toRemove = new HashSet<T>(items);
        foreach (var item in toRemove)
        {
            if (collection.Contains(item))
            {
                collection.Remove(item);
            }
        }
    }

    /// <summary>
    /// Synchronize a target collection based on key comparison with new keys.
    /// Automatically removes unmatched existing entities and adds missing new entities.
    /// </summary>
    public static void SyncWithKeys<TEntity, TKey>(
        this ICollection<TEntity> target,
        IEnumerable<TKey> newKeys,
        Func<TEntity, TKey> keySelector,
        Func<TKey, TEntity> createEntity)
    {
        if (target == null || newKeys == null) return;

        var existingKeys = target.Select(keySelector).ToHashSet();
        var incomingKeys = newKeys.ToHashSet();

        var toRemove = target.Where(e => !incomingKeys.Contains(keySelector(e))).ToList();
        foreach (var item in toRemove)
            target.Remove(item);

        var toAdd = incomingKeys.Except(existingKeys);
        foreach (var key in toAdd)
            target.Add(createEntity(key));
    }

}
