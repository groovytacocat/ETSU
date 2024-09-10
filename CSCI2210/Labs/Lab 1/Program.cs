using System;
using System.Collections;

namespace Sept10
{
    //Generics, Lists, Interfaces
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList<int> test = new();

            test.Add(1);
            test.Add(2);
            test.Add(3);

            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine($"test[{i}]: {test[i]}");
            }

            Console.WriteLine($"Contains(1): {test.Contains(1)}");

            test.Insert(1, 5);

            Console.WriteLine($"Test[1] should be 5. Test[1] is: {test[1]}");

            Console.WriteLine($"Index of 5 is: {test.IndexOf(5)}");

            test.RemoveAt(1);

            Console.WriteLine($"Test[1] was 5. Been removed should now be 2. Test[1] is: {test[1]}");

            test.Clear();

            Console.WriteLine("Test Values are now: ");
            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine($"test[{i}]: {test[i]}");
            }

            test.Add(1);
            test.Add(2);
            test.Add(3);
            test.Add(1);
            test.Add(2);
            test.Add(3);
            test.Add(1);
            test.Add(2);
            test.Add(3);

            Console.WriteLine("Test values: (using foreach)");
            foreach(int i in test)
            {
                Console.WriteLine(i);
            }
        }
    }

    #region ArrayList
    class ArrayList<T> : IList<T>
    {
        T[] guts = new T[0];

        public int Count { get { return guts.Length; } }

        public bool IsReadOnly => throw new NotImplementedException();

        public T this[int index]
        {
            get
            {
                return guts[index];
            }
            set
            {
                guts[index] = value;
            }
        }

        public void Add(T input)
        {
            Array.Resize(ref guts, guts.Length + 1);

            guts[^1] = input;
        }

        public bool Remove(T input)
        {
            if (guts.Length == 0) return false;

            for (int i = 0; i < guts.Length; i++)
            {
                if (guts[i].Equals(input))
                {
                    for (int j = i; j < guts.Length - 1; j++)
                    {
                        guts[j] = guts[j + 1];
                    }

                    Array.Resize(ref guts, guts.Length - 1);
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < guts.Length; i++)
            {
                if (guts[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= guts.Length) throw new ArgumentOutOfRangeException("Invalid Index");

            Array.Resize(ref guts, guts.Length + 1);

            for (int j = guts.Length - 1; j > index; j--)
            {
                guts[j] = guts[j - 1];
            }

            guts[index] = item;
        }

        public void RemoveAt(int index)
        {
            Remove(guts[index]);
        }

        public void Clear()
        {
            guts = new T[0];
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < guts.Length; i++)
            {
                if (guts[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < guts.Length; i++)
            {
                array[i] = guts[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListEnum<T>(guts);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
    #endregion

    public class ArrayListEnum<T> : IEnumerator<T>
    {
        public T[] values;

        public int index = -1;

        public T Current
        {
            get
            {
                try
                {
                    return values[index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            index++;
            return index < values.Length;
        }

        public void Reset()
        {
            index = -1;
        }

        public ArrayListEnum(T[] list)
        {
            values = list;
        }
    }

}