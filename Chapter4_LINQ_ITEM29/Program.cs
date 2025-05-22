namespace Chapter4_LINQ_ITEM29
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in GetAlpabet())
            {
                Console.WriteLine(item);
            }

            var foo = (from n in Enumerable.Range(0, 100) select n * n).ToArray();
            foo.ForAll(x => Console.WriteLine(x));
        }

        public static IEnumerable<char> GetAlpabet()
        {
            var letter = 'a';
            while (letter < 'z')
            {
                yield return letter;
                letter++;
            }
        }

        private static IEnumerable<Tuple<int, int>> QueryIndices3()
        {
            return from x in Enumerable.Range(0, 100)
                   from y in Enumerable.Range(0, 100)
                   where x + y < 100
                   orderby (x * x + y * y) descending
                   select Tuple.Create(x, y);
        }

        private static IEnumerable<Tuple<int, int>> MethodIndices3()
        {
            return Enumerable.Range(0, 100)
                .SelectMany(x => Enumerable.Range(0, 100), (x, y) => Tuple.Create(x, y))
                .Where(pt => pt.Item1 + pt.Item2 < 100)
                .OrderByDescending(pt => pt.Item1 * pt.Item2 + pt.Item2 * pt.Item2);
        }


        //아래 메서드는 필터링하는 순회방식을 prdicate를 사용하여 출력과 분리한 예시
        public static IEnumerable<T> EvertyNthItem<T>(IEnumerable<T> Source, Predicate<T> filterFunc)
        {
            foreach (T item in Source)
            {
                if (filterFunc(item))
                {
                    yield return item;
                }
            }
        }

        //아래 메서드는 N번째 요소만을 반환하는 코드 예시
        public static IEnumerable<T> EvertyNthItem<T>(IEnumerable<T> Source, int period)
        {
            var count = 0;
            foreach (T item in Source)
            {
                if(++count % period == 0)
                {
                    yield return item;
                }
            }
        }

        //아래 메서드는 새로운 시퀀스를 생성하는 변환 메서드 예시
        public static IEnumerable<T> Select<T>(IEnumerable<T> Source,Func<T,T> methoc)
        {
            foreach(T t in Source)
            {
                yield return methoc(t);
            }
        }
    }
    public static class Extensions
    {
        public static void ForAll<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }
    }

}
