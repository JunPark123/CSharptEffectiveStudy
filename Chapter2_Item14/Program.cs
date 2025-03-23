
//제목 : Item14 초기화 코드가 중복되는것을 최소화하라
//내용 : 여러 생성자 내에서 공통적으로 수행할 초기화 코드를
//공용으로 사용 할 수 있는 생성자에서 수행한다.
//변수에 대한 중복 초기화 코드를 제거해 주고 베이스 클래스의 생성자가 반복 호출되는 것을 막아줌

namespace Chapter2_Item14
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

/*
 ********* 아래 코드는 생성자 체인 기법이다**********
 아래 코드처럼 공통으로 인스턴스를 초기화하는 생성자를 추가함
하지만 결합도가 높아질 수 있음. 매개변수가 외부로 노출될 수 있음.
리플렉션을 사용하여 객체를 생성할 때는 기본생성자가 필요하다. 없으면 예외 발생 터짐
따라서 생성자 체인 기법을 사용하는게 낫다

$ 추가적으로 코드가 변경될 여지가 있다면 생성자 오버로딩하는게 낫다
 */
public class MyClass
{
    private List<ImportantData> coil;
    private string name;
    public MyClass() : this(0, "")
    {

    }

    public MyClass(int initialCount) : this(initialCount, string.Empty)
    {

    }

    public MyClass(int initialCount, string name)
    {
        coil = (initialCount > 0) ?
            new List<ImportantData>(initialCount) :
            new List<ImportantData>();

        this.name = name;
    }
}

public class ImportantData
{
    public string Name { get; set; }
    public string Description { get; set; }
}
