using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3_Item18
{
    public static class Enumerable
    {
        public static T MaxA<T>(this IEnumerable<T> source, Func<T, T, bool> compare)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

            T max = enumerator.Current;

            while (enumerator.MoveNext())
            {
                if (compare(enumerator.Current, max))
                    max = enumerator.Current;
            }

            return max;
        }
    }
    internal class Item21
    {
        public void medd()
        {
            string a = "dd";
            string b = "dd";

            a.CompareTo(b);

            int c = 6;
            int d = 7;
            int sum = Example.Add(1, 9, (x, y) => x + y);

            string[] list = { "a", "bb", "ccc" };
            int maxLength = list.MaxA(s => s.Length); // Selector 사용
        }
        
        
        public static class Example
        {
            public static T Add<T>(T left, T right, Func<T, T, T> addfunc)
            {
                int[] a = { 1, 2 };
                a.Max()
                return addfunc(left, right);
            }
        } 

        
    }
}
