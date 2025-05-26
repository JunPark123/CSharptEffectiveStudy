using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4_LINQ_ITEM29
{
    public static class cSelectMany
    {
        public static void SelectManyPractic()
        {
            int[] odds = { 1, 3, 5, 7 };
            int[] evens = { 2, 4, 6, 8 };

            var lists = odds.SelectMany(oddnum => evens, (oddnum, evennum) => new
            {
                oddnum,
                evennum,
                sum = oddnum + evennum
            });

            /* 1) string.Join + Select ⇒ 한 번에 출력 --------------------------------- */
            //Console.WriteLine(string.Join(Environment.NewLine,
            //    lists.Select(p => $"{p.oddnum} + {p.evennum} = {p.sum}")));

            lists.ForAll(x => Console.WriteLine($"{x.oddnum} + {x.evennum} = {x.sum}"));

            Console.WriteLine("---------------------");
            var list2 = odds.SelectMany(oddnum => evens, (oddnum, evennum) =>
            new { oddnum, evennum }).
            Where(pair => pair.oddnum > pair.evennum).
            Select(pair => new
            {
                pair.oddnum,
                pair.evennum,
                sum = pair.oddnum + pair.evennum
            });

            list2.ForAll(x => Console.WriteLine($"{x.oddnum} + {x.evennum} = {x.sum}"));

        }

        //SelectMany 함수가 구현된 메서드
        public static IEnumerable<TOutput> SelectMany<T1, T2, TOutput>(
            this IEnumerable<T1> src,
            Func<T1, IEnumerable<T2>> inpustSelector,
            Func<T1, T2, TOutput> resultSelctor)
        {
            //첫번째 입력 시퀀스를 순회하는 동안 입력 시퀀스의 현재 값을 활용하여
            foreach (T1 first in src)
            {
                //두번째 시퀀스를 순회한다.inputSelector는 처음시퀀스 값에 의존
                foreach (T2 second in inpustSelector(first))
                    yield return resultSelctor(first, second);
                //두 시퀀스로부터 개별 요소의 쌍을 매개변수로 resultSelctor 호출함
            }
        }
    }

    public static class cEvaluation
    {
        private static IEnumerable<TResult> Generate<TResult>(
            int number,Func<TResult> generator)
        {
            for (var i = 0; i < number; i++)
                yield return generator();
        }

        public static void LazyEvaluation()
        {
            Console.WriteLine($"Start Time {DateTime.Now:T}");
            var seq = Generate(10, () => DateTime.Now);
            Console.WriteLine("Wating \tPress Return ");
            Console.ReadLine();

            Console.WriteLine("Iterating");
            foreach(var val in seq)
            {
                Console.WriteLine($"{val:HH:mm:ss}");
            }

            Console.WriteLine("wating \t press return");
            Console.ReadLine();

            Console.WriteLine("Iterating");

            foreach (var val in seq)
            {
                Console.WriteLine($"{val:T}");
            }
            Console.ReadLine();
            aaa();
        }

        public static void aaa()
        {
            var answers = from number in AllNumbers()
                          where number < 10
                          select number;

            foreach (var item in answers)
            {
                Console.WriteLine(item);
            }
        }


        //무한시퀀스
        static IEnumerable<int> AllNumbers()
        {
            var number = 0;
            while (number < int.MaxValue)
                yield return number++;
        }
    }
}
