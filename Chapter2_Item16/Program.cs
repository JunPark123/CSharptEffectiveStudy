/*
제목 : Item16 생성자 내에서는 절대로 가상 함수를 호출하지 말라

 
****************내용****************
C#에서 생성자에 가상함수를 호출하면 멤버 초기화 시 예기치 못한 오류가 생길 수 있음


 */



namespace Chapter2_Item16
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var dd = new ABC();
            Console.WriteLine(dd.msg);
        }
    }
}

public class ABC
{
    public readonly string msg = "Static msg";

    public ABC()
    {
        msg = "Change msg";
    }
}

abstract class B
{
    protected B()
    {
        VFunc();
    }

    protected virtual void VFunc() { }
}
class Derived : B
{
    private readonly string Msg = "Set By Initialize";
    public Derived(string msg)
    {
        this.Msg = msg;
    }

    protected override void VFunc()
    {
        Console.WriteLine(Msg);
    }

    //public static void Main(string[] args)
    //{
    //    var d = new Derived("Constructed in main");
    //}
}
