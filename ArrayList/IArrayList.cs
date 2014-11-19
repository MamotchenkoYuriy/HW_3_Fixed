using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    public interface IArrayList<T> : IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }

        void Add(T item);
        void Remove(T item);
        void RemoveAt(int number);

        void Edit(int position, T entity);

        List<T> GetOnly<TSourse>();

        List<T> ToList();

        int Count { get; }

        int IndexOf(T entity);
    }

}
