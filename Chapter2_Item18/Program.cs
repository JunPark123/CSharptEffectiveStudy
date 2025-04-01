/*
제목 : 18 반드시 필요한 제약 조건만 설정하라

 
****************내용****************
타입매개변수로 전달할 수 있는 타입의 유형을 제한할 수 있다. 하지만 너무 많은 제약 ㅈ건을 설정하면
과도한 추가 작업을 수행해야하므로 적당한 균형을 맞추는 것이 좋다.

제약조건을 설정하면 컴파일러는 사용자가 타입 매개변수로 올바른 타입을 지정했는지 컴파일타임에 확인가능

제약조건을 최소화하기 위한 방법
1. 제네릭 타입 내에서 반드시 필요한 기능만을 제약 조건으로 설정하기.
2. 
 */

namespace Chapter3_Item18
{
    internal class Program
    {
        

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

    public class Sample2
    {
     
       public static bool AreEqual<T>(T left, T right) // 런타임에 검사할 수 있다
        {
            if (left == null) {return right == null; }

            if(left is IComparable<T>)
            {
                IComparable<T> lval = left as IComparable<T>;
                if(right  is IComparable<T>)
                {
                    return lval.CompareTo(right) == 0;
                }
                else
                {
                    throw new ArgumentException("Type does not implement ");
                }

            }
            else
            {
                throw new ArgumentException("Type does not implement ");
            }
        }

        public static bool AreEqual2<T>(T left, T right)  //제약조건을 설정하여 간단하게 작성할 수 있다.
        where T : IComparable<T> => left.CompareTo(right) ==0; //컴파일타임에 확인할 수 있다.
    }
    
}
