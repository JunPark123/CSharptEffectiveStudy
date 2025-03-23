/*
제목 : Item17 표준 Disopose 패턴을 구현하라

 
****************내용****************
비관리 리소스를 포함하는 클래스는 반드시 finalizer를 구현해야함. 그 외에는 구현하지 말 것
finalizer(소멸자)가 존재하는 것만으로 성능상의 손해를 감수해야함.




$$$$ 비관리 리소스란?
.NET 런타임(GC, 가비지 컬렉터)이 자동으로 관리해주지 않는 리소스를 말해.

대표적인 예:

파일 핸들

네트워크 소켓

데이터베이스 연결

윈도우 핸들 (Window handle)

GDI 객체 (그래픽 관련)

포인터/메모리 주소

COM 객체 등
 */



namespace Chapter2_Item17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

public class MyResouceHog : IDisposable
{
    private bool alreadyDisposed = false; // 이미 dispose되었는지 나타내는 플래그
    public void Dispose() //하나의 메서드만 정의되어 있음
    {
        //1.모든비관리 리소스를 정리한다.
        //2. 모든 관리 리소스를 정리한다.
        //3. 객체가 이미 정리되었음을 나타내기 위한 상태 플래그 설정. 앞서 이미 정리된 객체에 대하여 추가로 정리작업이 요청될 경우 플래그 확인 후 예외 발생
        //4. finalizer 호출 회피. 이를 위해 C.SuppressFinalize(this) 를 호출한다.

        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (alreadyDisposed) //dispose는 한번만 수행하도록 한다
        {
            return;
        }

        if (isDisposing)
        {
            //관리 리소스를 정리한다
        }

        //비관리 리소를 정리한다
        alreadyDisposed = true;
    }

    public void ExampleMethod()
    {
        if (alreadyDisposed)
        {
            throw new ObjectDisposedException("MyResourceHog", "Called Example Method on Disposed object");
        }
    }
}

public class DerivedResourceHog : MyResouceHog
{
    private bool disposed = false;

    protected override void Dispose(bool isDisposing)
    {
        if (disposed) { return; }
        if (isDisposing)
        {
            //관리 리소스를 정리한다
        }

        //비관리 리소스를 정리한다.

        //base 클래스의 리소스를 정리한다.
        base.Dispose(isDisposing);

        disposed = true;
    }
}
