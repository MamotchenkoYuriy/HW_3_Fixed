using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    [Serializable]
    public class ArrayList<T> : IArrayList<T>
    {
        public T this[int index]
        {
            get
            {
                if (index < _items.Length)
                { return _items[index]; }
                else { throw new IndexOutOfRangeException(); }
            }
        }

        private T[] _items;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="items">[] T - массив элементов типа T</param>
        public ArrayList(T[] items) { this._items = items; }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="items"> List<T> - список элементов типа T</param>
        public ArrayList(List<T> items) { this._items = items.ToArray(); }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ArrayList() { }

        /// <summary>
        /// Метод для добавление нового елемента в массив
        /// </summary>
        /// <param name="item"> Элемент который нужно добавить в список</param>
        public void Add(T item)
        {
            if (_items == null)
            {
                _items = new T[1];
                _items[0] = item;
            }
            else
            {
                T[] newArray = new T[1];
                newArray[0] = item;
                _items = _items.Concat(newArray).ToArray();
            }
        }

        /// <summary>
        /// Метод для удаления элемента массива
        /// </summary>
        /// <param name="item">T item  - элемент массива который нужно удалить</param>
        public void Remove(T item)
        {
            _items = _items.Where(m => !m.Equals(item)).ToArray();
        }

        /// <summary>
        /// Метод для удаления елемента массива на i-й позиции 
        /// </summary>
        /// <param name="number"> number - номер удаляемого элемента массива</param>
        public void RemoveAt(int number)
        {
            if (number >= _items.Length) { throw new IndexOutOfRangeException(); /*return;*/ }
            Remove(_items[number]);
        }

        /// <summary>
        /// Метод 
        /// </summary>
        /// <typeparam name="TSourse"></typeparam>
        /// <returns></returns>
        public List<T> GetOnly<TSourse>()
        {
            if (_items != null)
            {
                return _items.Where(m => m.GetType() == typeof(TSourse)).ToList<T>();
            }
            else return new List<T>();
        }

        public List<T> ToList()
        {
            if (_items != null)
            {
                return _items.ToList();
            }
            else return new List<T>();
        }
        public int Count { get { return _items.Length; } }

        public void Edit(int position, T entity)
        {
            if (position >= Count) { throw new IndexOutOfRangeException(); }
            _items[position] = entity;
        }
        
        
        /// <summary>
        /// Энумератор для реализации возможности пережирать коллекцию посредством foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListIEnumerator(_items);
        }

        /// <summary>
        /// Энумератор для реализации возможности пережирать коллекцию посредством foreach
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ArrayListIEnumerator(_items);
        }

        private class ArrayListIEnumerator : IEnumerator<T>, IEnumerator
        {
            private T[] _items;
            public int _position;
            public ArrayListIEnumerator(T[] items)
            {
                this._items = items;
                _position = -1;
            }

            public T Current
            {
                get { return _items[_position]; }
            }

            public void Dispose() { }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                _position++;
                return (_position < _items.Length);
            }

            public void Reset()
            {
                _position = -1;
            }
        }

        public int IndexOf(T entity)
        {
            for(int i = 0; i<Count; i++)
            {
                if (_items[i].Equals(entity)) { return i; }
            }
            return -1;
        }
    }
}
