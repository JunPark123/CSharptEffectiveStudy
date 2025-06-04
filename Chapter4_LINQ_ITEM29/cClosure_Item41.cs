using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4_LINQ_ITEM29
{
    public static class cClosure_Item41
    {
        public static void ClosureSampleAboutBadEx()
        {
            //람다식이나 익명메서드가 외부 지역변수에 접근할 때 이 변수들을 캡처하고 유지하는 기능을 클로저라고 함

            //아래는 기본 클로저 형식을 나타냄
            Func<int> MakeCounter()
            {
                int counter = 0;
                return () =>
                {
                    counter++;
                    return counter;
                };
            }

            var c = new Closure();
            c.generatedCounter = 0;
            var numbers = new Func<int>(() => c.generatedCounterFunc());
            for (int i = 0; i < 30; i++)
            {
                int nu = numbers();
                Console.WriteLine(nu);
            }

            var query = (from n in SomeFunction() select n).Take(5); // 배열 0~4까지 만드는 링큐 쿼리문
            foreach (var n in query)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("Again");

            foreach (var n in query)
            {
                Console.WriteLine(n);
            }
        }

        public static IEnumerable<int> SomeFunction()
        {
            using (Generator g = new Generator())
            {
                while (true)
                {
                    yield return g.GentNextNumber();
                }
            }
        }

        internal sealed class Closure
        {
            public int generatedCounter;
            public int generatedCounterFunc() => generatedCounter++;
        }
        internal sealed class Generator : IDisposable
        {
            private int count;
            public int GentNextNumber() => count++;
            public void Dispose() => Console.WriteLine("Dispose 됨");
        }

        public static IEnumerable<int> MakeAnotherSequnce()
        {
            var counter = 0;
            var interim = Extensions.Generate(30, () => counter++);
            var gen = new Random();
            var numbers = from n in interim
                          select gen.Next() - n;
            return numbers;
        }
    }


    // 그냥 연습
    class Calculator
    {
        public int factor = 3;

        public Func<int, int> GetMultiplier()
        {
            return x => x * this.factor;
        }

        public Func<int, int> GetMultiplier1() => x=> x * this.factor;

        private static int cold(int n) => n * n;
        public void ccc()
        {

            Func<int, int> method = new Func<int, int>(GetMultiplier());
            Func<int, int> method2 = GetMultiplier1();
            Func<int, int> method3 = new Func<int, int>((int n) => n * n);
           
            Func<int, int> method4 = ((int n) =>
            {
                return n * n;
            });

            

            int y = method(1);
        }
    }

}
