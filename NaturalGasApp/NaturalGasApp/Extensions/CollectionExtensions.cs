namespace NaturalGasApp.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Returns the last element index of a sequence that satisfies a condition or -1 if no such element is found.
    /// </summary>
    /// <param name="source">The collection to search through.</param>
    /// <param name="predicate">A function that defines the condition to check for each element in the collection.</param>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <returns>The index of the last element that satisfies the specified condition defined by the predicate.
    /// If no such element is found, returns -1.</returns>
    public static int LastMatchingIndex<T>(this IList<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
        for (var index = source.Count - 1; index >= 0; index--)
        {
            if (predicate(source[index]))
            {
                return index;
            }
        }
        return -1;
    }
}
