using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
제목 : 19 런타임에 타입을 확인하여 최적의 알고리즘을 사용하라

 
****************내용****************

 */
namespace Chapter3_Item18
{
    internal class Item19
    {
    }
}

public sealed class ReverseEnumerable<T> : IEnumerable<T>
{
    private class ReverseEnumerator : IEnumerator<T>
    {
        int currentIndex;
        IList<T> collection;
        public ReverseEnumerator(IList<T> srcCollection)
        {
            collection = srcCollection;
            currentIndex = collection.Count;
        }

        public T Current => collection[currentIndex];
        object IEnumerator.Current => this.Current;
        public void Dispose() { }
        public bool MoveNext() => --currentIndex >= 0;
        public void Reset() => currentIndex = collection.Count;
    }
    IEnumerable<T> sourceSequence;
    IList<T> originalSequence;

    public ReverseEnumerable(IEnumerable<T> sequence)
    {
        this.sourceSequence = sequence;
        originalSequence = sequence as IList<T>; //null이어도 문제가 되지 않음
    }
    public ReverseEnumerable(IList<T> sequence) //생성자를 오버로드하여 IList인 경우에는 효율적으로 수정
    {
        this.sourceSequence = sequence;
        originalSequence = sequence;
    }
    public IEnumerator<T> GetEnumerator()
    {
        if (originalSequence == null)
        {
            if (sourceSequence is ICollection<T>) //icollection인 경우에는 매우 느리게 동작하므로 예외처리한다
            {
                ICollection<T> source = sourceSequence as ICollection<T>;
                originalSequence = new List<T>(source);
            }
            else
            {
                originalSequence = new List<T>();
            }

            foreach (T item in sourceSequence)
            { originalSequence.Add(item); }
        }
        return new ReverseEnumerator(originalSequence);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }



}
