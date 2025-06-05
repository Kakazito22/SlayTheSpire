using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T Draw<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return default(T);
        }

        int index = Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }
}
