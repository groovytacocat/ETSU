using System;
using System.Collections;

namespace Sept12;

internal class ArrayList<T> : IList<T>
{
    T[] guts = new T[0];

    public int Count { get { return guts.Length; } }

    public bool IsReadOnly => false;

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
        T[] newGuts = new T[guts.Length + 1];

        for(int i = 0; i < guts.Length; i++)
        {
            newGuts[i] = guts[i];
        }

        newGuts[^1] = input;

        guts = newGuts;
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

                T[] swap = new T[guts.Length - 1];

                for(int x = 0; x < swap.Length; x++)
                {
                    swap[x] = guts[x];
                }

                guts = swap;

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

        T[] resize = new T[guts.Length + 1];

        guts.CopyTo(resize, 0);

        for (int j = resize.Length - 1; j > index; j--)
        {
            resize[j] = resize[j - 1];
        }

        resize[index] = item;

        guts = resize;
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
        if(array.Length < guts.Length) throw new ArgumentException("Array is too small");
        
        for (int i = 0; i < guts.Length; i++)
        {
            array[i + arrayIndex] = guts[i];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new ArrayListEnum<T>(guts);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }
}


/// <summary>
/// Enumerator for the <see cref="ArrayList"/> from above.
/// Read C# docs for how to implement this.
/// </summary>
/// <typeparam name="T">Generic Input Type</typeparam>
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
                throw new IndexOutOfRangeException("Invalid Index");
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
        //Empty Method C# docs say for "releasing unmanaged resources"
        //No resources that are unmanaged to be released as far as I know
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