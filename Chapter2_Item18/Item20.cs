using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3_Item18
{
    public struct Customer : IComparable<Customer>, IComparable //자기 자신과 비교한다
    {
        private readonly string name;
        private double revenue;
        public Customer(string name, double revenue)
        {
            this.name = name;
            this.revenue = revenue;
        }

        public int CompareTo(Customer customer) => name.CompareTo(customer.name);
        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Customer))
                throw new ArgumentException("Arguement is not a customer", "obj");

            Customer otherCustomer = (Customer)obj;
            return this.CompareTo(otherCustomer);
        }
        public static bool operator <(Customer left, Customer right) => left.CompareTo(right) < 0;
        public static bool operator <=(Customer left, Customer right) => left.CompareTo(right) <= 0;
        public static bool operator >(Customer left, Customer right) => left.CompareTo(right) > 0;
        public static bool operator >=(Customer left, Customer right) => left.CompareTo(right) >= 0;

        private static Lazy<RevenumComparer> revComp = new Lazy<RevenumComparer>(() => new RevenumComparer()); //revenueecompare객체 생성
        public static IComparer<Customer> RevenumeCompare => revComp.Value; // revcomp객체를 외부에서 호출하기 위한 용도
        public static Comparison<Customer> CompareByRevenue => (left, right) => left.revenue.CompareTo(right);

        private class RevenumComparer : IComparer<Customer>
        {
            int IComparer<Customer>.Compare(Customer left, Customer right) => left.revenue.CompareTo(right.revenue);
        }


        public class Sample
        {
            public void ss()
            {
                var customers = new List<Customer>
                {
                    new Customer("Alice", 3000),
                    new Customer("Bob", 2000),
                    new Customer("Charlie", 4000)
                };

                // 매출순 정렬 - IComparer 사용
                customers.Sort(Customer.RevenumeCompare);

                // 매출순 정렬 - Comparison<T> 델리게이트 사용
                customers.Sort(Customer.CompareByRevenue);
            }
        }
    }
}
