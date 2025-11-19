// CollectionExtensions.cs
using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{

    // Узагальнений метод для вибору N елементів, що мають найбільше значення за заданим критерієм.
    public static IEnumerable<T> TopNByValue<T>(
        this IEnumerable<T> source, 
        int count, 
        Func<T, double> valueSelector)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (count <= 0) return Enumerable.Empty<T>();
        
        // Сортуємо колекцію за значенням критерію у порядку спадання (OrderByDescending)
        // та беремо перші 'count' елементів (Take).
        return source
            .OrderByDescending(valueSelector)
            .Take(count);
    }
}