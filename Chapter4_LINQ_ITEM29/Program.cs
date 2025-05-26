using System.Net.Sockets;

namespace Chapter4_LINQ_ITEM29
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SelectMany연습입니다");
            cSelectMany.SelectManyPractic();

            Console.WriteLine("즉시평가, 지연평가 비교 예제코드입니다.");
            cEvaluation.LazyEvaluation();

            Console.WriteLine("이터레이터 연습입니다");
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
                if (++count % period == 0)
                {
                    yield return item;
                }
            }
        }

        //아래 메서드는 새로운 시퀀스를 생성하는 변환 메서드 예시
        public static IEnumerable<T> Select<T>(IEnumerable<T> Source, Func<T, T> methoc)
        {
            foreach (T t in Source)
            {
                yield return methoc(t);
            }
        }

        //아래 이터레이터 메서드는 새로운 시퀀스를 생성하는 메서드이다. 매개변수가 시퀀스가 아닌 형태임
        //또한 다른 메서드와 조합하여 순회를 중단할 수 있음
        public static IEnumerable<int> CreateSequnece(int numberOfElements, int startAt, int stepBy)
        {
            for (var i = 0; i < numberOfElements; i++)
            {
                yield return startAt + i * stepBy;
            }


#if DEBUG
            //호출자 예시
            var listStorage = new List<int>(CreateSequnece(100, 0, 5));
            var sequence = CreateSequnece(10000, 0, 7).TakeWhile(delegate (int num) { return num < 1000; });
            var sequence2 = CreateSequnece(10000, 0, 7).TakeWhile(num => num < 1000);
#endif
        }

        //아래 메서드는 CreateSquence에 매개변수로 출력 시퀀스를 만들기 위한 함수로 받는 버전이다
        public static IEnumerable<T> CreateSequnece<T>(int numberOfElements, Func<T> generator)
        {
            for (var i = 0; i < numberOfElements; i++)
            {
                yield return generator();
            }

#if DEBUG
            int startAt = 0;
            //호출자 예시
            var listStorage = new List<int>(CreateSequnece(100, () => startAt += 5));
#endif
        }

        public static TResult Fold<T, TResult>(IEnumerable<T> sequence, TResult total, Func<T, TResult, TResult> ACCUMULATOR)
        {
            foreach (T item in sequence)
            {
                total = ACCUMULATOR(item, total);
            }
            return total;

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
