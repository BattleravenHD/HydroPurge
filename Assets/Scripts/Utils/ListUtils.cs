using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class ListUtils
    {
        private static System.Random _random = new System.Random();

        public static T RandomElement<T>(this T[] array)
        {
            if (array == null || array.Length == 0) throw new Exception("list cannot be null or empty");
            return array[Random.Range(0, array.Length)];
        }

        public static T RandomElement<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) throw new Exception("list cannot be null or empty");
            return list[Random.Range(0, list.Count)];
        }

        public static bool IndexIsInRange<T>(this List<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }

        public static bool IndexIsInRange<T>(this T[] list, int index)
        {
            return index >= 0 && index < list.Length;
        }

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static bool ContainsDuplicates<T>(List<T> list)
        {
            HashSet<T> set = new HashSet<T>();
            foreach (T element in list)
            {
                if (set.Contains(element))
                    return true;
                set.Add(element);
            }

            return false;
        }
    }
}