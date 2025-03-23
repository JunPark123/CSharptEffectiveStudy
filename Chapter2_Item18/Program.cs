/*
제목 : 18 반드시 필요한 제약 조건만 설정하라

 
****************내용****************

 */

namespace Chapter3_Item18
{
    internal class Program
    {
        public static bool AreEqual2<T>(T left, T right)
        where T : IComparable<T> => left.Equals(right); //컴파일러를 통해 런타임 오류가 발생하지 않도록 함.

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }


    public class Sample
    {
        public delegate T FactoryFunc<T>();

        public static T Factroy<T>(FactoryFunc<T> makeANewT) where T : new()
        {
            T Rval = makeANewT();
            if (Rval == null) { return new T(); }
            else { return Rval; }
        }
    }

}
