/*
제목 : Item15 불필요한 객체를 만들지 말라

 
****************내용****************
가비지 수집기는 유저 대신 메모리를 훌륭히 관리함 하지만 가비지 수집기를 불필요하게 동작하는건 자원 낭비가 발생할 수 있음.
참조 타입의 객체는 지역 변수라고 하더라도 동적으로 메모리를 할당함. 자주 반복해서 객체 생성 제거하는 행위는 비효율적임.
Font 타입과 같이 IDisposable 인터페이스를 구현한 타입의 객체를 지역->멤버 변수로 변경하면 IDisaposable을 구현해야 한다.
호출이 빈번하지 않으면 굳이 변경안해도 됨
 
객체 생성을 최소화하기 위한 방법
1. 자주 사용되는 객체는 멤버 변수로 변경한다.
2. 자주 사용되는 객체를 생성했다가 이를 재활용(종속성 삽입 dependency injection)
3. 변경 불가능한 타입과 관련
 */



namespace Chapter2_Item15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}


public class SampleNo1No2
{
    public Data _data = new Data(); //자주사용하는 객체는 멤버변수로 만든다(자주사용한다고 가정함)

    
}

public class Data
{
    public string Name {  get; set; }


    public string DependencySampe() //string은 2번때문에 임의로 만든 객체라고 가정함
    {
        if(Name == null)
        {
            Name = new string("객체는 한 번만 생성하도록");
        }
        return Name;
    }
}
