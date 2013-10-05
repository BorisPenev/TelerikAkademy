using System;
using System.Diagnostics;

class AssertionsHomework
{
    public static void SelectionSort<T>(T[] arr) where T : IComparable<T>
    {
        Debug.Assert(arr != null, "array cannot be null");

        for (int index = 0; index < arr.Length-1; index++)
        {
            int minElementIndex = FindMinElementIndex(arr, index, arr.Length - 1);
            Swap(ref arr[index], ref arr[minElementIndex]);
        }
        
        Debug.Assert(IsArraySorted(arr), "");
    }

    private static bool IsArraySorted<T>(T[] arr) where T : IComparable<T>
    {
        bool result = true;

        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (i + 1 >= arr.Length - 1)
            {
                break;
            }

            if (arr[i].CompareTo(arr[i + 1]) > 0)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private static int FindMinElementIndex<T>(T[] arr, int startIndex, int endIndex) 
        where T : IComparable<T>
    {
        Debug.Assert(arr != null, "array cannot be null");
        Debug.Assert(startIndex >= 0, "startIndex should be positive!");
        Debug.Assert(endIndex >= 0, "endIndex should be positive!");
        Debug.Assert(endIndex <= arr.Length - 1, "endIndex should be smaller than the length of the array!");
        Debug.Assert(startIndex <= endIndex, "startIndex should be smaller than or equal to endIndex!");

        int minElementIndex = startIndex;
        for (int i = startIndex + 1; i <= endIndex; i++)
        {
            if (arr[i].CompareTo(arr[minElementIndex]) < 0)
            {
                minElementIndex = i;
            }
        }

        Debug.Assert(IsMinElement(arr, minElementIndex, startIndex, endIndex), "The index " + minElementIndex + " is not for the smaller element!");

        return minElementIndex;
    }

    private static bool IsMinElement<T>(T[] arr, int minElementIndex, int startIndex, int endIndex)
        where T : IComparable<T>
    {
        var result = true;

        for (int i = startIndex; i < endIndex - 1; i++)
        {
            if (arr[minElementIndex].CompareTo(arr[i]) > 0)
            {
                result = false;
                break;
            }
        }

        return result;
    }


    private static void Swap<T>(ref T x, ref T y)
    {
        T oldX = x;
        x = y;
        y = oldX;
    }

    public static int BinarySearch<T>(T[] arr, T value) where T : IComparable<T>
    {
        Debug.Assert(arr != null, "array cannot be null");
        Debug.Assert(value != null, "value cannot be null!");

        Debug.Assert(IsArraySorted(arr), "");

        var result = BinarySearch(arr, value, 0, arr.Length - 1);
        return result;
    }

    private static int BinarySearch<T>(T[] arr, T value, int startIndex, int endIndex) 
        where T : IComparable<T>
    {
        Debug.Assert(startIndex <= endIndex, "startIndex should be smaller than or equal to endIndex!");
        Debug.Assert(startIndex >= 0, "startIndex should be positive!");
        Debug.Assert(endIndex >= 0, "endIndex should be positive!");
        Debug.Assert(endIndex < arr.Length - 1, "endIndex should be smaller than the length of the array!");

        while (startIndex <= endIndex)
        {
            int midIndex = (startIndex + endIndex) / 2;
            if (arr[midIndex].Equals(value))
            {
                return midIndex;
            }
            if (arr[midIndex].CompareTo(value) < 0)
            {
                // Search on the right half
                startIndex = midIndex + 1;
            }
            else 
            {
                // Search on the right half
                endIndex = midIndex - 1;
            }
        }

        // Searched value not found
        return -1;
    }

    static void Main()
    {
        int[] arr = new int[] { 3, -1, 15, 4, 17, 2, 33, 0 };
        Console.WriteLine("arr = [{0}]", string.Join(", ", arr));
        SelectionSort(arr);
        Console.WriteLine("sorted = [{0}]", string.Join(", ", arr));

        SelectionSort(new int[0]); // Test sorting empty array
        SelectionSort(new int[1]); // Test sorting single element array

        Console.WriteLine(BinarySearch(arr, -1000));
        Console.WriteLine(BinarySearch(arr, 0));
        Console.WriteLine(BinarySearch(arr, 17));
        Console.WriteLine(BinarySearch(arr, 10));
        Console.WriteLine(BinarySearch(arr, 1000));
    }
}
