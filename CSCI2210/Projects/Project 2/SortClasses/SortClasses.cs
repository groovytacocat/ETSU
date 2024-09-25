namespace SortClasses
{
    public class IterSort : ISort
    {
        public void Sort<T>(List<T> values) where T: IComparable<T>
        {
            for(int i = 1; i < values.Count; i++)
            {
                for(int j = 0; j < values.Count - 1; j++)
                {
                    if(values[j].CompareTo(values[j + 1]) > 0) {
                        T temp = values[j];
                        values[j] = values[j + 1];
                        values[j + 1] = temp;
                    }
                }
            }
        }

    }

    public class RecursiveSort : ISort
    {
        public void Sort<T>(List<T> values) where T: IComparable<T>
        {

        }

        public void QuickSort<T>(List<T> values, int low, int high) where T: IComparable<T>
        {
            if(low < high){
                int pivotIndex = Partition<T>(values, low, high);
                QuickSort<T>(values, low, pivotIndex - 1);
                QuickSort<T>(values, pivotIndex + 1, high);
            }
        }

        public int Partition<T>(List<T> values, int low, int high) where T: IComparable<T>
        {
            T pivot = values[high];
            int i = low - 1;
            for(int j = low; j <= high - 1; j++)
            {
                if(values[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    T temp = values[i];
                    values[i] = values[j];
                    values[j] = temp; 
                }
            }

            T swap = values[i + 1];
            values[i + 1] = values[high];
            values[high] = swap;

            return i + 1;
        }
    }
}
